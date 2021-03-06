using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    private bool _isDraggingMouseBox = false;
    private Vector3 _dragStartPosition;

    private Ray _ray;
    private RaycastHit _raycastHit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDraggingMouseBox = true;
            _dragStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
            _isDraggingMouseBox = false;

        if (_isDraggingMouseBox && _dragStartPosition!=Input.mousePosition)
        {
            SelectUnitsInDraggingBox();
        }

        if (Globals.SELECTED_UNITS.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DeselectAllUnits();
            }
            if (Input.GetMouseButtonDown(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _raycastHit, 1000f))
                {
                    if (_raycastHit.transform.CompareTag("Terrain"))
                    {
                        DeselectAllUnits();
                    }
                }
            }
        }
    }

    private void DeselectAllUnits()
    {
        List<UnitManager> selectedUnits = new List<UnitManager>(Globals.SELECTED_UNITS);
        foreach (UnitManager unitManager in selectedUnits)
        {
            unitManager.Deselect();
        }
    }

    private void SelectUnitsInDraggingBox()
    {
        Bounds selectionBounds = Utils.GetViewportBounds
            (Camera.main, 
            _dragStartPosition,
            Input.mousePosition);
        GameObject[] selectedUnits = GameObject.FindGameObjectsWithTag("Unit");
        bool inBounds;
        foreach ( GameObject unit in selectedUnits)
        {
            inBounds = selectionBounds.Contains(
                Camera.main.WorldToViewportPoint(unit.transform.position));
            if (inBounds)
            {
                unit.GetComponent<UnitManager>().Select();
            }
            else
            {
                unit.GetComponent<UnitManager>().Deselect();
            }
        }
       
    }

    void OnGUI()
    {
        if (_isDraggingMouseBox)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(_dragStartPosition, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.5f, 1f, 0.4f, 0.2f));
            Utils.DrawScreenRectBorder(rect, 1, new Color(0.5f, 1f, 0.4f));
        }
    }

}
