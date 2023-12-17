using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireButtonScript : MonoBehaviour
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
        tilemapClick.placingWires = true;
        
        tilemapClick.switchPlacement = false;
        tilemapClick.movingToolPick = false;
        tilemapClick.movingToolPlace = false;
        tilemapClick.deleteComponent = false;
        tilemapClick.pushButtonPlacement = false;
        tilemapClick.NOTGatePlacement = false;
        tilemapClick.diodePlacement = false;
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
