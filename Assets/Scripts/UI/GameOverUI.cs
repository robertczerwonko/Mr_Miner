using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private Text pointsText;
    [SerializeField] private Text bestPointsText;


    public void setGameOverUi(int p, int bp)
    {
        pointsText.text = p.ToString();
        bestPointsText.text = string.Format("BEST: {0}", bp);
    }
}
