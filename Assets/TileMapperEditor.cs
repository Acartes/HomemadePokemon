using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (TileMapper))]
public class TileMapperEditor : Editor {

    public int widthPadding = 20;

    void OnSceneGUI () {
        List<GameObject> buttons = (target as TileMapper).availableTiles;

        Handles.BeginGUI ();

        int letterwidth = 0;
        foreach (var item in buttons) {
            if (letterwidth < item.name.Length)
                letterwidth = item.name.Length;
        }
        GUILayout.BeginArea (new Rect (20, 20, widthPadding + letterwidth * 8, buttons.Count * 70));

        var rect = EditorGUILayout.BeginVertical ();
        GUI.color = Color.yellow;
        GUI.Box (rect, GUIContent.none);

        GUI.color = Color.white;

        GUILayout.BeginHorizontal ();
        GUILayout.FlexibleSpace ();
        GUILayout.Label ("Tiles");
        GUILayout.FlexibleSpace ();
        GUILayout.EndHorizontal ();
        for (int i = 0; i < buttons.Count; i++) {
            if (GUILayout.Button (buttons[i].name)) {
                Debug.Log (buttons[i]);
                (target as TileMapper).selectedTile = buttons[i];
            }
        }

        GUILayout.BeginHorizontal ();
        GUI.backgroundColor = Color.red;

        GUILayout.EndHorizontal ();

        EditorGUILayout.EndVertical ();

        GUILayout.EndArea ();

        Handles.EndGUI ();

        Event e = Event.current;
        switch (e.type) {
            case EventType.KeyDown:
                {
                    if (e.keyCode == (KeyCode.Q)) {
                    (target as TileMapper).delete = false;
                        (target as TileMapper).draw = !(target as TileMapper).draw;
                    }
                    if (e.keyCode == (KeyCode.W)) {
                                            (target as TileMapper).draw = false;
                        (target as TileMapper).delete = !(target as TileMapper).delete;
                    }
                    break;
                }
        }
    }
}