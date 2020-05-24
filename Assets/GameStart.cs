using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject character;
    public int width;
    public int height;


    // Start is called before the first frame update
    void Start()
    {
        character.transform.position = new Vector3(width, height, 0);
    }
}
