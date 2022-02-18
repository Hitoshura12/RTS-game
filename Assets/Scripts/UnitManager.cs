using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject selectionCircle;

    private Transform canvas;
    private GameObject healthBar;

    private bool _hovered = false;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
    }

    private void Update()
    {
        if (_hovered && Input.GetMouseButtonDown(0) && IsActive())
        {
            Select(true,Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift));
        }
    }
    private void OnMouseEnter()
    {
        _hovered = true;
    }

    private void OnMouseExit()
    {
        _hovered = false;
    }
    protected virtual bool IsActive()
    {
        return true;
    }

    public void Select()
    {
        Select(false,false);
    }

    public void Select(bool singleClick, bool holdingShift)
    {
        //Selection Box
        if (!singleClick)
        {
            SelectUtils();
            return;
        }
        //Check for shift
        if (!holdingShift)
        {
            List<UnitManager> selectedUnits = new List<UnitManager>(Globals.SELECTED_UNITS);
            foreach (UnitManager unitManager in selectedUnits)
                unitManager.Deselect();
            SelectUtils();
        }
        else
        {
            if (!Globals.SELECTED_UNITS.Contains(this))
                SelectUtils();
            else
                Deselect();

        }
        
    }

    private void SelectUtils()
    {
        //if (Globals.SELECTED_UNITS.Contains(this)) return;
        Globals.SELECTED_UNITS.Add(this);
        selectionCircle.SetActive(true);
        if (healthBar==null)
        {
            healthBar = GameObject.Instantiate(Resources.Load("Prefabs/UI/HealthBar")) as GameObject;
            healthBar.transform.SetParent(canvas);
            HealthBar h = healthBar.GetComponent<HealthBar>();
            h.Initialize(transform);
            h.SetPosition();
        }
    }

    public void Deselect()
    {
       //if (!Globals.SELECTED_UNITS.Contains(this)) return;
        Globals.SELECTED_UNITS.Remove(this);
        if (selectionCircle!=null)
        {
            selectionCircle.SetActive(false);
        } 
        Destroy(healthBar);
        healthBar = null;
       
    }
}
