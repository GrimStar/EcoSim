using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCreature : MonoBehaviour, IAmPlant
{
    [SerializeField]
    string plantType = "Foliage";
    public string PlantType { get { return plantType; } }
}
