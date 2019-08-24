using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivorePref : MonoBehaviour, IPrefMeatType
{
    [SerializeField]
    string prefMeat = "RedMeat";
    public string PrefMeat
    {
        get
        {
            return prefMeat;
        }
    }
}
