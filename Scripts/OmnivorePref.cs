using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmnivorePref : MonoBehaviour, IPrefOmni
{
    [SerializeField]
    string prefFood = "Meat";
    public string PrefFoodOmni { get { return prefFood; } }
}
