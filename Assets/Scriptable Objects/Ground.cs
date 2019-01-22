using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Ground", menuName = "Ground")]
public class Ground : ScriptableObject {

    public new string name;


    public int rowNumer;


    public bool isValuable;


    public GroundType type;


}
