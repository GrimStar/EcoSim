using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHabitat_Plant : MonoBehaviour, IHavePlantStats
{
    [SerializeField]
    float prefTemperature = 30;
    public float PrefTemperature
    {
        get { return prefTemperature; }
     
    }

    [SerializeField]
    float tempDifferenceTolerance = 10;
    public float TemperatureDifTol
    {
        get { return tempDifferenceTolerance; }
    }
    [SerializeField]
    float prefGroundSoftness;
    public float PrefGroundSoftness
    {
        get { return prefGroundSoftness; }
    }
    [SerializeField]
    float groundSoftnessDifTol;
    public float GroundSoftnessDifTol
    {
        get { return groundSoftnessDifTol; }
    }
    [SerializeField]
    float prefGroundMoisture;
    public float PrefGroundMoisture
    {
        get { return prefGroundMoisture; }
    }
    [SerializeField]
    float groundMoistureDifTol;
    public float GroundMoistureDifTol
    {
        get { return groundMoistureDifTol; }
    }
    [SerializeField]
    float prefUVAmount;
    public float PrefUVAmount
    {
        get { return prefUVAmount; }
    }
    [SerializeField]
    float uVDifTol;
    public float UVDifTol
    {
        get { return uVDifTol; }
    }
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab
    {
        get { return prefab; }
        set { prefab = value; }
    }
    
}
