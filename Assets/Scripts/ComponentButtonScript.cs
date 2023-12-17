using UnityEngine;
using UnityEngine.UI;

public class ComponentButtonScript : MonoBehaviour
{
    public string componentName;
    public GameObject disablePlacementButton;

    private TilemapClick tilemapClick;
    private Button button;

    private void Start()
    {
        disablePlacementButton.SetActive(false);

        // Assign the tilemapClick variable to an instance of the TilemapClick class
        tilemapClick = FindObjectOfType<TilemapClick>();

        button = GetComponent<Button>(); // Get the Button component attached to this object
        button.onClick.AddListener(OnClick); // Add a listener for when the button is clicked
    }

    private void OnClick()
    {
        switch (componentName)
    {
        case "PushButton":
            tilemapClick.pushButtonPlacement = true;
            
            tilemapClick.switchPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.NOTGatePlacement = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.diodePlacement = false;
            tilemapClick.groundPlacement = false;
            tilemapClick.ANDGatePlacement = false;
            tilemapClick.NANDGatePlacement = false;
            tilemapClick.ORGatePlacement = false;
            tilemapClick.NORGatePlacement = false;
            tilemapClick.XORGatePlacement = false;
            tilemapClick.XNORGatePlacement = false;
            break;
        case "Switch":
            tilemapClick.switchPlacement = true;
            
            tilemapClick.pushButtonPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.NOTGatePlacement = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.diodePlacement = false;
            tilemapClick.groundPlacement = false;
            tilemapClick.ANDGatePlacement = false;
            tilemapClick.NANDGatePlacement = false;
            tilemapClick.ORGatePlacement = false;
            tilemapClick.NORGatePlacement = false;
            tilemapClick.XORGatePlacement = false;
            tilemapClick.XNORGatePlacement = false;
            break;
        case "Diode":
            tilemapClick.diodePlacement = true;

            tilemapClick.switchPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.pushButtonPlacement = false;
            tilemapClick.groundPlacement = false;
            tilemapClick.ANDGatePlacement = false;
            tilemapClick.NOTGatePlacement = false;
            tilemapClick.NANDGatePlacement = false;
            tilemapClick.ORGatePlacement = false;
            tilemapClick.NORGatePlacement = false;
            tilemapClick.XORGatePlacement = false;
            tilemapClick.XNORGatePlacement = false;
            break;
        case "Ground":
            tilemapClick.groundPlacement = true;

            tilemapClick.switchPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.diodePlacement = false;
            tilemapClick.pushButtonPlacement = false;
            tilemapClick.ANDGatePlacement = false;
            tilemapClick.NOTGatePlacement = false;
            tilemapClick.NANDGatePlacement = false;
            tilemapClick.ORGatePlacement = false;
            tilemapClick.NORGatePlacement = false;
            tilemapClick.XORGatePlacement = false;
            tilemapClick.XNORGatePlacement = false;
            break;
        case "ANDGate":
            tilemapClick.ANDGatePlacement = true;

            tilemapClick.switchPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.groundPlacement = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.diodePlacement = false;
            tilemapClick.pushButtonPlacement = false;
            tilemapClick.NOTGatePlacement = false;
            tilemapClick.NANDGatePlacement = false;
            tilemapClick.ORGatePlacement = false;
            tilemapClick.NORGatePlacement = false;
            tilemapClick.XORGatePlacement = false;
            tilemapClick.XNORGatePlacement = false;
            break;
        case "NOTGate":
            tilemapClick.NOTGatePlacement = true;

            tilemapClick.switchPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.groundPlacement = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.diodePlacement = false;
            tilemapClick.pushButtonPlacement = false;
            tilemapClick.ANDGatePlacement = false;
            tilemapClick.NANDGatePlacement = false;
            tilemapClick.ORGatePlacement = false;
            tilemapClick.NORGatePlacement = false;
            tilemapClick.XORGatePlacement = false;
            tilemapClick.XNORGatePlacement = false;
            break;
        case "NANDGate":
            tilemapClick.NANDGatePlacement = true;

            tilemapClick.switchPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.groundPlacement = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.diodePlacement = false;
            tilemapClick.pushButtonPlacement = false;
            tilemapClick.ANDGatePlacement= false;
            tilemapClick.NOTGatePlacement = false;
            tilemapClick.ORGatePlacement = false;
            tilemapClick.NORGatePlacement = false;
            tilemapClick.XORGatePlacement = false;
            tilemapClick.XNORGatePlacement = false;
            break;
        case "ORGate":
            tilemapClick.ORGatePlacement = true;

            tilemapClick.switchPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.groundPlacement = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.diodePlacement = false;
            tilemapClick.pushButtonPlacement = false;
            tilemapClick.ANDGatePlacement = false;
            tilemapClick.NOTGatePlacement = false;
            tilemapClick.NANDGatePlacement = false;
            tilemapClick.NORGatePlacement = false;
            tilemapClick.XORGatePlacement = false;
            tilemapClick.XNORGatePlacement = false;
            break;
        case "NORGate":
            tilemapClick.NORGatePlacement = true;

            tilemapClick.switchPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.groundPlacement = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.diodePlacement = false;
            tilemapClick.pushButtonPlacement = false;
            tilemapClick.ANDGatePlacement = false;
            tilemapClick.NOTGatePlacement = false;
            tilemapClick.NANDGatePlacement = false;
            tilemapClick.ORGatePlacement = false;
            tilemapClick.XORGatePlacement = false;
            tilemapClick.XNORGatePlacement = false;
            break;
        case "XORGate":
            tilemapClick.XORGatePlacement = true;

            tilemapClick.switchPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.groundPlacement = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.diodePlacement = false;
            tilemapClick.pushButtonPlacement = false;
            tilemapClick.ANDGatePlacement = false;
            tilemapClick.NOTGatePlacement = false;
            tilemapClick.NANDGatePlacement = false;
            tilemapClick.ORGatePlacement = false;
            tilemapClick.NORGatePlacement = false;
            tilemapClick.XNORGatePlacement = false;
            break;
        case "XNORGate":
            tilemapClick.XNORGatePlacement = true;

            tilemapClick.switchPlacement = false;
            tilemapClick.movingToolPick = false;
            tilemapClick.movingToolPlace = false;
            tilemapClick.groundPlacement = false;
            tilemapClick.placingWires = false;
            tilemapClick.deleteComponent = false;
            tilemapClick.diodePlacement = false;
            tilemapClick.pushButtonPlacement = false;
            tilemapClick.ANDGatePlacement = false;
            tilemapClick.NOTGatePlacement = false;
            tilemapClick.NANDGatePlacement = false;
            tilemapClick.ORGatePlacement = false;
            tilemapClick.NORGatePlacement = false;
            tilemapClick.XORGatePlacement = false;
            break;
        default:
            break;
        }

        disablePlacementButton.SetActive(true);
    }
}