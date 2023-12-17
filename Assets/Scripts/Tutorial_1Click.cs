using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tutorial_1Click : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase blankTile;

    public TileBase wireTile;
    public TileBase pushButtonUnpressedTile;
    public TileBase pushButtonPressedTile;
    public TileBase diodeTile;
    public TileBase unlitDiodeTile;
    public TileBase litDiodeTile;
    public TileBase groundTile;
    public bool pushButtonPlacement = false;
    public bool diodePlacement = false;
    public bool groundPlacement = false;
    public bool deleteComponent = false;
    public bool placingWires { get; set; } = false;

    private List<Wire> wireList = new List<Wire>();
    private List<PushButton> pushButtonList = new List<PushButton>();
    private List<Diode> diodeList = new List<Diode>();
    private List<Ground> groundList = new List<Ground>();
    private Dictionary<Vector3Int, ElectricalComponent> components = new Dictionary<Vector3Int, ElectricalComponent>();

    private void Start() 
    {
        
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);

        pushButtonClick(cellPosition);
        
        if(deleteComponent && Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckAndRemove(cellPosition);
            tilemap.SetTile(cellPosition, blankTile);
        }

        if (pushButtonPlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckAndRemove(cellPosition);
            PushButton newPushButton = new PushButton(tilemap, cellPosition);
            components[cellPosition] = newPushButton;
            tilemap.SetTile(cellPosition, pushButtonUnpressedTile);

            pushButtonList.Add(newPushButton);
        }

        if (diodePlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckAndRemove(cellPosition);
            Diode newDiode = new Diode(tilemap, cellPosition, tilemapClickInstance: this);
            components[cellPosition] = newDiode;
            //tilemap.SetTile(cellPosition, diodeTile);

            diodeList.Add(newDiode);
        }

        if (groundPlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckAndRemove(cellPosition);
            Ground newGround = new Ground(tilemap, cellPosition);
            components[cellPosition] = newGround;

            // Set the tile at the given position to the rule tile
            tilemap.SetTile(cellPosition, groundTile);

            groundList.Add(newGround);
        }

        if (placingWires && Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckAndRemove(cellPosition);
            Wire newWire = new Wire(tilemap, cellPosition, this);
            components.Add(cellPosition, newWire);
            tilemap.SetTile(cellPosition, wireTile);

            wireList.Add(newWire);
        }

        UpdateAllComponents();
    }

    void pushButtonClick(Vector3Int mousePosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (components.TryGetValue(mousePosition, out ElectricalComponent component) && component is PushButton pushButton)
            {
                pushButton.isPressed = true;
                tilemap.SetTile(pushButton.position, pushButtonPressedTile);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (components.TryGetValue(mousePosition, out ElectricalComponent component) && component is PushButton pushButton)
            {
                pushButton.isPressed = false;
                tilemap.SetTile(pushButton.position, pushButtonUnpressedTile);
            }
        }
    }

    private void UpdateAllComponents()
    {
        Dictionary<Vector3Int, ElectricalComponent> components = new Dictionary<Vector3Int, ElectricalComponent>();
        foreach (Wire wire in wireList) components.Add(wire.position, wire);
        foreach (PushButton button in pushButtonList) components.Add(button.position, button);
        foreach (Diode diode in diodeList) components.Add(diode.position, diode);
        foreach (Ground ground in groundList) components.Add(ground.position, ground);

        int maxIterations = 100;
        int iteration = 0;
        bool stateChanged = false;
        do
        {
            stateChanged = false;
            iteration++;

            foreach (ElectricalComponent component in components.Values)
            {
                bool previousPoweredState = component.isPowered;
                component.UpdateState(components);

                if (previousPoweredState != component.isPowered)
                {
                    stateChanged = true;
                }
            }
        } while (stateChanged && iteration < maxIterations);
    }

    private void CheckAndRemove(Vector3Int position)
    {
        if (components.TryGetValue(position, out ElectricalComponent existingComponent))
        {
            if (existingComponent is PushButton)
            {
                pushButtonList.Remove((PushButton)existingComponent);
            }
            else if (existingComponent is Diode)
            {
                diodeList.Remove((Diode)existingComponent);
            }
            else if (existingComponent is Ground)
            {
                groundList.Remove((Ground)existingComponent);
            }
            else if (existingComponent is Wire)
            {
                wireList.Remove((Wire)existingComponent);
            }

            components.Remove(position);
        }
    }
}
