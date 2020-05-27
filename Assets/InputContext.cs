using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAMESTATE{
    WORLDMAP,
}

public class InputContext : MonoBehaviour
{
    public InputEvent inputEvent;
    public GAMESTATE gameState;


    [Header("World Map movement")]
    public InGameEntity Character;

    public MoveEvent moveEvent;

    public MapInteractionEvent MapInteractionEvent;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        inputEvent.inputEvent_Direction.AddListener(UpdateGame);
        inputEvent.inputEvent_Action.AddListener(UpdateGame);
    }

    // Update is called once per frame
    void UpdateGame(Direction a)
    {
        switch (gameState)
        {
            case GAMESTATE.WORLDMAP :
                WorldMapContext(a);
            break;
        }
    }
    void UpdateGame(Action a)
    {
        switch (gameState)
        {
            case GAMESTATE.WORLDMAP :
                WorldMapContext(a);
            break;
        }
    }

    void WorldMapContext(Direction direction){
            moveEvent.Move(Character, direction);
    }
    void WorldMapContext(Action action){
            MapInteractionEvent.Action(action);
    }
}
