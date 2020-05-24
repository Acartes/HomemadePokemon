using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileMapper : MonoBehaviour {

    public bool activate;
    public bool draw;
    public bool delete;

    public List<GameObject> tilesInGame = new List<GameObject> ();
    public List<GameObject> availableTiles = new List<GameObject> ();

    public GameObject selectedTile;
    GameObject tileToDelete;

    public Vector3 position;

    void OnDrawGizmos () {
        if (!activate)
            return;

        Selection.activeGameObject = gameObject;

        Test ();
        Vector3 newPos = GetMousePosition ();
        newPos = new Vector3 (newPos.x, newPos.y, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawCube (GridPos (newPos), Vector3.one * .5f);
        if (draw) {
            foreach (var item in tilesInGame) {
                if (item.transform.position == GridPos (newPos)) {
                    return;
                }
            }
            GameObject a = Instantiate (selectedTile, GridPos (newPos), Quaternion.identity, transform);
            tilesInGame.Add (a);
        }
        if (delete) {
            foreach (var item in tilesInGame) {
                if (item.transform.position == GridPos (newPos)) {
                    tileToDelete = item;
                }
            }
        }
        if (tileToDelete)
            DeleteTile ();
    }

    void DeleteTile () {
        tilesInGame.Remove (tileToDelete);
        DestroyImmediate (tileToDelete);
    }

    void Test () {
        if (Event.current.type == EventType.MouseDrag) {
            Debug.Log ("up");
        }
    }

    Vector3 GetMousePosition () {
        return HandleUtility.GUIPointToWorldRay (Event.current.mousePosition).origin;
    }

    Vector3 GridPos (Vector3 toGrid) {
        int gridX = Mathf.RoundToInt (toGrid.x);
        int gridY = Mathf.RoundToInt (toGrid.y);
        int gridZ = Mathf.RoundToInt (toGrid.z);
        return new Vector3 (gridX, gridY, gridZ);
    }

}