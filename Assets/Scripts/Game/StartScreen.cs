using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

    #region singleton
    public static StartScreen instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    [HideInInspector] public bool IsStartScreen = true; 
    [HideInInspector] public int DestroyedRows = 0;
    [HideInInspector] public int depthValue = 0;
    [HideInInspector] public int MasterLevel = 0;
    [HideInInspector] public SaveState ourState;

    [SerializeField] private HoistMovement Hoist;
    [SerializeField] private GameObject StartCanvas;
    [SerializeField] private Row RowPrefab;
    [SerializeField] private GameObject bottomRow;
    [SerializeField] private PlayModeUI playModeUiController;

    private int NumberOfRows = 4;
    private bool justFinishedStage;
    private bool loadedData;
    private List<GameObject> AllRows = new List<GameObject>();

    public void Start()
    {
        SaveMananger.instance.Load();
        ourState = SaveMananger.instance.state;
    }

    public void loadData()
    {
        MasterLevel = ourState.masterLevel;
        GameManager.Instance.SetLoadedData(ourState.points, ourState.bestCombo, ourState.lives, MasterLevel,ourState.bestPoints);
        loadedData = true;
    }

    /// <summary>
    /// IsStartScreen is BOOL which controls that if game should run or not, Player starts the game by pressing at screen,
    /// if it's first load it is loading data from Player Prefs, else  player lost and has to reset points, but 
    /// </summary>
	void Update ()
    {
        if (!IsStartScreen)
        {
            return;
        }
		if(Input.GetMouseButtonDown(0)) //TODO:(A) START GAME after player click on screen
        {

            if (!loadedData)
            {
                loadData();
            }
            else
            {
                GameManager.Instance.ResetPoints();
                SaveMananger.instance.Save();
            }
            playModeUiController.activePlayModeUI(true);
            depthValue = 0; // Player always start after restar at phase 1 (From 4 depths)
            DestroyedRows = 0;
            IsStartScreen = false;
            StartCanvas.SetActive(false);
            StartCoroutine(LoadGame());
        }
	}

    public void CheckForRows()
    {
        DestroyedRows++;
        GameManager.Instance.saveData();
        SaveMananger.instance.state = ourState;
        SaveMananger.instance.Save();
        if (DestroyedRows >= NumberOfRows)
        {
            DestroyedRows = 0;
            bottomRow.SetActive(false);
            StartCoroutine(RestartLevel());
        }
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(LoadRows());
        Hoist.GameStarted = true;
    }

    private IEnumerator LoadRows()
    {
        for(int i=0; i<AllRows.Count; i++)
        {
            if(AllRows[i])
            {
                Destroy(AllRows[i]);
                yield return null;
            }
        }
        AllRows = new List<GameObject>();
        int valueToSpawn = Random.Range(0, 4);
        for (int i = 0; i < NumberOfRows; i++) {
            RowPrefab.GetComponent<Row>().SetPrefabs(valueToSpawn);
            AllRows.Add(Instantiate(RowPrefab, new Vector3(0f, i * -1f, 0f), Quaternion.identity).gameObject);
            yield return null;
        }
       
        bottomRow.SetActive(true);

    }


    private IEnumerator RestartLevel()
    {
        if(Hoist.ReturnedToDefaultPosition())
        {           
            depthValue++;
            if (depthValue >= 4)
            {
                depthValue = 0;
                justFinishedStage = true;
                
                if (justFinishedStage)
                {
                    BonusStage.instance.changeTitleInPlayMode(true);
                    BonusStage.instance.StartBonusLevel();
                    while (BonusStage.instance.isPlaying)
                    {
                        yield return null;
                    }
                    justFinishedStage = false;

                    GameManager.Instance.saveData();
                    SaveMananger.instance.state = ourState;
                    SaveMananger.instance.Save();
                }
                Hoist.GoToNextMasterLevel = true;
                GameManager.Instance.GoToNextMasterLevels(MasterLevel);
                MasterLevel++;                
                ourState.masterLevel = MasterLevel;
                SaveMananger.instance.state = ourState;
                SaveMananger.instance.Save();
                while (Hoist.GoToNextMasterLevel)
                {
                    yield return null;
                }
                GameManager.Instance.turnOffSwapingLevelUI();                
                
                
            }

            Transform HoistTransform = Hoist.transform;
            HoistTransform.position = new Vector3(0, HoistTransform.position.y, 0);
            HoistTransform.rotation = new Quaternion();
            Hoist.GoToNextLevel = true;

            while (Hoist.GoToNextLevel)
            {
                yield  return  null;
            }
            
            GameManager.Instance.SetDepth(depthValue);

          

            StartCoroutine(LoadRows());
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(RestartLevel());
        }
    }



}
