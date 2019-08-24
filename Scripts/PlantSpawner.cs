using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{

    [SerializeField]
    float growthTime = 30;
    [SerializeField]
    GameObject plantPrefab;
    GameObject spawnedPlant;
    bool growingPlant = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnedPlant = Instantiate(plantPrefab);
        spawnedPlant.transform.position = transform.position;
        spawnedPlant.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!growingPlant)
        {
            CheckPlantStatus();
        }
    }
    void CheckPlantStatus()
    {
        if(spawnedPlant == null)
        {
            if (!growingPlant)
            {
                growingPlant = true;
                StartCoroutine(GrowthCountdown());
            }
        }
    }
    void SpawnPrefab()
    {
        spawnedPlant = Instantiate(plantPrefab);
        spawnedPlant.transform.position = transform.position;
        spawnedPlant.transform.rotation = transform.rotation;
    }
    IEnumerator GrowthCountdown()
    {
        yield return new WaitForSeconds(growthTime);
        SpawnPrefab();
        growingPlant = false;
        
    }
}
