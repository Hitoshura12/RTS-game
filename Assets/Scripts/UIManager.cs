using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{

    public Transform buildingMenu;
    public GameObject buildingButtonPrefab;

    public Transform resourcesMenu;
    public GameObject resourcesButtonPrefab;

    private Dictionary<string, TextMeshProUGUI> resourcesTexts;
    private Dictionary<string, Button> buildingButtons;

    private BuildingPlacer buildingPlacer;


    private void Awake()
    {
        //Resources menu
        resourcesTexts = new Dictionary<string, TextMeshProUGUI>();
        foreach (KeyValuePair<string,GameResource> pair in Globals.GAME_RESOURCES)
        {
            GameObject display = Instantiate(resourcesButtonPrefab);
            display.name = pair.Key;
            resourcesTexts[pair.Key] = display.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            SetResources(pair.Key, pair.Value.Amount);
            display.transform.SetParent(resourcesMenu);
        }
       
        //Building menu
        buildingPlacer = GetComponent<BuildingPlacer>();
        buildingButtons = new Dictionary<string, Button>();

        for (int i = 0; i < Globals.BUILDING_DATA.Length; i++)
        {
            BuildingData data = Globals.BUILDING_DATA[i];
            GameObject button = Instantiate(buildingButtonPrefab);
            button.name = data.unitName;
            button.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = data.unitName;
            Button b = button.GetComponent<Button>();
            AddBuildingButtonListener(b, i);
            button.transform.SetParent(buildingMenu);
    
            buildingButtons[data.code] = b;
            if (!Globals.BUILDING_DATA[i].CanBuy())
            {
                b.interactable = false;
            }
        }
    }
    private void AddBuildingButtonListener(Button button, int index)
    {
        button.onClick.AddListener(() => buildingPlacer.SelectPlacedBuilding(index));
    }

    private void SetResources(string resource, int amount)
    {
        resourcesTexts[resource].text = amount.ToString();
    }

    //Resources UI
    private void OnUpdateResourcesTexts()
    {

        foreach (KeyValuePair<string, GameResource> updatedPair in Globals.GAME_RESOURCES)
        {
            SetResources(updatedPair.Key, updatedPair.Value.Amount);
        }
    }

    //Building Button UI
    //Changes here
    private void OnCheckBuildingButtons()
    {
        foreach (BuildingData data in Globals.BUILDING_DATA)
        {
            buildingButtons[data.code].interactable = data.CanBuy();
        }
    }

    private void OnEnable()
    {
        EventManager.AddListener("UpdateResourcesTexts", OnUpdateResourcesTexts);
        EventManager.AddListener("CheckBuildingButtons", OnCheckBuildingButtons);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("UpdateResourcesTexts", OnUpdateResourcesTexts);
        EventManager.RemoveListener("CheckBuildingButtons", OnCheckBuildingButtons);
    }

}
