using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRules : MonoBehaviour
{
    public static GridRules Instance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance = this;
    }

    public List<Vector3> blockedPosition = new List<Vector3>();

    public void blockPosition(int width, int height){
        blockedPosition.Add(new Vector3(width, height, 0));
    }

    public void unblockPosition(int width, int height){
        blockedPosition.Remove(new Vector3(width, height, 0));
    }
}
