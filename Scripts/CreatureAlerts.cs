using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAlerts : MonoBehaviour
{

    public float carnivoreFoodAlert;
    public float carnivoreWaterAlert;
    public float carnivoreStaminaAlert;
    public float carnivoreHealthAlert;
    public float carnivoreTorpidityAlert;

    public CreatureStatAlerts _statAlerts;
    // Start is called before the first frame update
    void Start()
    {
        _statAlerts.FoodAlert = carnivoreFoodAlert;
        _statAlerts.WaterAlert = carnivoreWaterAlert;
        _statAlerts.StaminaAlert = carnivoreStaminaAlert;
        _statAlerts.HealthAlert = carnivoreHealthAlert;
        _statAlerts.TorpidityAlert = carnivoreTorpidityAlert;
    }

   
}
