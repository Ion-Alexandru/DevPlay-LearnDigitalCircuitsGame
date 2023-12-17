using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class NOTGate : ElectricalComponent
{
    public static TilemapClick tilemapClick;
    private bool lastPoweredState = false;
    private Tutorial_1Click tilemapClickInstance;
    public NOTGate(Tilemap tilemap, Vector3Int position, TilemapClick tilemapClickInstance) : base(tilemap, position)
    {
        tilemapClick = tilemapClickInstance;
        // Set the tile at the diode's position to the unlit diode tile
        tilemap.SetTile(position, tilemapClick.NOTGate);
    }

    public NOTGate(Tilemap tilemap, Vector3Int position, Tutorial_1Click tilemapClickInstance) : base(tilemap, position)
    {
        this.tilemapClickInstance = tilemapClickInstance;
    }

    public override void UpdateState(Dictionary<Vector3Int, ElectricalComponent> components)
    {
        bool input1Powered = false;

        // Check the left neighbor for power input
        Vector3Int leftNeighbor = new Vector3Int(position.x - 1, position.y, position.z);

        if (components.ContainsKey(leftNeighbor))
        {
            ElectricalComponent leftComponent = components[leftNeighbor];
            input1Powered = leftComponent.isPowered;
        }

        if(input1Powered)
        {
            isPowered = false;
        } else if (!input1Powered)
        {
            isPowered = true;
        }

        foreach (Vector3Int neighborPos in GetNeighbors())
        {
            // Skip the left neighbor (already checked for input power)
            if (neighborPos == leftNeighbor) continue;

            UpdateNeighbor(components, neighborPos);
        }

        if (lastPoweredState != isPowered)
        {
            lastPoweredState = isPowered;
        }
    }
}