using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Building",menuName ="Scriptable Objects/Building",order =1)]
public class BuildingData: ScriptableObject
{
    public int healthPoints;
    public string code;
    public string unitName;
    public string description;
    public GameObject buildingPrefab;
    public List<ResourceValue> cost;

    public bool CanBuy()
    {
        foreach (ResourceValue resource in cost)
        {
            if (Globals.GAME_RESOURCES[resource.code].Amount < resource.amount)
            {
                return false;
            }
        }
        return true;
    }

}
