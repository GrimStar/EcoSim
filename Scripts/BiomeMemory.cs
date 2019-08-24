using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeMemory : MonoBehaviour, IAmBiome
{
    public int herbivoreCount;
    public int carnivoreCount;

    public float meatMass;
    public float fruitMass;
    public float foliageMass;

    public int treeCount;
    public float waterMass;
    public float Humidity;
    public float meatConsumptionMass;
    public float foliageConsumptionMass;
    public float fruitConsumptionMass;
    public float waterConsumption;
    public float temperature;
    public float UVAmount;
    public List<GameObject> carnivoreList = new List<GameObject>();
    public List<GameObject> herbivoreList = new List<GameObject>();

    public BiomeDataStruct _biomeData;

    public Vector3[] connectedBiomes;

    public enum PositionType
    {
        TopBorder,
        BottomBorder,
        LeftBorder,
        RightBorder,
        TopLeftCorner,
        TopRightCorner,
        BottomLeftCorner,
        BottomRightCorner,
        InsideBorder
    }
    public PositionType _positionType;

    private void Start()
    {
        Setup();
        _biomeData.Position = transform.position;
        _biomeData.BiomeWidth = transform.localScale;
        _biomeData.ConnectedBiomes = connectedBiomes;
    }
    void Setup()
    {
        if (_positionType == PositionType.TopBorder)
        {
            connectedBiomes = new Vector3[3];
            connectedBiomes[0] = Vector3.right;
            connectedBiomes[1] = Vector3.left;
            connectedBiomes[2] = Vector3.back;
        }
        else if (_positionType == PositionType.TopLeftCorner)
        {
            connectedBiomes = new Vector3[2];
            connectedBiomes[0] = Vector3.right;
            connectedBiomes[1] = Vector3.back;

        }
        else if(_positionType == PositionType.TopRightCorner)
        {
            connectedBiomes = new Vector3[2];
            connectedBiomes[0] = Vector3.left;
            connectedBiomes[1] = Vector3.back;
        }
        else if (_positionType == PositionType.LeftBorder)
        {
            connectedBiomes = new Vector3[3];
            connectedBiomes[0] = Vector3.right;
            connectedBiomes[1] = Vector3.back;
            connectedBiomes[2] = Vector3.forward;
        }
        else if (_positionType == PositionType.RightBorder)
        {
            connectedBiomes = new Vector3[3];
            connectedBiomes[0] = Vector3.left;
            connectedBiomes[1] = Vector3.back;
            connectedBiomes[2] = Vector3.forward;
        }
        else if (_positionType == PositionType.BottomLeftCorner)
        {
            connectedBiomes = new Vector3[2];
            connectedBiomes[0] = Vector3.right;
            connectedBiomes[1] = Vector3.forward;
        }
        else if (_positionType == PositionType.BottomRightCorner)
        {
            connectedBiomes = new Vector3[2];
            connectedBiomes[0] = Vector3.left;
            connectedBiomes[1] = Vector3.forward;
        }
        else if (_positionType == PositionType.BottomBorder)
        {
            connectedBiomes = new Vector3[3];
            connectedBiomes[0] = Vector3.left;
            connectedBiomes[1] = Vector3.right;
            connectedBiomes[2] = Vector3.forward;
        }
        else if (_positionType == PositionType.InsideBorder)
        {
            connectedBiomes = new Vector3[4];
            connectedBiomes[0] = Vector3.left;
            connectedBiomes[1] = Vector3.back;
            connectedBiomes[2] = Vector3.forward;
            connectedBiomes[3] = Vector3.right;
        }
    }
}
