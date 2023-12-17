using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingToolScript : MonoBehaviour
{
    public Button movingButton;
    TilemapClick tilemapClick;


    // Start is called before the first frame update
    void Start()
    {
        tilemapClick = FindObjectOfType<TilemapClick>();

        movingButton.onClick.AddListener(MovingMethod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MovingMethod()
    {
        tilemapClick.movingToolPick = true;

        tilemapClick.movingToolPlace = false;
        tilemapClick.placingWires = false;
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
    }
}
