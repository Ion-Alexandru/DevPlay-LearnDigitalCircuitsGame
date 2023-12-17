using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wire : ElectricalComponent
{
    public static TilemapClick tilemapClick;
    private Tutorial_1Click tutorial_1Click;

    public Wire(Tilemap tilemap, Vector3Int position, TilemapClick tilemapClickInstance) : base(tilemap, position)
    {
        tilemapClick = tilemapClickInstance;
    }

    public Wire(Tilemap tilemap, Vector3Int position, Tutorial_1Click tutorial_1Click) : base(tilemap, position)
    {
        this.tutorial_1Click = tutorial_1Click;
    }

    // Check if this wire is powered by checking if any neighbors are powered.
    public override void UpdateState(Dictionary<Vector3Int, ElectricalComponent> components)
    {
        isPowered = false;
        isGrounded = false;
        Vector3Int leftNeighbor = position + Vector3Int.left;
        Vector3Int rightNeighbor = position + Vector3Int.right;
        Vector3Int downNeighbor = position + Vector3Int.down;
        Vector3Int upNeighbor = position + Vector3Int.up;
        Vector3Int gateNeighborOutput = position + new Vector3Int(-1, 1, 0); // Neighbor position for gateComponent
        Vector3Int gateNeighborInput = position + new Vector3Int(1, 2, 0); // Neighbor position for gateComponent

        // Check if gateComponent is present and powered
        if (components.ContainsKey(gateNeighborOutput))
        {
            ElectricalComponent neighborComponent = components[gateNeighborOutput];
            if (neighborComponent is ANDGate && neighborComponent.isPowered)
            {
                isPowered = true;
            }
            if (neighborComponent is NANDGate && neighborComponent.isPowered)
            {
                isPowered = true;
            }
            if (neighborComponent is ORGate && neighborComponent.isPowered)
            {
                isPowered = true;
            }
            if (neighborComponent is NORGate && neighborComponent.isPowered)
            {
                isPowered = true;
            }
            if (neighborComponent is XORGate && neighborComponent.isPowered)
            {
                isPowered = true;
            }
            if (neighborComponent is XNORGate && neighborComponent.isPowered)
            {
                isPowered = true;
            }
        }

        if (components.ContainsKey(leftNeighbor))
        {
            ElectricalComponent neighborComponent = components[leftNeighbor];
            if (neighborComponent is PushButton && ((PushButton)neighborComponent).isPressed)
            {
                isPowered = true;
            }
            else if (neighborComponent is Switch && ((Switch)neighborComponent).isPressed)
            {
                isPowered = true;
            }
            else if (neighborComponent is Diode && neighborComponent.isPowered)
            {
                isPowered = true;
            }
            else if (neighborComponent is NOTGate && neighborComponent.isPowered)
            {
                isPowered = true;
            }
            else if (neighborComponent is Wire && neighborComponent.isPowered)
            {
                isPowered = true;
            }
        }

        // Check right and down neighbors for grounding
        if (components.ContainsKey(rightNeighbor) || components.ContainsKey(downNeighbor))
        {
            ElectricalComponent rightNeighborComponent = components.GetValueOrDefault(rightNeighbor);
            ElectricalComponent downNeighborComponent = components.GetValueOrDefault(downNeighbor);

            if (downNeighborComponent is Ground)
            {
                isGrounded = true;
                //Debug.Log("Wire is grounded");
            }
            else if (rightNeighborComponent is Diode && rightNeighborComponent.isGrounded)
            {
                isGrounded = true;
                //Debug.Log("Wire is grounded by diode");
            }
            else if (rightNeighborComponent is Wire && rightNeighborComponent.isGrounded)
            {
                isGrounded = true;
                //Debug.Log("Wire is grounded by wire");
            }
        }

        // Check if there is a wire anywhere
        if (components.ContainsKey(upNeighbor) || components.ContainsKey(downNeighbor) || components.ContainsKey(leftNeighbor) || components.ContainsKey(rightNeighbor) || components.ContainsKey(gateNeighborOutput) || components.ContainsKey(gateNeighborInput))
        {
            components.TryGetValue(upNeighbor, out ElectricalComponent upComponent);
            components.TryGetValue(downNeighbor, out ElectricalComponent downComponent);
            components.TryGetValue(leftNeighbor, out ElectricalComponent leftComponent);
            components.TryGetValue(rightNeighbor, out ElectricalComponent rightComponent);
            components.TryGetValue(gateNeighborInput, out ElectricalComponent gateInput);
            components.TryGetValue(gateNeighborOutput, out ElectricalComponent gateOutput);

            // if(upComponent !=null){
            //     tilemap.SetTile(position, tilemapClick.upWireTile);
            // }

            // if(downComponent != null && downComponent is Ground){
            //     tilemap.SetTile(position, tilemapClick.downWireTile);
            // }
            if(leftComponent != null){
                tilemap.SetTile(position, tilemapClick.leftWireTile);
            }
            if(rightComponent != null){
                tilemap.SetTile(position, tilemapClick.rightWireTile);
            }
            // if(upComponent !=null && downComponent !=null){
            //     tilemap.SetTile(position, tilemapClick.verticalWireTile);
            // }
            if(leftComponent != null && rightComponent != null){
                tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            }

            if(gateInput != null && gateInput is ANDGate || gateInput is NANDGate || gateInput is ORGate || gateInput is NORGate || gateInput is XORGate || gateInput is XNORGate){
                tilemap.SetTile(position, tilemapClick.rightWireTile);
            }

            if(gateOutput != null && gateOutput is ANDGate || gateOutput is NANDGate || gateOutput is ORGate || gateOutput is NORGate || gateOutput is XORGate || gateOutput is XNORGate){
                tilemap.SetTile(position, tilemapClick.leftWireTile);
            }

            if(gateInput != null && leftComponent != null){
                if(gateInput is ANDGate || gateInput is NANDGate || gateInput is ORGate || gateInput is NORGate || gateInput is XORGate || gateInput is XNORGate){
                    tilemap.SetTile(position, tilemapClick.horizontalWireTile);
                }
            }

            if(gateOutput != null && rightComponent != null){
                if(gateOutput is ANDGate || gateOutput is NANDGate || gateOutput is ORGate || gateOutput is NORGate || gateOutput is XORGate || gateOutput is XNORGate){
                    tilemap.SetTile(position, tilemapClick.horizontalWireTile);
                }
            }

            // if(upComponent !=null && leftComponent !=null){
            //     tilemap.SetTile(position, tilemapClick.upLeftWireTile);
            // }
            // if(upComponent !=null && rightComponent !=null){
            //     tilemap.SetTile(position, tilemapClick.upRightWireTile);
            // }
            if(downComponent !=null && leftComponent !=null && downComponent is Ground){
                tilemap.SetTile(position, tilemapClick.downLeftWireTile);
            }
            // if(downComponent !=null && rightComponent !=null){
            //     tilemap.SetTile(position, tilemapClick.downRightWireTile);
            // }
            // if(upComponent !=null && downComponent !=null && leftComponent !=null){
            //     tilemap.SetTile(position, tilemapClick.upDownLeftWireTile);
            // }
            // if(upComponent !=null && downComponent !=null && rightComponent !=null){
            //     tilemap.SetTile(position, tilemapClick.upDownRightWireTile);
            // }
            // if(upComponent !=null && leftComponent !=null && rightComponent !=null){
            //     tilemap.SetTile(position, tilemapClick.upLeftRightWireTile);
            // }
            // if(downComponent !=null && leftComponent !=null && rightComponent !=null){
            //     tilemap.SetTile(position, tilemapClick.downLeftRightWireTile);
            // }
            // if(upComponent !=null && downComponent !=null && leftComponent !=null && rightComponent !=null){
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }

            // if (upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.upWireTile);
            // }
            // if (downComponent != null && downComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.downWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.leftWireTile);
            // }
            // if (rightComponent != null && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.rightWireTile);
            // }
            // if (upComponent != null && downComponent != null && upComponent is Wire && downComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.verticalWireTile);
            // }
            // if (leftComponent != null && rightComponent != null && leftComponent is Wire && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (upComponent != null && leftComponent != null && upComponent is Wire && leftComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.upLeftWireTile);
            // }
            // if (upComponent != null && rightComponent != null && upComponent is Wire && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.upRightWireTile);
            // }
            // if (downComponent != null && leftComponent != null && downComponent is Wire && leftComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftWireTile);
            // }
            // if (downComponent != null && rightComponent != null && downComponent is Wire && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.downRightWireTile);
            // }
            // if (upComponent != null && downComponent != null && leftComponent != null && upComponent is Wire && downComponent is Wire && leftComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.upDownLeftWireTile);
            // }
            // if (upComponent != null && downComponent != null && rightComponent != null && upComponent is Wire && downComponent is Wire && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.upDownRightWireTile);
            // }
            // if (upComponent != null && leftComponent != null && rightComponent != null && upComponent is Wire && leftComponent is Wire && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.upLeftRightWireTile);
            // }
            // if (downComponent != null && leftComponent != null && rightComponent != null && downComponent is Wire && leftComponent is Wire && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftRightWireTile);
            // }
            // if (upComponent != null && downComponent != null && leftComponent != null && rightComponent != null && upComponent is Wire && downComponent is Wire && leftComponent is Wire && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if(leftComponent != null && leftComponent is Diode && leftComponent.isPowered)
            // {
            //     tilemap.SetTile(position, tilemapClick.leftWireTile);
            // }
            // if (rightComponent != null && rightComponent is Diode && rightComponent.isPowered)
            // {
            //     tilemap.SetTile(position, tilemapClick.rightWireTile);
            // }
            // if(leftComponent != null && rightComponent != null && leftComponent is Diode && rightComponent is Diode)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if(leftComponent != null && rightComponent != null && leftComponent is Diode && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (leftComponent != null && rightComponent != null && leftComponent is Wire && rightComponent is Diode)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if(leftComponent != null && leftComponent is PushButton && leftComponent.isPowered)
            // {
            //     tilemap.SetTile(position, tilemapClick.leftWireTile);
            // }
            // if (rightComponent != null && rightComponent is PushButton && rightComponent.isPowered)
            // {
            //     tilemap.SetTile(position, tilemapClick.rightWireTile);
            // }
            // if(leftComponent != null && rightComponent != null && leftComponent is PushButton && rightComponent is PushButton)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if(leftComponent != null && rightComponent != null && leftComponent is PushButton && rightComponent is PushButton)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (leftComponent != null && rightComponent != null && leftComponent is PushButton && rightComponent is PushButton)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if(downComponent != null && downComponent is Ground)
            // {
            //     tilemap.SetTile(position, tilemapClick.downWireTile);
            // }
            // if(downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && rightComponent != null && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.downRightWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftRightWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if(downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is PushButton)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && rightComponent != null && rightComponent is PushButton)
            // {
            //     tilemap.SetTile(position, tilemapClick.downRightWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is PushButton && rightComponent != null && rightComponent is PushButton)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftRightWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is PushButton && rightComponent != null && rightComponent is PushButton && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Diode)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && rightComponent != null && rightComponent is Diode)
            // {
            //     tilemap.SetTile(position, tilemapClick.downRightWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Diode && rightComponent != null && rightComponent is Diode)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftRightWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Diode && rightComponent != null && rightComponent is Diode && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Diode && rightComponent != null && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftRightWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Diode)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftRightWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.downLeftRightWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (downComponent != null && downComponent is Ground && leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if(leftComponent != null && leftComponent is PushButton)
            // {
            //     tilemap.SetTile(position, tilemapClick.leftWireTile);
            // }
            // if (rightComponent != null && rightComponent is PushButton)
            // {
            //     tilemap.SetTile(position, tilemapClick.rightWireTile);
            // }
            // if (leftComponent != null && leftComponent is PushButton && rightComponent != null && rightComponent is PushButton)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (leftComponent != null && leftComponent is PushButton && rightComponent != null && rightComponent is PushButton && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (leftComponent != null && leftComponent is PushButton && rightComponent != null && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is PushButton)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (leftComponent != null && leftComponent is Diode)
            // {
            //     tilemap.SetTile(position, tilemapClick.leftWireTile);
            // }
            // if (rightComponent != null && rightComponent is Diode)
            // {
            //     tilemap.SetTile(position, tilemapClick.rightWireTile);
            // }
            // if (leftComponent != null && leftComponent is Diode && rightComponent != null && rightComponent is Diode)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (leftComponent != null && leftComponent is Diode && rightComponent != null && rightComponent is Diode && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (leftComponent != null && leftComponent is Diode && rightComponent != null && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Diode)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.horizontalWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }
            // if (leftComponent != null && leftComponent is Wire && rightComponent != null && rightComponent is Wire && upComponent != null && upComponent is Wire)
            // {
            //     tilemap.SetTile(position, tilemapClick.allWireTile);
            // }

        }
    }

    // If this wire was removed, then any wires connected to this wire may have changed state.
    // Update their state.
    public override void OnRemove(Dictionary<Vector3Int, ElectricalComponent> components)
    {
        foreach (Vector3Int neighbor in GetNeighbors())
        {
            if (components.ContainsKey(neighbor))
            {
                ElectricalComponent neighborComponent = components[neighbor];
                if (neighborComponent is Wire)
                {
                    neighborComponent.UpdateState(components);
                }
            }
        }
    }
}