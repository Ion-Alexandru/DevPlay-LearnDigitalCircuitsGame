using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public abstract class ElectricalComponent
{
    public Vector3Int position;
    public Tilemap tilemap;
    public bool isPowered;
    public bool isGrounded;

    public ElectricalComponent(Tilemap tilemap, Vector3Int position)
    {
        this.tilemap = tilemap;
        this.position = position;
        isPowered = false;
        isGrounded = false;
    }

    // UpdateState updates the state of this component based on the other components in the system
    public abstract void UpdateState(Dictionary<Vector3Int, ElectricalComponent> electricalComponents);

    // GetNeighbors returns the neighboring positions
    protected List<Vector3Int> GetNeighbors()
    {
        return new List<Vector3Int>
        {
            position + Vector3Int.right,
            position + Vector3Int.left,
            position + Vector3Int.up,
            position + Vector3Int.down,
            position + new Vector3Int(-1, 1, 0) // New neighbor position for gateComponent
        };
    }

    // UpdateNeighbor updates the state of a specific neighbor component
    protected void UpdateNeighbor(Dictionary<Vector3Int, ElectricalComponent> components, Vector3Int neighborPosition)
    {
        if (components.ContainsKey(neighborPosition))
        {
            components[neighborPosition].UpdateState(components);
        }
    }

    public virtual void OnRemove(Dictionary<Vector3Int, ElectricalComponent> components)
    {
    }
}