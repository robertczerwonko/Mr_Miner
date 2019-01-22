using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusUI : MonoBehaviour
{
    private static BonusUI instance;

    public static BonusUI Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<BonusUI>();
            }
            return instance;
        }
    }

    [SerializeField] private GameObject dynamicScoreText;
    [SerializeField] private RectTransform Dynamic_Canvas;

    [SerializeField] private Text admireText;
    [SerializeField] private Text healthPoints;

    private bool bonusIsShowing;
    private bool admireIsShowing;
    private bool healthIsShowing;

    [SerializeField] private float fadeTime = 0.7f;


    private const int textSize = 65;
    private int sumBP;


    public void Start()
    {
        admireText.enabled = false;
        healthPoints.enabled = false;
    }

    public void Update()
    {
        if (healthIsShowing)
        {
            StartCoroutine(FadeHealth(fadeTime));
        }
        if (admireIsShowing)
        {
            StartCoroutine(FadeAdmire(fadeTime));
        }

    }


    public void createFadeText(Vector3 position,GroundType type)
    {
        dynamicScoreText.GetComponent<Text>().text = "+" + GameManager.Instance.level;
        dynamicScoreText.GetComponent<Text>().color = setTextColor(type);
        GameObject sct = (GameObject) Instantiate(dynamicScoreText, position, Quaternion.identity);
        sct.transform.SetParent(Dynamic_Canvas);
        sct.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
    }

    private Color setTextColor(GroundType t)
    {
        switch(t)
        {
            case GroundType.DIAMOND:
            case GroundType.DOUBLE_DIAMOND:
                return Color.cyan;
            case GroundType.RUBIN:
            case GroundType.DOUBLE_RUBIN:
                return Color.red;
            case GroundType.DOUBLE_GREEN:
            case GroundType.GREEN:
                return Color.green;
            case GroundType.YELLOW:
            case GroundType.DOUBLE_YELLOW:
                return Color.yellow;
            default:
                return Color.cyan;
        }
    }


    public void ShowHealthPoints(string s)
    {
        healthPoints.text = s;
        healthPoints.enabled = true;
        healthIsShowing = true;
    }

    public void ShowAdmireText()
    {
        if (!GameManager.Instance.isFullHealth())

            admireText.text = HelperUI.motivationText() + "\n +1 LIVE";
        else

            admireText.text = HelperUI.motivationText();

        admireText.enabled = true;
        admireIsShowing = true;
    }

    private IEnumerator FadeHealth(float time)
    {
        yield return new WaitForSeconds(time);
        healthPoints.enabled = false;
        healthIsShowing = false;
    }

    private IEnumerator FadeAdmire(float time)
    {
        yield return new WaitForSeconds(time);
        admireText.enabled = false;
        admireIsShowing = false;
    }



}
