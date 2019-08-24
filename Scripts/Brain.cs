using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public bool isHungry = false;
    bool isHunting = false;
    bool isGettingWater = false;
    CreatureMemory _memory;
    Biology _bio;
    NPCNavigation _nav;
    IHaveReach _reach;
    IHarvest _harvest;
    IAttack _attack;
    Transform curTarget;
    public bool isThirsty;
    CreatureDrink _drink;
    [SerializeField]
    LayerMask mask;
    public BiomeMemory CurBiome
    {
        set { _memory.CurBiome = value; CheckBiome(value); }
    }
    private void Awake()
    {
        _bio = GetComponent<Biology>();
        _harvest = GetComponent<IHarvest>();
        _attack = GetComponent<IAttack>();
        _memory = GetComponent<CreatureMemory>();
        _nav = GetComponent<NPCNavigation>();
        _reach = GetComponent<IHaveReach>();
        _drink = GetComponent<CreatureDrink>();
    }
    private void Update()
    {
        CheckStatus();
    }
    public void CheckBiome(BiomeMemory _biome)
    {

        IHavePrefTemperature _temperatureStats = GetComponent<IHavePrefTemperature>();

        
        CreatureEvaluateBiome _evBiome = new CreatureEvaluateBiome();
        
        if(_temperatureStats != null)
        {
            if (_evBiome.BiomeHasSuitableTemperature(_temperatureStats.PrefTemp, _temperatureStats.TempDifTolerance, _biome.temperature))
            {
                Debug.Log("CorrectTemp");
                IHaveFood _foodStat = GetComponent<IHaveFood>();
                IHaveWater _waterStat = GetComponent<IHaveWater>();
                if (_foodStat != null)
                {
                    if (_evBiome.BiomeHasFood(_biome, _bio, _foodStat.MaxFood, _waterStat.MaxWater))
                    {
                        //Biome has food
                        Debug.Log("BiomeHasFood");
                        _memory.isMigrating = false;
                        _memory.isWandering = true;
                    }
                    else
                    {
                        ContinueMigrating(_biome, _temperatureStats);
                        Debug.Log("Biome Does not have food");
                    }
                }
                else
                {
                    Debug.Log("foodstats is null");
                }
               
            }
            else
            {
                ContinueMigrating(_biome, _temperatureStats);
                Debug.Log("WrongTemp");
            }
        }
    }
    void ContinueMigrating(BiomeMemory _biome, IHavePrefTemperature _tempStats)
    {
        BiomeCoordination _biomeCo = new BiomeCoordination();
        Vector3 nextBiome = _biomeCo.NextBiome(_biome, _tempStats, WorldEnvironmentData.instance.SunPositionZ(), transform.position.z, mask);
        _memory.isMigrating = true;
        _memory.isWandering = false;
        _nav.migrationDestination = nextBiome;
    }
    void CheckStatus()
    {
        if (isThirsty)
        {
            if (_memory.curWaterSource == null)
            {
                isGettingWater = false;
                LookForWater();
                
            }
            else
            {
                if (!isGettingWater)
                {
                    isGettingWater = true;
                    _nav.SetFollowTarget(_memory.curWaterSource);

                }
            }
        }
        else
        {
            isGettingWater = false;
            if(_nav.followTarget == _memory.curWaterSource)
            {
                _nav.followTarget = null;
            }
        }
        if (isHungry && !isGettingWater)
        {
            if (_memory.curTargetPrey == null)
            {
                isHunting = false;
                LookForFood();
            }
            else
            {
                if (!isHunting)
                {
                    isHunting = true;
                    curTarget = _memory.curTargetPrey;
                    _nav.SetFollowTarget(_memory.curTargetPrey);
                }                       
            }
        }
        else
        {
            isHunting = false;
            if(_nav.followTarget == _memory.curTargetPrey)
            {
                _nav.followTarget = null;
            }
        }
        if (isGettingWater)
        {
            GetWater();
        }
        else if (isHunting)
        {
            Hunt();
        }
    }
    void GetWater()
    {
        if (_reach != null)
        {
            if (_nav.agent.remainingDistance <= _reach.Reach)
            {
                if(_drink != null)
                {
                    IHaveMass _water = _memory.curWaterSource.GetComponent<IHaveMass>();
                    if(_water != null)
                    {
                        _drink.DrinkWater(_water);
                    }
                }
            }
        }
    }

    void Hunt()
    {
        if(_reach != null)
        {
            if (_nav.agent.remainingDistance <= _reach.Reach)
            {
                if (curTarget != null)
                {
                    IHaveHealth _health = curTarget.GetComponent<IHaveHealth>();
                    if (_health != null)
                    {
                        if (_health.Health > 0)
                        {
                            if (_attack != null)
                            {
                                _attack.AttackTarget(curTarget);
                            }
                        }
                        else
                        {
                            if (_harvest != null)
                            {
                                _harvest.HarvestTarget(curTarget);
                            }
                        }
                    }
                    else if (curTarget.GetComponent<IAmPlant>() != null)
                    {
                        //Harvest
                        
                        if(_harvest != null)
                        {
                            _harvest.HarvestTarget(curTarget);
                        }

                    }
                }
                else
                {
                    isHunting = false;
                }
            }
        }
        
    }
    void LookForFood()
    {
        if (_memory.primaryPotentialPrey.Count > 0)
        {
            float shortestDist = 9999;
            Transform closestPrey = null;
            List<Transform> _preyList = _memory.primaryPotentialPrey;
            foreach (Transform _prey in _preyList)
            {
                if (_prey != null)
                {
                    float dist = Vector3.Distance(transform.position, _prey.position);
                    if (dist < shortestDist)
                    {
                        closestPrey = _prey;
                        shortestDist = dist;
                    }
                }
            }
            if (closestPrey != null)
            {
                _memory.curTargetPrey = closestPrey;
            }
        }
        else if(_memory.secondaryPotentialPrey.Count > 0)
        {
            float shortestDist = 9999;
            Transform closestPrey = null;
            List<Transform> _preyList = _memory.secondaryPotentialPrey;
            foreach (Transform _prey in _preyList)
            {
                if (_prey != null)
                {
                    float dist = Vector3.Distance(transform.position, _prey.position);
                    if (dist < shortestDist)
                    {
                        closestPrey = _prey;
                        shortestDist = dist;
                    }
                }
            }
            if (closestPrey != null)
            {
                _memory.curTargetPrey = closestPrey;
            }
            else
            {
                _memory.curTargetPrey = null;
            }
        }
        
    }
    void LookForWater()
    {
        if (_memory.waterSources.Count > 0)
        {
            float shortestDist = 9999;
            Transform closestWater = null;
            List<Transform> _waterList = _memory.waterSources;
            foreach (Transform _water in _waterList)
            {
                if (_water != null)
                {
                    float dist = Vector3.Distance(transform.position, _water.position);
                    if (dist < shortestDist)
                    {
                        closestWater = _water;
                        shortestDist = dist;
                    }
                }
            }
            if (closestWater != null)
            {
                _memory.curWaterSource = closestWater;
            }
            else
            {
                _memory.curWaterSource = null;
            }
        }     
    }
}
