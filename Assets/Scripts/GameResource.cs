using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource
{
    private string resourceName;
    private int currentAmount;

    public GameResource(string resourceName, int currentAmount)
    {
        this.resourceName = resourceName;
        this.currentAmount = currentAmount;
    }

    public void AddAmount(int value)
    {
        currentAmount += value;
        if (currentAmount<0)
            currentAmount = 0;

    }
    public int Amount { get => currentAmount; }
    public string ResourceName { get => resourceName; }

   
}
