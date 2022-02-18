using System.Collections.Generic;

public static class Globals
{
    public static List<UnitManager> SELECTED_UNITS = new List<UnitManager>();

    public static BuildingData[] BUILDING_DATA;
    public static int TERRAIN_LAYER_MASK = 1 << 8;

    public static Dictionary<string, GameResource> GAME_RESOURCES = new Dictionary<string, GameResource>()
    {
        {"gold", new GameResource("Gold",600) },
        {"wood", new GameResource("Wood",500) },
        {"stone", new GameResource("Stone",500) }

    };

}
