using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeCoordination
{


    public Vector3 NextBiome(BiomeMemory _biome, IHavePrefTemperature _temperatureStats, float sunZpos, float creatureZpos, LayerMask mask)
    {
        Vector3 sunDir;
        float tempDir = 0;
        if(_biome.temperature > _temperatureStats.PrefTemp)
        {
            tempDir = -1;
        }
        else
        {
            tempDir = 1;
        }
        if(sunZpos > creatureZpos)
        {
            sunDir = Vector3.forward;
        }
        else
        {
            sunDir = Vector3.back;
        }
        sunDir.z *= tempDir;

        //Need to check if any connected biomes are == sunDir
        BiomeDataStruct _biomeData = _biome._biomeData;
        int i = 0;
        Vector3 horizontalBiome = Vector3.zero;
        Vector3 verticalBiome = Vector3.zero;
        bool nextBiomeVert = false;
        foreach (Vector3 _biomeDir in _biomeData.ConnectedBiomes)
        {
            if(_biomeDir == sunDir)
            {
                verticalBiome = _biomeDir;
                nextBiomeVert = true;
            }
            if(_biomeDir == Vector3.right || _biomeDir == -Vector3.right)
            {
                horizontalBiome = _biomeDir;
            }
            i++;
        }
        Vector3 direction = Vector3.zero;
        if (nextBiomeVert)
        {
            direction = verticalBiome;
        }
        else
        {
            direction = horizontalBiome;
        }

        float distance = _biomeData.BiomeWidth.x;
        Vector3 nextBiomePosition = _biomeData.Position + (distance * direction);
        nextBiomePosition.y += 200;
        RaycastHit hit;
        if(Physics.Raycast(nextBiomePosition, Vector3.down, out hit, 500, mask))
        {
            nextBiomePosition = hit.point;
        }
        FindDestination _findDestination = new FindDestination();

        Vector3 nextPosition = _findDestination.FindDestinationWithinRadius(nextBiomePosition, 30);
        return nextPosition;
    }
   

}
