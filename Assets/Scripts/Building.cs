using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuildingPlacement
{
    Valid,
    Invalid,
    Fixed
};
public class Building
{
    private BuildingPlacement _placement;
    private List<Material> _materials;
    private BuildingManager _buildingManager;
    private Transform _transform;
    private int _currentHealth;
    private BuildingData _data;

    

    public string Code { get => _data.code; }
    public Transform Transform { get => _transform; }
    public int HP { get => _currentHealth; set => _currentHealth = value; }
    public int MaxHP { get => _data.healthPoints; }

    public int DataIndex
    {
        get
        {
            for (int i = 0; i < Globals.BUILDING_DATA.Length; i++)
            {
                if (Globals.BUILDING_DATA[i].code == _data.code)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public bool HasValidPlacement
    {
        get => _placement == BuildingPlacement.Valid;
    }
    public bool IsFixed { get => _placement == BuildingPlacement.Fixed; }

    public Building(BuildingData data)
    {
       
        _data = data;
        _currentHealth = data.healthPoints;
        
        GameObject g = GameObject.Instantiate(data.buildingPrefab);
        _transform = g.transform;
       
        _materials = new List<Material>();

        foreach (Material mat in _transform.Find("Mesh").GetComponent<Renderer>().materials)
        {
            _materials.Add(new Material(mat));
        }
        _placement = BuildingPlacement.Valid;
        _buildingManager = g.GetComponent<BuildingManager>();
        
        SetMaterials();
        
    }


    public void SetMaterials()
    {
        SetMaterials(_placement);
    }

    public void SetMaterials(BuildingPlacement placement)
    {
        List<Material> materials;
        if (placement == BuildingPlacement.Valid)
        {
            Material refMaterial = (Material)Resources.Load("Materials/Valid");
            materials = new List<Material>();
            for (int i = 0; i < _materials.Count; i++)
            {
                materials.Add(refMaterial);
            }
        }
        else if (placement == BuildingPlacement.Invalid)
        {
            Material refMaterial = (Material)Resources.Load("Materials/Invalid");
            materials = new List<Material>();
            for (int i = 0; i < _materials.Count; i++)
            {
                materials.Add(refMaterial);
            }
        }
        else if (placement == BuildingPlacement.Fixed)
        {
            materials = _materials;
        }
        else
        {
            return;
        }
        _transform.Find("Mesh").GetComponent<Renderer>().materials = materials.ToArray();
    }

    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

   public void Place()
    {
        _placement = BuildingPlacement.Fixed;
        _transform.GetComponent<BoxCollider>().isTrigger = false;
        SetMaterials();

        foreach (ResourceValue resource in _data.cost)
        {
            Globals.GAME_RESOURCES[resource.code].AddAmount(-resource.amount);
        }
    }

    public bool CanBuy()
    {
        return _data.CanBuy();
    }
    public void CheckValidPlacement()
    {
        if (_placement == BuildingPlacement.Fixed) return;

        _placement = _buildingManager.CheckPlacement()
            ? BuildingPlacement.Valid : BuildingPlacement.Invalid;
    }
}
