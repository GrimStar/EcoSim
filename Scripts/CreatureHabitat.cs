using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHabitat : MonoBehaviour, IHavePrefTemperature
{
    [SerializeField]
    float prefTemperature = 30;
    public float PrefTemp
    {
        get { return prefTemperature; }
        set { prefTemperature = value; }
    }

    [SerializeField]
    float tempDifferenceTolerance = 10;
    public float TempDifTolerance
    {
        get { return tempDifferenceTolerance; }
    }
  
}
