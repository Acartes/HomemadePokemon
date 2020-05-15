using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InfiniteTile : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tile;
    // Start is called before the first frame update
    void OnEnable()
    {
        RemoveAll();
        tile.SetActive(true);
     for (int i = 0; i < width; i++)
     {
         for (int j = 0; j < height; j++)
         {
            GameObject newObj = Instantiate(tile, new Vector3(i, j, 0), Quaternion.identity, transform);   
         }
     }
        tile.SetActive(false);
     enabled = false;
    }

    void RemoveAll(){
        while (transform.childCount != 1){
            DestroyImmediate(transform.GetChild(1).gameObject);
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
         var fps = 1.0f / Time.deltaTime;
         if(fps < 5) {
             Debug.Break();
         }
     }
}
