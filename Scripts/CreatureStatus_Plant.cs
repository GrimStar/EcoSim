using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStatus_Plant : MonoBehaviour, IHaveMass
{
    public LayerMask mask;
    CreatureMemory_Other _memory;
    IHavePlantStats _plantStats;
    [SerializeField]
    float mass = 0;
    PlantRegeneration _plantReproduction;
    public float Mass
    {
        get { return mass; }
        set
        {
            float amountRemoved = mass - value;
            if (GetComponent<IAmMeat>() != null)
            {
                _memory.CurBiome.meatMass -= amountRemoved;
            }
            else if (GetComponent<IAmPlant>() != null)
            {
                _memory.CurBiome.foliageMass -= amountRemoved;
            }
            mass = value;
            if (value <= 0)
            {
                SelfDestruct();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _plantReproduction = GetComponent<PlantRegeneration>();
        _plantStats = GetComponent<IHavePlantStats>();
        _memory = GetComponent<CreatureMemory_Other>();
        CheckTile();
    }
    void Update()
    {
        CheckTemperature();
    }
    bool CheckMoisture()
    {
        return CheckAvailability(_memory._tileData.Moisture, _plantStats.PrefGroundMoisture, _plantStats.GroundMoistureDifTol);
    }
    bool CheckTemperature()
    {
        if(CheckAvailability(_memory.CurBiome.temperature, _plantStats.PrefTemperature, _plantStats.TemperatureDifTol))
        {
            _plantReproduction.CanReproduce = true;
            return true;
        }
        else
        {
            _plantReproduction.CanReproduce = false;
            return false;
        }
    }
    bool CheckAvailability(float amount, float pref, float tol)
    {
        float dif = Mathf.Abs(amount - pref);
        if (dif < tol)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void CheckTile()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1, mask))
        {
            Debug.Log("hit tile");
            IAmTile _tileData = hit.transform.GetComponent<IAmTile>();
            if(_tileData != null)
            {               
                if(_plantStats != null)
                {
                    if(CheckAvailability(_tileData.Moisture, _plantStats.PrefGroundMoisture, _plantStats.GroundMoistureDifTol))
                    {
                        if(CheckAvailability(_tileData.Temperature, _plantStats.PrefTemperature, _plantStats.TemperatureDifTol))
                        {
                            _memory._tileData = _tileData;
                        }
                        
                    }
                }
            }
        }
    }
    void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}
