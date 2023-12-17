using UnityEngine;
using UnityEngine.UI;

public class XButtonScript : MonoBehaviour
{    private TilemapClick tilemapClick;
    private Button xButton;
    public GameObject disablePlacementButton;

    private void Start()
    {
        tilemapClick = FindObjectOfType<TilemapClick>();

        xButton = GetComponent<Button>(); // Get the Button component attached to this object
        xButton.onClick.AddListener(OnClick); // Add a listener for when the button is clicked
    }

    private void OnClick()
    {
        tilemapClick.placingWires = false;
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

        disablePlacementButton.SetActive(false);
    }
}