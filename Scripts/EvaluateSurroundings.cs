using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateSurroundings : MonoBehaviour
{
    [SerializeField]
    CreatureMemory _memory;
    private Biology myBiology;

    // Start is called before the first frame update
    void Start()
    {
        _memory = transform.parent.GetComponent<CreatureMemory>();
        myBiology = transform.parent.GetComponent<Biology>();            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckDetectedCreature(Transform _creature, bool _add)
    {
        if(_creature.gameObject == transform.root.gameObject)
        {
            return;
        }
        IAmCreature _ICreature = transform.parent.GetComponent<IAmCreature>();
        if(_ICreature != null)
        {
      
            if(myBiology != null)
            {
                //NEED TO CHECK FOODCHAIN RANK HERE
                if(myBiology.dietType == Biology.DietType.Carnivore)
                {
                    CarnivoreCreatureCheck(_creature, _add);
                }
                else if (myBiology.dietType == Biology.DietType.Herbivore)
                {
                    IAmPlant plantType = _creature.GetComponent<IAmPlant>();
                    if (plantType != null)
                    {
                        IPrefPlantType myPrefPlant = transform.parent.GetComponent<IPrefPlantType>();
                        if (myPrefPlant != null)
                        {
                            if (myPrefPlant.PrefPlant == plantType.PlantType)
                            {
                                if (_add)
                                {
                                    //Add To Primary List
                                    _memory.primaryPotentialPrey.Add(_creature);
                                }
                                else
                                {
                                    _memory.primaryPotentialPrey.Remove(_creature);
                                    if (_memory.lastSeenPreyLocations.Count < _memory.maxPreyLocations)
                                    {
                                        _memory.lastSeenPreyLocations.Add(_creature.position);
                                    }
                                    else
                                    {
                                        _memory.lastSeenPreyLocations.RemoveAt(0);
                                        _memory.lastSeenPreyLocations.Add(_creature.position);
                                    }
                                }

                            }
                            else
                            {
                                if (_add)
                                {
                                    //Add To Secondary List
                                    _memory.secondaryPotentialPrey.Add(_creature);
                                }
                                else
                                {
                                    _memory.secondaryPotentialPrey.Remove(_creature);
                                    if (_memory.lastSeenPreyLocations.Count < _memory.maxPreyLocations)
                                    {
                                        _memory.lastSeenPreyLocations.Add(_creature.position);
                                    }
                                    else
                                    {
                                        _memory.lastSeenPreyLocations.RemoveAt(0);
                                        _memory.lastSeenPreyLocations.Add(_creature.position);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
    }
    void CarnivoreCreatureCheck(Transform _creature, bool _add)
    {
        IAmMeat meatType = _creature.GetComponent<IAmMeat>();
        if (meatType != null)
        {
            IPrefMeatType myPrefMeat = transform.parent.GetComponent<IPrefMeatType>();
            if (myPrefMeat != null)
            {
                if (myPrefMeat.PrefMeat == meatType.MeatType)
                {
                    List<Transform> _list = _memory.primaryPotentialPrey;
                    if (_add)
                    {

                        AddToList(_list, _creature);
                    }
                    else
                    {
                        RemoveFromList(_list, _creature);
                    }

                }
                else
                {
                    List<Transform> _list = _memory.secondaryPotentialPrey;
                    if (_add)
                    {
                        AddToList(_list, _creature);
                    }
                    else
                    {
                        RemoveFromList(_list, _creature);
                    }
                }
            }
        }
    }
    void AddToList(List<Transform> _list, Transform _creature)
    {
        if (!_list.Contains(_creature))
        {
            _list.Add(_creature);
        }
        
    }
    void RemoveFromList(List<Transform> _list, Transform _creature)
    {
        if (_list.Contains(_creature))
        {
            _list.Remove(_creature);
            if (_memory.lastSeenPreyLocations.Count < _memory.maxPreyLocations)
            {
                _memory.lastSeenPreyLocations.Add(_creature.position);
            }
            else
            {
                _memory.lastSeenPreyLocations.RemoveAt(0);
                _memory.lastSeenPreyLocations.Add(_creature.position);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.GetComponent<IAmWater>() != null)
        {
            RemoveFromList(_memory.waterSources, other.transform);
        }
        else if(other.GetComponent<IAmCreature>() != null)
        {
            CheckDetectedCreature(other.transform, true);
       
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        CheckDetectedCreature(other.transform, false);
        if (other.GetComponent<IAmWater>() != null)
        {
            if (_memory != null)
            {
                if (!_memory.waterSources.Contains(other.transform))
                {
                    _memory.waterSources.Add(other.transform);
                }
            }
        }
        else if (other.GetComponent<IAmCreature>() != null)
        {
            CheckDetectedCreature(other.transform, false);
        
        }
    }
}
