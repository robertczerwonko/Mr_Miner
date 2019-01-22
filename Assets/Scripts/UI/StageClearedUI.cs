using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearedUI : MonoBehaviour {

    [SerializeField] private Text pointsText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text bestPointsText;


    public void setStageClearUI(int p,int bp, int l)
    {
        pointsText.text = p.ToString();
        bestPointsText.text = string.Format("BEST: {0}", bp);
        levelText.text = string.Format("LEVEL {0}\n CLEARED!", l);
    }
}
