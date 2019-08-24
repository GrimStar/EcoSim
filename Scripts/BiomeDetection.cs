using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeDetection : MonoBehaviour
{
    BiomeMemory _memory;
    // Start is called before the first frame update
    void Start()
    {
        _memory = GetComponent<BiomeMemory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        float massAmount = 0;
        IHaveMass _mass = other.GetComponent<IHaveMass>();
        IHaveWater _water = other.GetComponent<IHaveWater>();
        if (_mass != null)
        {
            massAmount = _mass.Mass;
        }
        IAmPlant _plant = other.GetComponent<IAmPlant>();
        if (_plant != null)
        {
            other.GetComponent<CreatureMemory_Other>().CurBiome = _memory;
            
            if (_plant.PlantType == "Foliage")
            {

                _memory.foliageMass += massAmount;

            }
            else
            {
                _memory.fruitMass += massAmount;
            }
        }
        else if (other.GetComponent<IAmTree>() != null)
        {
            _memory.treeCount += 1;
        }
        else if (other.GetComponent<IAmCreature>() != null)
        {
            Biology _bio = other.GetComponent<Biology>();
            if (_bio != null)
            {
                if (_bio.dietType == Biology.DietType.Carnivore)
                {
                    _memory.meatConsumptionMass += other.GetComponent<IHaveFood>().MaxFood;
                    _memory.carnivoreList.Add(other.gameObject);
                    _memory.carnivoreCount += 1;
                    _memory.meatMass += massAmount;
                    other.GetComponent<Brain>().CurBiome = _memory;
                }
                else if (_bio.dietType == Biology.DietType.Herbivore)
                {
                    _memory.foliageConsumptionMass += other.GetComponent<IHaveFood>().MaxFood;
                    _memory.herbivoreList.Add(other.gameObject);
                    _memory.herbivoreCount += 1;
                    _memory.meatMass += massAmount;
                    other.GetComponent<Brain>().CurBiome = _memory;
                }
                _memory.waterConsumption += _water.MaxWater;
            }

        }
        else if (other.GetComponent<IAmWater>() != null)
        {
            _memory.waterMass += massAmount;
            other.GetComponent<CreatureMemory_Other>().CurBiome = _memory;
        }
        else if(other.GetComponent<IAmTile>() != null)
        {
            IAmTile _tiledata = other.GetComponent<IAmTile>();
            _tiledata.CurBiome = _memory;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        float massAmount = 0;
        IHaveMass _mass = other.GetComponent<IHaveMass>();
        IHaveWater _water = other.GetComponent<IHaveWater>();
        if (_mass != null)
        {
            massAmount = _mass.Mass;
        }
        IAmPlant _plant = other.GetComponent<IAmPlant>();
        if (_plant != null)
        {

            if (_mass != null)
            {
                massAmount = _mass.Mass;
            }
            if (_plant.PlantType == "Foliage")
            {

                _memory.foliageMass -= massAmount;

            }
            else
            {
                _memory.fruitMass -= massAmount;
            }
        }
        else if (other.GetComponent<IAmTree>() != null)
        {
            _memory.treeCount -= 1;
        }
        else if (other.GetComponent<IAmCreature>() != null)
        {
            Biology _bio = other.GetComponent<Biology>();
            if (_bio != null)
            {
                if (_bio.dietType == Biology.DietType.Carnivore)
                {
                    _memory.meatConsumptionMass -= other.GetComponent<IHaveFood>().MaxFood;
                    _memory.carnivoreList.Remove(other.gameObject);
                    _memory.carnivoreCount -= 1;
                    _memory.meatMass -= massAmount;
 
                }
                else if (_bio.dietType == Biology.DietType.Herbivore)
                {
                    _memory.foliageConsumptionMass -= other.GetComponent<IHaveFood>().MaxFood;
                    _memory.herbivoreList.Remove(other.gameObject);
                    _memory.herbivoreCount -= 1;
                    _memory.meatMass -= massAmount;
       
                }
                _memory.waterConsumption -= _water.MaxWater;
            }

        }
        else if (other.GetComponent<IAmWater>() != null)
        {
            _memory.waterMass -= massAmount;
        }
    }



}
