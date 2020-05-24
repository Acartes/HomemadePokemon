using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GridMap gridMap;

    public void SpawnElement(GridElement gridElement, int width, int height){
        GameObject grid = gridMap.getGrid(width, height);
        gridElement.transform.position = grid.transform.position;
    }
}
