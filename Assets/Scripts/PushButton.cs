 using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PushButton : ElectricalComponent
{
    public bool isPressed;
    public bool hasUpdatedWires;

    public PushButton(Tilemap tilemap, Vector3Int position) : base(tilemap, position)
    {
        isPressed = false;
        hasUpdatedWires = false;
    }

    public override void UpdateState(Dictionary<Vector3Int, ElectricalComponent> components)
    {
        if (isPressed && !hasUpdatedWires)
        {
            hasUpdatedWires = true;
        }
        else if (!isPressed)
        {
            hasUpdatedWires = false;
        }
    }
}