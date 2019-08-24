using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMemory : MonoBehaviour
{
    public bool isMigrating = true;
    public bool isWandering = false;
    public bool isSettled = false;
    public bool isHunting = false;
    private int foodSourceCount;
    public int FoodSourceCount
    {
        get
        {
            return foodSourceCount;
        }
        set
        {
            foodSourceCount += value;
        }
    }
    public BiomeMemory CurBiome
    {
        get { return _curBiome; }
        set { _curBiome = value; }
    }

    public List<Transform> primaryPotentialPrey;
    public List<Transform> secondaryPotentialPrey;
    public List<Vector3> lastSeenPreyLocations;
    public int maxPreyLocations = 20;
    public List<Transform> potentialDangers;
    public List<Transform> potentialCompetitors;
    public List<Transform> waterSources;
    public BiomeMemory _curBiome;
    public Transform curTargetPrey;
    public Transform curWaterSource;
    
}
