using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAddFood : MonoBehaviour, IConsume
{
    CreatureStatus _creatureStats;

    void Start()
    {
        _creatureStats = GetComponent<CreatureStatus>();
        
    }
    // Start is called before the first frame update
    public void IAddFood(float amount)
    {
       
        _creatureStats.Food += amount;
    }
}
