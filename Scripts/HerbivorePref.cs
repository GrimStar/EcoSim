using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbivorePref : MonoBehaviour, IPrefPlantType
{
    [SerializeField]
    string prefPlant = "Foliage";
    public string PrefPlant
    {
        get { return prefPlant; }
    }
}
