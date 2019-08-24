using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeEnvironment : MonoBehaviour
{

    [SerializeField]
    Material[] groundMats;
    float temperature;
    BiomeMemory _memory;
    [SerializeField]
    Transform ground;
    Renderer rend;
    bool isFreezing = false;
    bool isCold = false;
    bool isWarm = false;
    bool isHot = false;
    bool isHottest = false;
    // Start is called before the first frame update
    void Start()
    {
        _memory = GetComponent<BiomeMemory>();
        rend = ground.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        temperature = _memory.temperature;
        if(temperature < 0)
        {
            isCold = false;
            isWarm = false;
            isHot = false;
            isHottest = false;
            if (!isFreezing)
            {
                isFreezing = true;
                rend.material = groundMats[0];
            }

        }
        else if(temperature < 10)
        {
            isFreezing = false;
            isWarm = false;
            isHot = false;
            isHottest = false;
            if (!isCold)
            {
                isCold = true;
                rend.material = groundMats[1];
            }
        }
        else if(temperature < 20)
        {
            isCold = false;
            isHot = false;
            isHottest = false;
            isFreezing = false;
            if (!isWarm)
            {
                isWarm = true;
                rend.material = groundMats[2];
            }
        }
        else if(temperature < 30)
        {
            isCold = false;
            isWarm = false;
            isFreezing = false;
            isHottest = false;
            if (!isHot)
            {
                isHot = true;
                rend.material = groundMats[3];
            }
        }
        else
        {
            isCold = false;
            isWarm = false;
            isHot = false;
            isFreezing = false;
            if (!isHottest)
            {
                isHottest = true;
                rend.material = groundMats[4];
            }
        }
    }
}
