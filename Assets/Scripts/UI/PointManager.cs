using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [SerializeField] private HoistManager hManager;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            hManager.deactiveType();
            GameManager.Instance.SetPoints();
        }
    }


}

