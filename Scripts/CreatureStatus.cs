using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStatus : MonoBehaviour, IHaveWater, IHaveHealth, IHaveStrength, IHaveMass, IHaveBiteSize, IHaveReach, IHaveDrinkSize, IHaveFood
{

    
    StatChangeRates _statRates;
    CreatureStatAlerts _creatureStatAlerts;
    CreatureAlerts _statAlerts;
    public CreatureStats _creatureStats;
    CreatureStatistics _creatureStatistics;
    public CreatureStats _creatureLiveStats;
    CreatureMemory _memory;
    Brain _brain;
    private bool setup = false;
    public bool dead = false;
    [SerializeField]
    float drinkSize;
    public float curSpeed;
    public float MaxWater { get { return _creatureStatistics.maxWater; } }
    public float Stamina { get { return _creatureLiveStats.Stamina; } }
    public float MaxFood { get { return _creatureStatistics.maxFood; } }
    public float Food { get { return _creatureLiveStats.Food; } set { _creatureLiveStats.Food = value; CheckBiome(); } }
    public float Energy { get { return _creatureLiveStats.Energy; } }
    public float DrinkSize { get { return drinkSize; } }
    public float Reach { get { return _creatureLiveStats.AttackReach; } }
    public float Strength { get { return _creatureLiveStats.Strength; } }
    public float BiteSize { get { return _creatureLiveStats.BiteSize; } }
    public float Water
    {
        get { return _creatureLiveStats.Water; }
        set { float amountRemoved = value - _creatureLiveStats.Water; _memory.CurBiome.waterMass -= amountRemoved; _creatureLiveStats.Water = value;  CheckBiome(); }
    }  
    public float Health
    {
        get
        {
            return _creatureLiveStats.Health;
        }
        set
        {
            _creatureLiveStats.Health = value;
            if (value <= 0)
            {
                UpdateBiomeStats();               
            }
        }
    }   
    public float Mass
    {
        get { return _creatureLiveStats.Mass; }
        set
        {
            float amountRemoved = _creatureLiveStats.Mass - value;
            if(GetComponent<IAmMeat>() != null)
            {
                _memory.CurBiome.meatMass -= amountRemoved;
            }
            else if(GetComponent<IAmPlant>() != null)
            {
                _memory.CurBiome.foliageMass -= amountRemoved;
            }
            _creatureLiveStats.Mass = value;
            if(value <= 0)
            {
                SelfDestruct();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _memory = GetComponent<CreatureMemory>();
        _statRates = GetComponent<StatChangeRates>();
        _brain = GetComponent<Brain>();
        _creatureStatistics = GetComponent<CreatureStatistics>();
        if (_creatureStatistics != null)
        {
            _creatureStats.Health = _creatureStatistics.maxHealth;
            _creatureStats.Stamina = _creatureStatistics.maxStamina;
            _creatureStats.Energy = _creatureStatistics.maxEnergy;
            _creatureStats.Food = _creatureStatistics.maxFood;
            _creatureStats.Water = _creatureStatistics.maxWater;
            _creatureStats.Strength = _creatureStatistics.maxStrength;
            _creatureStats.Fortitude = _creatureStatistics.maxFortitude;
            _creatureStats.Torpidity = _creatureStatistics.maxTorpidity;
            _creatureStats.AttackRate = _creatureStatistics.attackRate;
            _creatureStats.AttackReach = _creatureStatistics.attackReach;
            _creatureStats.Mass = _creatureStatistics.maxMass;
            _creatureStats.BiteSize = _creatureStatistics.biteSize;
        }
        _statAlerts = GetComponent<CreatureAlerts>();
       
        _creatureLiveStats = _creatureStats;
    }
    void UpdateBiomeStats()
    {
        _creatureLiveStats.Health = 0;
        dead = true;
        if (_memory.CurBiome != null)
        {
            Biology _bio = GetComponent<Biology>();
            if (_bio != null)
            {
                float _food = GetComponent<IHaveFood>().MaxFood;
                if (_bio.dietType == Biology.DietType.Herbivore)
                {
                    _memory.CurBiome.herbivoreCount -= 1;
                    _memory.CurBiome.foliageConsumptionMass -= _food;
                }
                else if (_bio.dietType == Biology.DietType.Carnivore)
                {
                    _memory.CurBiome.carnivoreCount -= 1;
                    _memory.CurBiome.meatConsumptionMass -= _food;
                }
            }
        }
    }
    private void Update()
    {
        if (!setup)
        {
            setup = true;
            _creatureStatAlerts = _statAlerts._statAlerts;
        }
        StaminaConsumption();
        FoodConsumption();
        StaminaRegeneration();
        
    }
    void CheckBiome()
    {
        if (Food >= MaxFood || Water >= MaxWater)
        {
            _brain.CheckBiome(_memory.CurBiome);
        }
    }
    void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
    public void EnergyConsumption()
    {
        if(_creatureLiveStats.Energy > 0)
        {
            _creatureLiveStats.Energy -= _statRates.energyConsumption * Time.deltaTime;
        }
    }
    public void EnergyRegeneration()
    {
        _creatureLiveStats.Energy += _statRates.energyRegeneration * Time.deltaTime;
    }
    public void FoodConsumption()
    {
        if (_creatureLiveStats.Energy < _creatureStats.Energy)
        {
            if (_creatureLiveStats.Food > 0)
            {
                _creatureLiveStats.Food -= _statRates.foodConsumption * Time.deltaTime;

            }
            
            EnergyRegeneration();
            if (_creatureLiveStats.Food < _creatureStatAlerts.FoodAlert)
            {
                _brain.isHungry = true;
            }
            else if(_creatureLiveStats.Food >= _creatureStats.Food)
            {
                _brain.isHungry = false;
            }
        }
        if(_creatureLiveStats.Food <= 0)
        {
            if (Health > 0)
            {
                Health -= _statRates.healthLoss * Time.deltaTime;
            }
        }
    }
    public void StaminaConsumption()
    {
        if (_creatureLiveStats.Stamina > 0)
        {
            _creatureLiveStats.Stamina -= _statRates.staminaConsumption * curSpeed * Time.deltaTime;
        }
    }
    public void StaminaRegeneration()
    {
        if (_creatureLiveStats.Stamina < _creatureStats.Stamina)
        {
            if (_creatureLiveStats.Energy > 0)
            {
                _creatureLiveStats.Stamina += _statRates.staminaRegeneration * Time.deltaTime;
                EnergyConsumption();
                
            }
            WaterConsumption();
        }
    }
    public void WaterConsumption()
    {
        if (_creatureLiveStats.Water > 0)
        {
            _creatureLiveStats.Water -= _statRates.waterConsumption * Time.deltaTime;           
        }
        if (_creatureLiveStats.Water < _creatureStatAlerts.WaterAlert)
        {
            if (!_brain.isThirsty)
            {
                _brain.isThirsty = true;
            }
        }
        else if(Water >= MaxWater)
        { 
            if (_brain.isThirsty)
            {
                _brain.isThirsty = false;
            }
        }
    }   
}
