using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeTemperature : MonoBehaviour
{
    [SerializeField]
    Transform sun;
    [SerializeField]
    float temperatureRatio = 0;
    [SerializeField]
    float curTemperature;
    float maxTemperature = 80;
    float sunDistance;
    float maxDistance = 250;
    float minDistance;
    BiomeMemory biomeMemory;
    // Start is called before the first frame update
    void Start()
    {
        biomeMemory = GetComponent<BiomeMemory>();
        maxDistance = SceneSetupData.instance.maxSunDistance;
        minDistance = SceneSetupData.instance.minSunDistance;
        maxTemperature = SceneSetupData.instance.maxBiomeTemperture;
    }

    // Update is called once per frame
    void Update()
    {
        sunDistance = DistanceFromSun();
        temperatureRatio = TemperatureRatio();
        curTemperature = maxTemperature * temperatureRatio;
        biomeMemory.temperature = curTemperature;
    }
    float DistanceFromSun()
    {
        //float _distance = Vector3.Distance(transform.position, sun.position);
        //return _distance;
        
        float distance = Mathf.Abs(transform.position.z - sun.position.z);
        return distance;

    }
    float TemperatureRatio()
    {
        float tempRatio = Mathf.InverseLerp(maxDistance, minDistance, sunDistance);
        return tempRatio;
    }
}
