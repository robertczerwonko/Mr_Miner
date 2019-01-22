using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusStage : MonoBehaviour {

    #region singleton


    const float leftBorderX = -2.5f;
    [SerializeField] private List<GameObject> BonusGems;
    [SerializeField] private GameObject DirtPrefab;
    [SerializeField] private Text bonusPointsValueText;
    [SerializeField] private Text movesLeftText;
    [SerializeField] private BonusUI BonusControl;

    [SerializeField] private GameObject stageLevelText;
    [SerializeField] private GameObject bonusStageText;

    public static BonusStage instance;
    public void Awake()
    {
        instance = this;
        bonusPointsValueText.enabled = false;
        movesLeftText.enabled = false;
    }

    #endregion



    public int howManyDiamondsToSpawn = 50;
    public bool isPlaying;
    //public int points;

    private float randomX, randomY;
    private int movesLeft = 2;
    private List<GameObject> allGems = new List<GameObject>();
    private List<GameObject> allDirt = new List<GameObject>();



    public void StartBonusLevel()
    {
        isPlaying = true;
        bonusPointsValueText.enabled = true;
        movesLeftText.enabled = true;
        SpawnBonusDiaondsBlocks();
        StartCoroutine(FinishBonusLevel());
    }

    public void madeMove()
    {
        movesLeft--;
        if (movesLeft > 0)
            movesLeftText.text = "Moves : " + movesLeft;
        else
            movesLeftText.enabled = false;
    }


    public IEnumerator FinishBonusLevel()
    {
        while (!HoistMovement.instance.MadeTwoMoves)
        {
            yield return null;
        }
        DestroyLeftDiamondsBlocks();
        changeTitleInPlayMode(false);
        bonusPointsValueText.text = 0.ToString();
        bonusPointsValueText.enabled = false;
        //GameManager.Instance.AddEarnedBonusPoints(points);
        //BonusControl.ShowBonusPoints(points);
        //points = 0;
        isPlaying = false;        
    }

    public void changeTitleInPlayMode(bool b)
    {
        if(b)
        {
            stageLevelText.SetActive(false);
            bonusStageText.SetActive(true);
        }
        else
        {
            stageLevelText.SetActive(true);
            bonusStageText.SetActive(false);
        }
        
    }

    private void SpawnBonusDiaondsBlocks()
    {

        for (int i = 0; i < howManyDiamondsToSpawn; i++)
        {
            randomY = Random.Range(0f, -4f);
            randomX = Random.Range(-1.8f, 1.8f);
            allGems.Add(Instantiate(BonusGems[Random.Range(0,BonusGems.Count)], new Vector3(randomX, randomY, 0f), Quaternion.identity).gameObject);
        }
        for(int j = 0; j< 5;j++)
        {
            for(int k = 0; k < 6; k++)
            {
                allDirt.Add(Instantiate(DirtPrefab, new Vector3(leftBorderX + k, j *  -1, 0f), Quaternion.identity).gameObject);
            }
        }
    }

    private void DestroyLeftDiamondsBlocks()
    {
        movesLeft = 2;
        movesLeftText.text = "Moves : " + movesLeft;
        foreach (GameObject gameO in allGems)
        {
            Destroy(gameO);
        }
        foreach(GameObject dirt in allDirt)
        {
            Destroy(dirt);
        }
    }

    
}
