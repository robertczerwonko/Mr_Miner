using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayModeUI : MonoBehaviour {
   
    public static PlayModeUI instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Text levelText;
    [SerializeField] private Text pointsText;
    [SerializeField] private Image[] livesImages;
    [SerializeField] private Image[] progressImage;

    public float AlphaColor = 0.3f;

    public void activePlayModeUI(bool active)
    {
        levelText.enabled = active;
        pointsText.enabled = active;
        foreach(Image i in livesImages)
        {
            i.enabled = active;
        }
        foreach(Image p in progressImage)
        {
            p.enabled = active;
        }
    }

    public void setLevelText(int level)
    {
        levelText.text = "LEVEL " + level.ToString();
    }

    public void setPointsText(int points)
    {
        pointsText.text = points.ToString();
    }

    public void updateProgressBar(int progress)
    {
        for(int i = 0; i< progressImage.Length;i++)
        {
            if(i <= progress)
            {
                var tempColor = progressImage[i].color;
                tempColor.a = 1f;
                progressImage[i].color = tempColor;
            }
            else
            {
                var tempColor = progressImage[i].color;
                tempColor.a = AlphaColor;
                progressImage[i].color = tempColor;
            }
        }
    }

    public void updateLivesBar(int lives)
    {
        for(int i = 0; i < livesImages.Length; i++)
        {
            if(i+1 <= lives)
            {
                var tempColor = livesImages[i].color;
                tempColor.a = 1f;
                livesImages[i].color = tempColor;
            }
            else
            {
                var tempColor = livesImages[i].color;
                tempColor.a = AlphaColor;
                livesImages[i].color = tempColor;
            }
        }
    }

    public void loadDatatoUI(int points, int lives, int level, int progress)
    {
        setLevelText(level);
        updateLivesBar(lives);
        setPointsText(points);
        updateProgressBar(progress);
    }
}
