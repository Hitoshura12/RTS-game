using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BuildingManager : UnitManager
{
    private BoxCollider _boxCollider;
    private Building _building =null;

    private int nCollisions = 0;

    public void Initialize(Building building)
    {
        _boxCollider = GetComponent<BoxCollider>();
        _building = building;
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain") return;
        nCollisions++;
        CheckPlacement();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Terrain") return;
        nCollisions--;
        CheckPlacement();
    }
    public bool CheckPlacement()
    {
        if (_building == null) return false;
        if (_building.IsFixed) return false;

        bool validPlacement = HasValidPlacement();
        if (!validPlacement)
        {
            _building.SetMaterials(BuildingPlacement.Invalid);
        }
       else
        {
            _building.SetMaterials(BuildingPlacement.Valid);
        }

        return validPlacement;
    }
    protected override bool IsActive()
    {
        return _building.IsFixed;
    }
    private bool HasValidPlacement()
    {
        if (nCollisions > 0) return false;

        // get 4 bottom corner positions
        Vector3 p = transform.position;
        Vector3 c = _boxCollider.center;
        Vector3 e = _boxCollider.size / 2f;
        float bottomHeight = c.y - e.y + 0.5f;
        Vector3[] bottomCorners = new Vector3[]
        {
        new Vector3(c.x - e.x, bottomHeight, c.z - e.z),
        new Vector3(c.x - e.x, bottomHeight, c.z + e.z),
        new Vector3(c.x + e.x, bottomHeight, c.z - e.z),
        new Vector3(c.x + e.x, bottomHeight, c.z + e.z)
        };
        // cast a small ray beneath the corner to check for a close ground
        // (if at least two are not valid, then placement is invalid)
        int invalidCornersCount = 0;
        foreach (Vector3 corner in bottomCorners)
        {
            if (!Physics.Raycast(
                p + corner,
                Vector3.up * -1f,
                2f,
                Globals.TERRAIN_LAYER_MASK
            ))
                invalidCornersCount++;
        }
        return invalidCornersCount < 3;
    }
}
