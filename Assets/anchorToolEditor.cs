using System.Collections;
using System.Collections.Generic;
using UnityEngine;

     using UnityEditor;
     class anchorToolEditor : Editor {
         void OnSceneGUI() {
             Debug.Log(Input.mousePosition);
         }
     }
