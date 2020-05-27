using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public enum Direction {
    UP,
    DOWN,
    RIGHT,
    LEFT,
    STOP
}

public enum Action {
    MAIN_ACTION
}

[System.Serializable]
public class InputEvent_Direction : UnityEvent<Direction>
{
}
[System.Serializable]
public class InputEvent_Action : UnityEvent<Action>
{
}
[System.Serializable]
public class InputEvent_StopDirection : UnityEvent
{
}

public class InputEvent : MonoBehaviour
{

    public InputEvent_Direction inputEvent_Direction;
    public InputEvent_Action inputEvent_Action;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)){
            inputEvent_Direction.Invoke(Direction.UP);
        }   
        if(Input.GetKey(KeyCode.DownArrow)){
            inputEvent_Direction.Invoke(Direction.DOWN);
        }   
        if(Input.GetKey(KeyCode.RightArrow)){
            inputEvent_Direction.Invoke(Direction.RIGHT);
        }   
        if(Input.GetKey(KeyCode.LeftArrow)){
            inputEvent_Direction.Invoke(Direction.LEFT);
        }
        if(AnyArrowUp()){
            inputEvent_Direction.Invoke(Direction.STOP);
        }
        if(Input.GetKey(KeyCode.Space)){
            inputEvent_Action.Invoke(Action.MAIN_ACTION);
        }
    }

    bool AnyArrowUp(){
        return Input.GetKeyUp(KeyCode.UpArrow)||Input.GetKeyUp(KeyCode.DownArrow)||Input.GetKeyUp(KeyCode.RightArrow)||Input.GetKeyUp(KeyCode.LeftArrow);
    }
}
