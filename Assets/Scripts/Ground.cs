using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Ground : ElectricalComponent
{
    public Ground(Tilemap tilemap, Vector3Int position) : base(tilemap, position)
    {
    }

    public override void UpdateState(Dictionary<Vector3Int, ElectricalComponent> electricalComponents)
    {
        // No need to update anything for the ground component.
    }
}