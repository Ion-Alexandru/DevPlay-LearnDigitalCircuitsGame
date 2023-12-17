using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TileHighlighterScript : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase highlightTile;
    private Vector3Int previousTilePosition;

    void Update()
    {
        // Check if the mouse is not over a UI element
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = tilemap.WorldToCell(mousePosition);

            if (tilePosition != previousTilePosition)
            {
                // Remove highlight from the previous tile
                tilemap.SetTile(previousTilePosition, null);

                // Highlight the current tile
                tilemap.SetTile(tilePosition, highlightTile);

                previousTilePosition = tilePosition;
            }
        }
    }
}