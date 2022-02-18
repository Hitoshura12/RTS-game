using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomEvent : UnityEvent<CustomEventData> { }

public class CustomEventData : MonoBehaviour
{
    private BuildingData buildingData;

    public CustomEventData(BuildingData buildingData)
    {
        this.buildingData = buildingData;
    }
}
