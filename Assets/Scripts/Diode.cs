using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Diode : ElectricalComponent
{
    public static TilemapClick tilemapClick;
    private bool lastPoweredState = false;
    private bool lastGroundedState = false;
    private Tutorial_1Click tilemapClickInstance;

    public Diode(Tilemap tilemap, Vector3Int position, TilemapClick tilemapClickInstance) : base(tilemap, position)
    {
        tilemapClick = tilemapClickInstance;
        // Set the tile at the diode's position to the unlit diode tile
        tilemap.SetTile(position, tilemapClick.unlitDiodeTile);
    }

    public Diode(Tilemap tilemap, Vector3Int position, Tutorial_1Click tilemapClickInstance) : base(tilemap, position)
    {
        this.tilemapClickInstance = tilemapClickInstance;
    }

    public override void UpdateState(Dictionary<Vector3Int, ElectricalComponent> components)
    {
        bool inputPowered = false;
        bool inputGrounded = false;

        // Check the left neighbor for power input
        Vector3Int leftNeighbor = new Vector3Int(position.x - 1, position.y, position.z);
        Vector3Int rightNeighbor = new Vector3Int(position.x + 1, position.y, position.z);

        if (components.ContainsKey(leftNeighbor))
        {
            ElectricalComponent leftComponent = components[leftNeighbor];
            inputPowered = leftComponent.isPowered;
        }

        if (components.ContainsKey(rightNeighbor))
        {
            ElectricalComponent rightComponent = components[rightNeighbor];
            inputGrounded = rightComponent.isGrounded;
        }

        isPowered = inputPowered;
        isGrounded = inputGrounded;

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
            if (neighborPos == rightNeighbor) continue;

            UpdateNeighbor(components, neighborPos);
        }

        if (lastPoweredState != isPowered || lastGroundedState != isGrounded)
        {
            lastPoweredState = isPowered;
            lastGroundedState = isGrounded;
            if (isPowered && isGrounded)
            {
                tilemap.SetTile(position, tilemapClick.litDiodeTile);
                //Debug.Log("Diode is powered");
            }
            else
            {
                tilemap.SetTile(position, tilemapClick.unlitDiodeTile);
            }
        }
    }
}