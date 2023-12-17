using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NANDGate : ElectricalComponent
{
    public static TilemapClick tilemapClick;
    private bool lastPoweredState = false;
    private Tutorial_1Click tilemapClickInstance;
    public NANDGate(Tilemap tilemap, Vector3Int position, TilemapClick tilemapClickInstance) : base(tilemap, position)
    {
        tilemapClick = tilemapClickInstance;
        // Set the tile at the diode's position to the unlit diode tile
        tilemap.SetTile(position, tilemapClick.ANDGate1);
        tilemap.SetTile(position + Vector3Int.down, tilemapClick.NANDGate);
        tilemap.SetTile(position + 2*Vector3Int.down, tilemapClick.ANDGate3);
    }

    public NANDGate(Tilemap tilemap, Vector3Int position, Tutorial_1Click tilemapClickInstance) : base(tilemap, position)
    {
        this.tilemapClickInstance = tilemapClickInstance;
    }

    public override void UpdateState(Dictionary<Vector3Int, ElectricalComponent> components)
    {
        bool input1Powered = false;
        bool input2Powered = false;

        // Check the left neighbor for power input
        Vector3Int leftNeighbor = new Vector3Int(position.x - 1, position.y, position.z);
        Vector3Int leftNeighbor2 = new Vector3Int(position.x - 1, position.y - 2, position.z);

        if (components.ContainsKey(leftNeighbor))
        {
            ElectricalComponent leftComponent = components[leftNeighbor];
            input1Powered = leftComponent.isPowered;
        }

        if (components.ContainsKey(leftNeighbor2))
        {
            ElectricalComponent leftComponent = components[leftNeighbor2];
            input2Powered = leftComponent.isPowered;

        }

        if (input1Powered == false && input2Powered == false)
        {
            isPowered = true;
        }
        else if (input1Powered == true && input2Powered == false)
        {
            isPowered = true;
        }
        else if (input1Powered == false && input2Powered == true)
        {
            isPowered = true;
        }
        else
        {
            isPowered = false;
        }

        // if(input1Powered)
        //     Debug.Log("Input 1 is powered");

        // if(input2Powered)
        //     Debug.Log("Input 2 is powered");

        // Propagate the state to neighbors
        foreach (Vector3Int neighborPos in GetNeighbors())
        {
            // Skip the left neighbor (already checked for input power)
            if (neighborPos == leftNeighbor) continue;

            UpdateNeighbor(components, neighborPos);
        }

        // Propagate the state to neighbors
        foreach (Vector3Int neighborPos in GetNeighbors())
        {
            // Skip the right neighbor (already checked for input power)
            if (neighborPos == leftNeighbor2) continue;

            UpdateNeighbor(components, neighborPos);
        }

        if (lastPoweredState != isPowered)
        {
            lastPoweredState = isPowered;
        }
    }
}