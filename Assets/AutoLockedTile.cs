﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLockedTile : MonoBehaviour
{
    public GridRules gridRules;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gridRules.blockPosition((int)transform.position.x, (int)transform.position.y);
    }
}
