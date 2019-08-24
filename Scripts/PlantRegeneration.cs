using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantRegeneration : MonoBehaviour
{
    [SerializeField]
    float breedingTime;
    float curBreedingTime;
    [SerializeField]
    GameObject seed;
    public bool CanReproduce
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        curBreedingTime = breedingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanReproduce)
        {

            if(curBreedingTime > 0)
            {
                curBreedingTime -= Time.deltaTime;
            }
            else
            {
                CanReproduce = false;
                curBreedingTime = breedingTime;
                SpawnSeed();
                
            }
        }
    }
    void SpawnSeed()
    {
        CreatureLiveStatistics_Plant _stats = GetComponent<CreatureLiveStatistics_Plant>();
        float radius = 0;
        if(_stats != null)
        {
            radius = _stats.SeedDropRadius;
            Vector3 spawnPos = transform.position + Vector3.right * 3;
            spawnPos = transform.position + Random.insideUnitSphere * radius;
            spawnPos.y = transform.position.y;
            GameObject go = Instantiate(seed);
            go.transform.position = spawnPos;
            go.GetComponent<PlantSeed>()._plantStats = GetComponent<IHavePlantStats>();
        }
        else
        {
            Debug.Log("PlantLiveStats = null");
        }
        
        
    }
}
