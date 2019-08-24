using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CameraMotor_test))]
public class CameraFollow_motortest : MonoBehaviour
{
    private CameraMotor_test _motor;

    private StatsDisplay _statsUI;
    private void Awake()
    {
        _statsUI = GetComponent<StatsDisplay>();
        _motor = GetComponent<CameraMotor_test>();
    }
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickOnObject();
            }          
        }
    }
    void ClickOnObject()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            IAmCreature _creature = hit.transform.GetComponent<IAmCreature>();
            if (_creature != null)
            {
                CreatureStatus _stats = hit.transform.GetComponent<CreatureStatus>();
                BiomeMemory _curBiome = hit.transform.GetComponent<CreatureMemory>().CurBiome;
                IHavePrefTemperature _tempStats = hit.transform.GetComponent<IHavePrefTemperature>();
                
                if (_stats != null)
                {
                    _statsUI.UpdateStatUI(_stats, _curBiome, _tempStats);
                }
                _motor.SetFollowTarget(hit.transform);
            }

        }
    }
}
