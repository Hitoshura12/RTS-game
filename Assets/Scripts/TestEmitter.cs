using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEmitter : MonoBehaviour
{
    private void Start()
    {
        EventManager.TriggerEvent("Test Event");
    }
}
