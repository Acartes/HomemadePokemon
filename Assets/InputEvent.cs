using System.Collections;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class InputEvent_Direction : UnityEvent<Direction>
{
}
[System.Serializable]
public class InputEvent_StopDirection : UnityEvent
{
}

public class InputEvent : MonoBehaviour
{
    public InputEvent_Direction inputEvent_Direction;
    public InputEvent_StopDirection inputEvent_StopDirection;
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
    }

    bool AnyArrowUp(){
        return Input.GetKeyUp(KeyCode.UpArrow)||Input.GetKeyUp(KeyCode.DownArrow)||Input.GetKeyUp(KeyCode.RightArrow)||Input.GetKeyUp(KeyCode.LeftArrow);
    }
}
