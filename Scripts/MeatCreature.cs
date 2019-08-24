using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCreature : MonoBehaviour, IAmMeat
{
    [SerializeField]
    string meatType = "RedMeat";

    public string MeatType { get {return meatType; } }
     
}
