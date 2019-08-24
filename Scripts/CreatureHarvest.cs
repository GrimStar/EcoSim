using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHarvest : MonoBehaviour, IHarvest
{
    bool isHarvesting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HarvestTarget(Transform target)
    {
        if (!isHarvesting)
        {
            isHarvesting = true;
            StartCoroutine(AttackCooldown());
            IHaveMass _mass = target.GetComponent<IHaveMass>();
            IHaveBiteSize _bite = GetComponent<IHaveBiteSize>();
            if (_mass != null)
            {
                if (_bite != null)
                {
                    if (_mass.Mass - _bite.BiteSize > 0)
                    {
                        _mass.Mass -= _bite.BiteSize;
                        IConsume _consumeFood = GetComponent<IConsume>();
                        if (_consumeFood != null)
                        {
                            _consumeFood.IAddFood(_bite.BiteSize);
                        }
                    }
                    else
                    {
                        IConsume _consumeFood = GetComponent<IConsume>();
                        if (_consumeFood != null)
                        {
                            _consumeFood.IAddFood(_mass.Mass);
                            EvaluateSurroundings _surroundings = GetComponent<EvaluateSurroundings>();
                            if(_surroundings != null)
                            {
                                _surroundings.CheckDetectedCreature(target, false);
                            }
                        }
                        _mass.Mass = 0;
                    }
                }
            }
        }
       
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1);
        isHarvesting = false;
    }
}
