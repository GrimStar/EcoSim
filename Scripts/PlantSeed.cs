using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour, IDestroyedOnTrigger
{

    float startDelay = 4;
    IAmTile _tileData;
    public IHavePlantStats _plantStats;
    public LayerMask mask;
    private int growRank;
    private IHavePlantRanks _myRanks;
    bool go = false;
    bool canPlant = true;
    // Start is called before the first frame update
    void Start()
    {
        _myRanks = GetComponent<IHavePlantRanks>();
        if(_myRanks != null)
        {
            growRank = _myRanks.GrowRank;
        }
    }
    void Update()
    {
        if(startDelay > 0)
        {
            startDelay -= Time.deltaTime;
        }
        else if(!go)
        {
            go = true;
            if (canPlant)
            {
                CheckTile();
            }
            else
            {
                SelfDestruct();
            }
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
    public void CheckSurroundingObjects(int otherRank, GameObject otherObject)
    {
        if(growRank > otherRank)
        {
            Destroy(otherObject);
        }
        else
        {
            canPlant = false;
        }

    }
    void CheckTile()
    {
       
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 50, mask))
        {
             _tileData = hit.transform.GetComponent<IAmTile>();
            if (_tileData != null)
            {     
                if (_plantStats != null)
                {
                    if (CheckAvailability(_tileData.Moisture, _plantStats.PrefGroundMoisture, _plantStats.GroundMoistureDifTol))
                    {
                        if (CheckAvailability(_tileData.Temperature, _plantStats.PrefTemperature, _plantStats.TemperatureDifTol))
                        {
                            if (transform != null)
                            {
                                GameObject _prefab = _plantStats.Prefab;
                                Vector3 offset = new Vector3(0, 0.25f, 0);
                                SpawnPlant(_prefab, hit.point + offset);
                            }
                        }
                        else
                        {
                            SelfDestruct();
                        }
                    }
                    else
                    {
                        SelfDestruct();
                    }
                }
                else
                {
                    SelfDestruct();
                }
            }
            else
            {
                SelfDestruct();
            }
        }
        else
        {
            SelfDestruct();
        }
    }

    private void SelfDestruct()
    {
        Destroy(transform.gameObject);
    }

    void SpawnPlant(GameObject _prefab, Vector3 _pos)
    {
        if (_prefab != null)
        {
            GameObject go = Instantiate(_prefab);
            if (go != null)
            {
                go.transform.position = _pos;
                go.GetComponent<CreatureMemory_Other>()._tileData = _tileData;
            }
        }
        SelfDestruct();
    }
}
