using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserButtonScript : MonoBehaviour
{
    public GameObject disablePlacementButton;
    TilemapClick tilemapClick;

    // Start is called before the first frame update
    void Start()
    {
        // Find the TilemapClick component on a GameObject in the scene
        tilemapClick = FindObjectOfType<TilemapClick>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        tilemapClick.deleteComponent = true;

        tilemapClick.placingWires = false;
        tilemapClick.switchPlacement = false;
        tilemapClick.movingToolPick = false;
        tilemapClick.movingToolPlace = false;
        tilemapClick.placingWires = false;
        tilemapClick.diodePlacement = false;
        tilemapClick.pushButtonPlacement = false;
        tilemapClick.groundPlacement = false;
        tilemapClick.ANDGatePlacement = false;
        tilemapClick.NANDGatePlacement = false;
        tilemapClick.ORGatePlacement = false;
        tilemapClick.NORGatePlacement = false;
        tilemapClick.XORGatePlacement = false;
        tilemapClick.XNORGatePlacement = false;
        disablePlacementButton.SetActive(true);
    }
}
