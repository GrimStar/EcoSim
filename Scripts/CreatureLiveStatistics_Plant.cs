using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureLiveStatistics_Plant : MonoBehaviour
{
    public float Health { get; set; }
    public float Age { get; set; }
    public float SeedDropRadius { get; set; }
    CreatureStatistics_Plant _stats;

    private void Start()
    {
        _stats = GetComponent<CreatureStatistics_Plant>();
        Health = _stats.maxHealth;
        SeedDropRadius = _stats.seedDropRadius;
    }

}
