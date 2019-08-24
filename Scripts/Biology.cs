using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biology : MonoBehaviour, IAmCreature
{
    public TypeOfCreature _typeOfCreature;
    [SerializeField]
    int foodChainRank;
    [SerializeField]
    int diet;
    [SerializeField]
    int habitat;
    [SerializeField]
    int type;
    public enum DietType
    {
        Herbivore,
        Carnivore,
        Omnivore,
        Insectivore,
        Plant
            
    }
    public DietType dietType;

    // Start is called before the first frame update
    void Start()
    {
        _typeOfCreature.FoodChainRank = foodChainRank;
        _typeOfCreature.Diet = diet;
        _typeOfCreature.Type = type;
        _typeOfCreature.Habitat = habitat;
    }
    

   
}
