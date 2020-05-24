using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour {
    public GameObject[, ] grid;
    public GameObject basicBox;

    public int gridWidth;
    public int gridHeight;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Awake () {
        GenerateGrid ();
    }

    void GenerateGrid () {
        grid = new GameObject[gridWidth, gridHeight];
        basicBox.SetActive (true);
        for (int i = 0; i < gridWidth; i++) {
            for (int j = 0; j < gridHeight; j++) {
                GameObject a = Instantiate (basicBox, new Vector3 (i, j, 0), Quaternion.identity, transform);
                grid[i, j] = a;
            }
        }
        basicBox.SetActive (false);
    }

    public GameObject getGrid (int width, int height) {
        return grid[width, height];
    }
    public GameObject getRelativeGrid (GridElement gridElement, Direction direction) {

        return null;
    }
}