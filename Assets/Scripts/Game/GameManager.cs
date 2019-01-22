using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GroundType CurrentType;

    [SerializeField] private PlayModeUI playModeUIController;
    [SerializeField] private GameOverUI gameOverUI; // LOST GAME UI
    [SerializeField] private StageClearedUI StageClearedUI; // BETWEEN LEVELS UI
    [SerializeField] private HoistMovement hoist; // PLAYER MOVEMENT
    [SerializeField] private BonusUI BonusControl; // BONUS LEVEL UI

    private float pointsValue;
    private float bestPointsValue;
    private float pointsEarnedThisMasterLevel;
    private int Lives = 3;
    private int ActiveComboValue;
    private int BestComboValue;
    private float speedModifer;
    public int level = 1;
    

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //playModeUIController = PlayModeUI.instance;
        playModeUIController.activePlayModeUI(false);
    }


    public void updateScore()
    {
        pointsValue += level;
        playModeUIController.setPointsText((int)pointsValue);
        if (bestPointsValue < pointsValue) bestPointsValue = pointsValue;
    }

    public void SetPoints()
    {

        int AddPoints = 0;
        if (CurrentType == GroundType.GROUND)
        {
            Lives--;
            playModeUIController.updateLivesBar(Lives);           
            if (Lives <= 0)
            {
                playModeUIController.activePlayModeUI(false);
                gameOverUI.setGameOverUi((int)pointsValue, (int)bestPointsValue);
                gameOverUI.gameObject.SetActive(true);
                hoist.GameStarted = false;
                StartScreen.instance.IsStartScreen = true;
            }
            else            
               BonusControl.ShowHealthPoints("-");
        }
        else if (CurrentType == GroundType.DOUBLE_DIAMOND || CurrentType == GroundType.DOUBLE_GREEN || CurrentType == GroundType.DOUBLE_RUBIN || CurrentType == GroundType.DOUBLE_YELLOW)
        {
            AddPoints = PointsDatabase.instance.GetNumberOfPoints(CurrentType) * level * 2;
            BonusControl.ShowAdmireText();
            if (Lives < 3 && Lives > 0)
            {
                Lives++;
                playModeUIController.updateLivesBar(Lives);
            }
        }
        else
        {
            AddPoints = PointsDatabase.instance.GetNumberOfPoints(CurrentType) * level;
        }


        if (AddPoints > 0)
        {
            ActiveComboValue++;
            pointsValue += AddPoints;
        }
        else if(AddPoints < 0)
        {
            if (ActiveComboValue > BestComboValue) BestComboValue = ActiveComboValue;
            ActiveComboValue = 0;         
        }

        playModeUIController.setPointsText((int)pointsValue);


        if (bestPointsValue < pointsValue) bestPointsValue = pointsValue;


        //TODO:(B) END GAME, By setting "IsStartScreen" to true, we reset the game and show player EndGameUI with option to restart, INT pointsValue is variable which controls amount of points, between each Level this var is saved, NOT BETWEEN STAGES!
        CurrentType = GroundType.EMPTY;
    }
            
    public bool isFullHealth()
    {
        if (Lives >= 3)
            return true;
        else
            return false;
    }

    //p - points, bc - best Combo, l - lives, ml - masterLevel
    public void SetLoadedData(int p, int bc, int l, int ml,int bp)
    {
        level = ml;
        pointsValue = p;
        BestComboValue = bc;
        bestPointsValue = bp;
        Lives = l;
        playModeUIController.loadDatatoUI(p, Lives, ml, 0);
    }

    public void ResetPoints()
    {
        Lives = 3;
        pointsValue = 0;
        ActiveComboValue = 0;
        level = 1;
        StartScreen.instance.MasterLevel = level;
        playModeUIController.loadDatatoUI((int)pointsValue, Lives, level, 0);   
        saveData();
        gameOverUI.gameObject.SetActive(false);
    }

    public void SetDepth(int d)
    {
        if (d > 5) playModeUIController.updateProgressBar(0);
        else playModeUIController.updateProgressBar(d);
    }



    public void GoToNextMasterLevels(int masterLevel)
    {
        StageClearedUI.gameObject.SetActive(true);
        if (Lives < 3 && Lives > 0)
        {
            Lives++;
            playModeUIController.updateLivesBar(Lives);
        }
        saveData();
        level++;
    
        playModeUIController.setLevelText(masterLevel + 1);
        playModeUIController.updateProgressBar(0);

        if (ActiveComboValue > BestComboValue) BestComboValue = ActiveComboValue;
        ActiveComboValue = 0;

        playModeUIController.activePlayModeUI(false);
        StageClearedUI.setStageClearUI((int)pointsValue,(int) bestPointsValue, masterLevel);
    }
   

    public void turnOffSwapingLevelUI()
    {
        playModeUIController.activePlayModeUI(true);
        StageClearedUI.gameObject.SetActive(false);
    }



    public void saveData()
    {
        StartScreen.instance.ourState.masterLevel = level;
        StartScreen.instance.ourState.bestCombo = BestComboValue;
        StartScreen.instance.ourState.lives = Lives;
        StartScreen.instance.ourState.points = (int)pointsValue;
        StartScreen.instance.ourState.bestPoints = (int)bestPointsValue;
    }
}

