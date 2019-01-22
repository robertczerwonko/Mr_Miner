using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoistManager : MonoBehaviour {

    public GameObject diamondObject;
    public GameObject diamondsObject;
    public bool haveDiamond;
    public int howManyDiamonds;

    public GameObject Rubin;
    public GameObject Rubins;

    public GameObject Yellow;
    public GameObject Yellows;

    public GameObject Green;
    public GameObject Greens;

    private List<GameObject> GemsList = new List<GameObject>();
    private void Start()
    {
        GemsList.Add(diamondObject);
        GemsList.Add(diamondsObject);
        GemsList.Add(Rubin);
        GemsList.Add(Rubins);
        GemsList.Add(Yellow);
        GemsList.Add(Yellows);
        GemsList.Add(Green);
        GemsList.Add(Greens);
    }

    //SOLUTION FOR TIME WHEN THERE IS ONLY ONE TYPE OF GEMS IN LEVEL!
    public void activeType(GroundType gt)
    {
        howManyDiamonds++;
        switch (gt)
        {
            case GroundType.DIAMOND:
                {
                    
                    if (!diamondObject.activeSelf && howManyDiamonds == 1)
                    {
                        diamondObject.SetActive(true);           
                    }
                    else if(!diamondsObject.activeSelf && howManyDiamonds == 2)
                    {
                        diamondsObject.SetActive(true);
                        if (diamondObject.activeSelf) diamondObject.SetActive(false);
                        GameManager.Instance.CurrentType = GroundType.DOUBLE_DIAMOND;
                    }
                    haveDiamond = true;
                }
              break;
            case GroundType.RUBIN:
                {
                    if (!Rubin.activeSelf && howManyDiamonds == 1)
                    {
                        Rubin.SetActive(true);
                    }
                    else if (!Rubins.activeSelf && howManyDiamonds == 2)
                    {
                        Rubins.SetActive(true);
                        if (Rubin.activeSelf) Rubin.SetActive(false);
                        GameManager.Instance.CurrentType = GroundType.DOUBLE_RUBIN;
                    }
                    haveDiamond = true;
                }
                break;
            case GroundType.YELLOW:
                {
                    if (!Yellow.activeSelf && howManyDiamonds == 1)
                    {
                        Yellow.SetActive(true);
                    }
                    else if (!Yellows.activeSelf && howManyDiamonds == 2)
                    {
                        Yellows.SetActive(true);
                        if (Yellow.activeSelf) Yellow.SetActive(false);
                        GameManager.Instance.CurrentType = GroundType.DOUBLE_YELLOW;
                    }
                    haveDiamond = true;
                }
                break;
            case GroundType.GREEN:
                {
                    if (!Green.activeSelf && howManyDiamonds == 1)
                    {
                        Green.SetActive(true);
                    }
                    else if (!Greens.activeSelf && howManyDiamonds == 2)
                    {
                        Greens.SetActive(true);
                        if (Green.activeSelf) Green.SetActive(false);
                        GameManager.Instance.CurrentType = GroundType.DOUBLE_GREEN;
                    }
                    haveDiamond = true;
                }
                break;

        }
    }

    public void deactiveType()
    {
        foreach (GameObject a in GemsList)
        {
            if (a.activeSelf)
                a.SetActive(false);
        }
        haveDiamond = false;
        howManyDiamonds = 0;
    }    
}
