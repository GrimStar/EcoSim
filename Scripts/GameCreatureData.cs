using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCreatureData : MonoBehaviour
{
    public static GameCreatureData instance;

    
    public float carnivoreFoodAlert;
    public float carnivoreWaterAlert;
    public float carnivoreStaminaAlert;
    public float carnivoreHealthAlert;
    public float carnivoreTorpidityAlert;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
