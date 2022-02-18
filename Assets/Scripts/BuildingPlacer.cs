using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
    //null
    private Building placedBuilding = null;
    private RaycastHit raycastHit;
    private Ray ray;
    private Vector3 lastPlacementPosition;


    private void Update()
    {
        if (placedBuilding != null)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                CancelPlaceBuilding();
                return;
            }
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, 1000f, Globals.TERRAIN_LAYER_MASK))
            {

                placedBuilding.SetPosition(raycastHit.point);
                //Debug.Log(placedBuilding.HP);
                //Debug.Log(placedBuilding.Code);
                if (lastPlacementPosition != raycastHit.point)
                {
                    placedBuilding.CheckValidPlacement();
                }
                lastPlacementPosition = raycastHit.point;

            }
            if (placedBuilding.HasValidPlacement && Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PlaceBuilding();
            }
        }

    }
    public void SelectPlacedBuilding(int buildingIndex)
    {
        PreparePlacedBuilding(buildingIndex);
    
    }
    private void PreparePlacedBuilding(int buildingDataIndex)
    {
        if (placedBuilding != null && !placedBuilding.IsFixed)
        {
            Destroy(placedBuilding.Transform.gameObject);

        }

        Building building = new Building(Globals.BUILDING_DATA[buildingDataIndex]);

        building.Transform.GetComponent<BuildingManager>().Initialize(building);
        placedBuilding = building;
        lastPlacementPosition = Vector3.zero;
        
    }
    private void PlaceBuilding()
    {
 
        placedBuilding.Place();
        if (placedBuilding.CanBuy())
        {
            PreparePlacedBuilding(placedBuilding.DataIndex);
        }
        else
        {
            placedBuilding = null;
        }
        EventManager.TriggerEvent("UpdateResourcesTexts");
        EventManager.TriggerEvent("CheckBuildingButtons");

    }

    public void CancelPlaceBuilding()
    {
        Destroy(placedBuilding.Transform.gameObject);
        placedBuilding = null;
    }
}
