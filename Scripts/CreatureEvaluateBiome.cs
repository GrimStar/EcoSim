using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEvaluateBiome
{


    public bool BiomeHasFood(BiomeMemory _biome, Biology _bio, float dailyFoodConsumption, float dailyWaterConsumption)
    {
        if (_bio.dietType == Biology.DietType.Carnivore)
        {
            if (dailyFoodConsumption < (_biome.meatMass - _biome.meatConsumptionMass) && dailyWaterConsumption < (_biome.waterMass - _biome.waterConsumption))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (dailyFoodConsumption < (_biome.foliageMass - _biome.foliageConsumptionMass) && dailyWaterConsumption < (_biome.waterMass - _biome.waterConsumption))
            {
                return true;
            }
            else
            {
                return false;
            }
        }       
    }
    public bool BiomeHasSuitableTemperature(float prefTemp, float tempDifTolerance, float biomeTemp)
    {
        float tempDifference = Mathf.Abs(biomeTemp - prefTemp);
        if(tempDifference < tempDifTolerance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

}
