using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsDatabase : MonoBehaviour {
    public PointsTypes[] PointTypes;

    public static PointsDatabase instance;

    private Dictionary<GroundType, int> PointTypesValues = new Dictionary<GroundType, int>();

    void Awake()
    {
        instance = this;
        for(int i=0; i<PointTypes.Length; i++)
        {
            PointTypesValues.Add(PointTypes[i].Type, PointTypes[i].Points);
        }
    }

    public int GetNumberOfPoints(GroundType type)
    {
        if(PointTypesValues.ContainsKey(type)) {
            return PointTypesValues[type];
        }

        return 0;
    }
}
