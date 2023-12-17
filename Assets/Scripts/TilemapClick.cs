using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TilemapClick : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase rightWireTile;
    public TileBase leftWireTile;
    public TileBase upWireTile;
    public TileBase downWireTile;
    public TileBase verticalWireTile;
    public TileBase horizontalWireTile;
    public TileBase upLeftWireTile;
    public TileBase upRightWireTile;
    public TileBase downLeftWireTile;
    public TileBase downRightWireTile;
    public TileBase upDownLeftWireTile;
    public TileBase upDownRightWireTile;
    public TileBase upLeftRightWireTile;
    public TileBase downLeftRightWireTile;
    public TileBase allWireTile;
    public TileBase pointWire;
    public TileBase blankTile;

    public TutorialUIScript tutorialUIScript;

    public TileBase wireTile;
    public TileBase pushButtonUnpressedTile;
    public TileBase pushButtonPressedTile;
    public TileBase diodeTile;
    public TileBase unlitDiodeTile;
    public TileBase litDiodeTile;
    public TileBase groundTile;

    public TileBase ANDGate1;
    public TileBase ANDGate2;
    public TileBase ANDGate3;
    public TileBase NANDGate;
    public TileBase NOTGate;
    public TileBase ORGate1;
    public TileBase ORGate2;
    public TileBase ORGate3;
    public TileBase NORGate;
    public TileBase XORGate1;
    public TileBase XORGate2;
    public TileBase XORGate3;
    public TileBase XNORGate;
    public TileBase Switch1;
    public TileBase Swtich2;

    public bool pushButtonPlacement = false;
    public bool switchPlacement = false;
    public bool diodePlacement = false;
    public bool groundPlacement = false;
    public bool deleteComponent = false;
    public bool ANDGatePlacement = false;
    public bool NANDGatePlacement = false;
    public bool NOTGatePlacement = false;
    public bool ORGatePlacement = false;
    public bool NORGatePlacement = false;
    public bool XORGatePlacement = false;
    public bool XNORGatePlacement = false;
    public bool movingToolPick = false;
    public bool movingToolPlace = false;
    public bool placingWires { get; set; } = false;

    private List<Wire> wireList = new List<Wire>();
    private List<PushButton> pushButtonList = new List<PushButton>();
    private List<Switch> switchList = new List<Switch>();
    private List<Diode> diodeList = new List<Diode>();
    private List<Ground> groundList = new List<Ground>();
    private List<ANDGate> ANDGateList = new List<ANDGate>();
    private List<NANDGate> NANDGateList = new List<NANDGate>();
    private List<NOTGate> NOTGateList = new List<NOTGate>();
    private List<ORGate> ORGateList = new List<ORGate>();
    private List<NORGate> NORGateList = new List<NORGate>();
    private List<XORGate> XORGateList = new List<XORGate>();
    private List<XNORGate> XNORGateList = new List<XNORGate>();
    private Dictionary<Vector3Int, ElectricalComponent> components = new Dictionary<Vector3Int, ElectricalComponent>();

    private void Start() 
    {
        if (SceneManager.GetActiveScene().name == "Tutorial#1")
        {
            // Fixed position for the diode in the "Tutorial1" scene
            Vector3Int diodePosition = new Vector3Int(0, 0, 0);
            Vector3Int pushButtonPosition = new Vector3Int(-4, 0, 0);
            Vector3Int groundPosition = new Vector3Int(+3, -1, 0);

            // Create and add a diode at the fixed position
            Diode newDiode = new Diode(tilemap, diodePosition, tilemapClickInstance: this);
            components[diodePosition] = newDiode;
            tilemap.SetTile(diodePosition, diodeTile);

            diodeList.Add(newDiode);

            Ground newGround = new Ground(tilemap, groundPosition);
            tilemap.SetTile(groundPosition, groundTile);

            groundList.Add(newGround);

            PushButton newPushButton = new PushButton(tilemap, pushButtonPosition);
            components[pushButtonPosition] = newPushButton;
            tilemap.SetTile(pushButtonPosition, pushButtonUnpressedTile);

            pushButtonList.Add(newPushButton);
        }
    }

    private void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (SceneManager.GetActiveScene().name == "Tutorial#1")
        {
            Vector3Int diodePosition = new Vector3Int(0, 0, 0);

            if (components.TryGetValue(diodePosition, out ElectricalComponent component) && component is Diode diode)
            {
                if(diode.isGrounded && diode.isPowered)
                    tutorialUIScript.playCongrats();
            }
        }

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        Vector3Int belowCellPosition = new Vector3Int(cellPosition.x, cellPosition.y - 1, cellPosition.z);
        Vector3Int underBelowCellPosition = new Vector3Int(belowCellPosition.x, belowCellPosition.y - 1, belowCellPosition.z);

        pushButtonClick(cellPosition);
        switchClick(cellPosition);

        // if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     ANDGatePlacement = true;
        // }

        // if (Input.GetKeyUp(KeyCode.Alpha2))
        // {
        //     ANDGatePlacement = false;
        // }
        
        if(deleteComponent && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (components.TryGetValue(cellPosition, out ElectricalComponent existingComponent))
            {
                if (existingComponent is ANDGate || existingComponent is NANDGate || existingComponent is ORGate || existingComponent is NORGate || existingComponent is XORGate || existingComponent is XNORGate)
                {
                    CheckAndRemove(cellPosition);
                    CheckAndRemove(belowCellPosition);
                    CheckAndRemove(underBelowCellPosition);

                    tilemap.SetTile(cellPosition, blankTile);
                    tilemap.SetTile(belowCellPosition, blankTile);
                    tilemap.SetTile(underBelowCellPosition, blankTile);
                }
                else 
                {   
                    CheckAndRemove(cellPosition);
                    tilemap.SetTile(cellPosition, blankTile);
                }
            }
            
        }

        if (pushButtonPlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (components.TryGetValue(cellPosition, out ElectricalComponent existingComponent))
            {
                if (existingComponent is ANDGate || existingComponent is NANDGate || existingComponent is ORGate || existingComponent is NORGate || existingComponent is XORGate || existingComponent is XNORGate)
                {
                    CheckAndRemove(cellPosition);
                    CheckAndRemove(belowCellPosition);
                    CheckAndRemove(underBelowCellPosition);

                    tilemap.SetTile(cellPosition, blankTile);
                    tilemap.SetTile(belowCellPosition, blankTile);
                    tilemap.SetTile(underBelowCellPosition, blankTile);
                }
                else 
                {   
                    CheckAndRemove(cellPosition);
                }
            }

            PushButton newPushButton = new PushButton(tilemap, cellPosition);
            components[cellPosition] = newPushButton;
            tilemap.SetTile(cellPosition, pushButtonUnpressedTile);

            pushButtonList.Add(newPushButton);
        }

        if (switchPlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (components.TryGetValue(cellPosition, out ElectricalComponent existingComponent))
            {
                if (existingComponent is ANDGate || existingComponent is NANDGate || existingComponent is ORGate || existingComponent is NORGate || existingComponent is XORGate || existingComponent is XNORGate)
                {
                    CheckAndRemove(cellPosition);
                    CheckAndRemove(belowCellPosition);
                    CheckAndRemove(underBelowCellPosition);

                    tilemap.SetTile(cellPosition, blankTile);
                    tilemap.SetTile(belowCellPosition, blankTile);
                    tilemap.SetTile(underBelowCellPosition, blankTile);
                }
                else 
                {   
                    CheckAndRemove(cellPosition);
                }
            }

            Switch newSwitch = new Switch(tilemap, cellPosition);
            components[cellPosition] = newSwitch;
            tilemap.SetTile(cellPosition, Switch1);

            switchList.Add(newSwitch);
        }

        if (diodePlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (components.TryGetValue(cellPosition, out ElectricalComponent existingComponent))
            {
                if (existingComponent is ANDGate || existingComponent is NANDGate || existingComponent is ORGate || existingComponent is NORGate || existingComponent is XORGate || existingComponent is XNORGate)
                {
                    CheckAndRemove(cellPosition);
                    CheckAndRemove(belowCellPosition);
                    CheckAndRemove(underBelowCellPosition);

                    tilemap.SetTile(cellPosition, blankTile);
                    tilemap.SetTile(belowCellPosition, blankTile);
                    tilemap.SetTile(underBelowCellPosition, blankTile);
                }
                else 
                {   
                    CheckAndRemove(cellPosition);
                }
            }

            Diode newDiode = new Diode(tilemap, cellPosition, tilemapClickInstance: this);
            components[cellPosition] = newDiode;
            //tilemap.SetTile(cellPosition, diodeTile);

            diodeList.Add(newDiode);
        }

        if (ANDGatePlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {   
            CheckAndRemove(cellPosition);
            CheckAndRemove(belowCellPosition);
            CheckAndRemove(underBelowCellPosition);

            ANDGate newANDGate = new ANDGate(tilemap, cellPosition, tilemapClickInstance: this);
            components[cellPosition] = newANDGate;

            ANDGateList.Add(newANDGate);
        }

        if (NANDGatePlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {   
            CheckAndRemove(cellPosition);
            CheckAndRemove(belowCellPosition);
            CheckAndRemove(underBelowCellPosition);

            NANDGate newNANDGate = new NANDGate(tilemap, cellPosition, tilemapClickInstance: this);
            components[cellPosition] = newNANDGate;

            NANDGateList.Add(newNANDGate);
        }

        if (ORGatePlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {   
            CheckAndRemove(cellPosition);
            CheckAndRemove(belowCellPosition);
            CheckAndRemove(underBelowCellPosition);

            ORGate newORGate = new ORGate(tilemap, cellPosition, tilemapClickInstance: this);
            components[cellPosition] = newORGate;

            ORGateList.Add(newORGate);
        }

        if (NORGatePlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {   
            CheckAndRemove(cellPosition);
            CheckAndRemove(belowCellPosition);
            CheckAndRemove(underBelowCellPosition);

            NORGate newNORGate = new NORGate(tilemap, cellPosition, tilemapClickInstance: this);
            components[cellPosition] = newNORGate;

            NORGateList.Add(newNORGate);
        }

        if (XORGatePlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {   
            CheckAndRemove(cellPosition);
            CheckAndRemove(belowCellPosition);
            CheckAndRemove(underBelowCellPosition);

            XORGate newXORGate = new XORGate(tilemap, cellPosition, tilemapClickInstance: this);
            components[cellPosition] = newXORGate;

            XORGateList.Add(newXORGate);
        }

        if (XNORGatePlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {   
            CheckAndRemove(cellPosition);
            CheckAndRemove(belowCellPosition);
            CheckAndRemove(underBelowCellPosition);

            XNORGate newXNORGate = new XNORGate(tilemap, cellPosition, tilemapClickInstance: this);
            components[cellPosition] = newXNORGate;

            XNORGateList.Add(newXNORGate);
        }

        if (NOTGatePlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (components.TryGetValue(cellPosition, out ElectricalComponent existingComponent))
            {
                if (existingComponent is ANDGate || existingComponent is NANDGate || existingComponent is ORGate || existingComponent is NORGate || existingComponent is XORGate || existingComponent is XNORGate)
                {
                    CheckAndRemove(cellPosition);
                    CheckAndRemove(belowCellPosition);
                    CheckAndRemove(underBelowCellPosition);

                    tilemap.SetTile(cellPosition, blankTile);
                    tilemap.SetTile(belowCellPosition, blankTile);
                    tilemap.SetTile(underBelowCellPosition, blankTile);
                }
                else 
                {   
                    CheckAndRemove(cellPosition);
                }
            }

            NOTGate newNOTGate = new NOTGate(tilemap, cellPosition, tilemapClickInstance: this);
            components[cellPosition] = newNOTGate;

            NOTGateList.Add(newNOTGate);
        }

        if (groundPlacement == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (components.TryGetValue(cellPosition, out ElectricalComponent existingComponent))
            {
                if (existingComponent is ANDGate || existingComponent is NANDGate || existingComponent is ORGate || existingComponent is NORGate || existingComponent is XORGate || existingComponent is XNORGate)
                {
                    CheckAndRemove(cellPosition);
                    CheckAndRemove(belowCellPosition);
                    CheckAndRemove(underBelowCellPosition);

                    tilemap.SetTile(cellPosition, blankTile);
                    tilemap.SetTile(belowCellPosition, blankTile);
                    tilemap.SetTile(underBelowCellPosition, blankTile);
                }
                else 
                {   
                    CheckAndRemove(cellPosition);
                }
            }

            Ground newGround = new Ground(tilemap, cellPosition);
            components[cellPosition] = newGround;

            // Set the tile at the given position to the rule tile
            tilemap.SetTile(cellPosition, groundTile);

            groundList.Add(newGround);
        }

        if (placingWires && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (components.TryGetValue(cellPosition, out ElectricalComponent existingComponent))
            {
                if (existingComponent is ANDGate || existingComponent is NANDGate || existingComponent is ORGate || existingComponent is NORGate || existingComponent is XORGate || existingComponent is XNORGate)
                {
                    CheckAndRemove(cellPosition);
                    CheckAndRemove(belowCellPosition);
                    CheckAndRemove(underBelowCellPosition);

                    tilemap.SetTile(cellPosition, blankTile);
                    tilemap.SetTile(belowCellPosition, blankTile);
                    tilemap.SetTile(underBelowCellPosition, blankTile);
                }
                else 
                {   
                    CheckAndRemove(cellPosition);
                }
            }

            Wire newWire = new Wire(tilemap, cellPosition, this);
            components.Add(cellPosition, newWire);
            tilemap.SetTile(cellPosition, wireTile);

            wireList.Add(newWire);
        }

        if (movingToolPick && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (components.TryGetValue(cellPosition, out ElectricalComponent existingComponent))
            {
                if (existingComponent is Diode)
                {
                    diodePlacement = true;
                } else if(existingComponent is PushButton)
                {
                    pushButtonPlacement = true;
                } else if (existingComponent is Ground)
                {
                    groundPlacement = true;
                }
                else if (existingComponent is ANDGate)
                {
                    ANDGatePlacement = true;
                }
                else if (existingComponent is NANDGate)
                {
                    NANDGatePlacement = true;
                }
                else if (existingComponent is NOTGate)
                {
                    NOTGatePlacement = true;
                }
                else if (existingComponent is ORGate)
                {
                    ORGatePlacement = true;
                }
                else if (existingComponent is NORGate)
                {
                    NORGatePlacement = true;
                }
                else if (existingComponent is XORGate)
                {
                    XORGatePlacement = true;
                }
                else if (existingComponent is XNORGate)
                {
                    XNORGatePlacement = true;
                }
                else if (existingComponent is Wire)
                {
                    placingWires = true;
                }
                else if (existingComponent is Switch)
                {
                    switchPlacement = true;
                }
            }
            
            if (existingComponent is ANDGate || existingComponent is NANDGate || existingComponent is ORGate || existingComponent is NORGate || existingComponent is XORGate || existingComponent is XNORGate)
                {
                    CheckAndRemove(cellPosition);
                    CheckAndRemove(belowCellPosition);
                    CheckAndRemove(underBelowCellPosition);

                    tilemap.SetTile(cellPosition, blankTile);
                    tilemap.SetTile(belowCellPosition, blankTile);
                    tilemap.SetTile(underBelowCellPosition, blankTile);
                }
                else 
                {   
                    CheckAndRemove(cellPosition);
                    tilemap.SetTile(cellPosition, blankTile);
                }

            movingToolPlace = true;
            movingToolPick = false;
        }
        else if (movingToolPlace && Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log("Im here!");
            placingWires = false;
            deleteComponent = false;
            pushButtonPlacement = false;
            NOTGatePlacement = false;
            diodePlacement = false;
            groundPlacement = false;
            ANDGatePlacement = false;
            NANDGatePlacement = false;
            ORGatePlacement = false;
            NORGatePlacement = false;
            XORGatePlacement = false;
            XNORGatePlacement = false;
            switchPlacement = false;

            movingToolPlace = false;
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

    void switchClick(Vector3Int mousePosition)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (components.TryGetValue(mousePosition, out ElectricalComponent component) && component is Switch switchButton)
            {
                if(switchButton.isPressed == false)
                {
                    tilemap.SetTile(switchButton.position, Swtich2);
                    switchButton.isPressed = true;
                }else
                {
                    tilemap.SetTile(switchButton.position, Switch1);
                    switchButton.isPressed = false;
                }
            }
        }
    }

    private void UpdateAllComponents()
    {
        Dictionary<Vector3Int, ElectricalComponent> components = new Dictionary<Vector3Int, ElectricalComponent>();
        foreach (Wire wire in wireList) components.Add(wire.position, wire);
        foreach (PushButton button in pushButtonList) components.Add(button.position, button);
        foreach (Switch swtichButton in switchList) components.Add(swtichButton.position, swtichButton);
        foreach (Diode diode in diodeList) components.Add(diode.position, diode);
        foreach (Ground ground in groundList) components.Add(ground.position, ground);
        foreach (ANDGate andGate in ANDGateList) components.Add(andGate.position, andGate);
        foreach (NANDGate nandGate in NANDGateList) components.Add(nandGate.position, nandGate);
        foreach (NOTGate notGate in NOTGateList) components.Add(notGate.position, notGate);
        foreach (ORGate orGate in ORGateList) components.Add(orGate.position, orGate);
        foreach (NORGate norGate in NORGateList) components.Add(norGate.position, norGate);
        foreach (XORGate xorGate in XORGateList) components.Add(xorGate.position, xorGate);
        foreach (XNORGate xnorGate in XNORGateList) components.Add(xnorGate.position, xnorGate);

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
            else if (existingComponent is ANDGate)
            {
                ANDGateList.Remove((ANDGate)existingComponent);
            }
            else if (existingComponent is NANDGate)
            {
                NANDGateList.Remove((NANDGate)existingComponent);
            }
            else if (existingComponent is NOTGate)
            {
                NOTGateList.Remove((NOTGate)existingComponent);
            }
            else if (existingComponent is ORGate)
            {
                ORGateList.Remove((ORGate)existingComponent);
            }
            else if (existingComponent is NORGate)
            {
                NORGateList.Remove((NORGate)existingComponent);
            }
            else if (existingComponent is XORGate)
            {
                XORGateList.Remove((XORGate)existingComponent);
            }
            else if (existingComponent is XNORGate)
            {
                XNORGateList.Remove((XNORGate)existingComponent);
            }
            else if (existingComponent is Switch)
            {
                switchList.Remove((Switch)existingComponent);
            }

            components.Remove(position);
        }
    }
}