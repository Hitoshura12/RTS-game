using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestListener : MonoBehaviour
{
    private void OnTest()
    {
        Debug.Log("Testing the 'Test' event");
    }
    private void OnEnable()
    {
        EventManager.AddListener("Test", OnTest);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("Test", OnTest);
    }
}
