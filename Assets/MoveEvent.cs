using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum Direction{
        UP,
        DOWN,
        RIGHT,
        LEFT,
        STOP
    }

public class MoveStackArguments{
    public MoveStackArguments(Transform _toMove, Direction _direction){
        toMove = _toMove;
        direction = _direction;
    }
    public Transform toMove;
    public Direction direction;
}

public class MoveEvent : MonoBehaviour
{

    Stack<MoveStackArguments> moveStack = new Stack<MoveStackArguments>();
    public int steps = 20;
    bool lockedAction;

    public void Move(Transform toMove, Direction direction){
        moveStack.Push(new MoveStackArguments(toMove, direction));
    }

    public void StopMove(){
        moveStack.Clear();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update(){
        CheckStacks();
    }

    void CheckStacks(){ //Updated function
        if(moveStack.Count != 0){
            if(!lockedAction){
                StartCoroutine(MoveAction(moveStack.Pop()));
            }
        }
    }

    IEnumerator MoveAction(MoveStackArguments arguments){
        Direction direction = arguments.direction;
        Transform toMove = arguments.toMove;

        lockedAction = true;
        Vector3 vectorDirection = Vector3.zero;
        switch (direction)
        {
          case Direction.UP:
              vectorDirection = new Vector3(0, 1, 0);
              break;
          case Direction.DOWN:
              vectorDirection = new Vector3(0, -1, 0);
              break;
          case Direction.RIGHT:
              vectorDirection = new Vector3(1, 0, 0);
              break;
          case Direction.LEFT:
              vectorDirection = new Vector3(-1, 0, 0);
              break;
          case Direction.STOP:
              moveStack.Clear();
              Debug.Log("a");
              yield return null;
              break;
        }

        Vector3 finalPosition = vectorDirection + arguments.toMove.position;

        for(int i = 0; i < steps; i++){
            toMove.position+=vectorDirection/steps;
            yield return new WaitForEndOfFrame();
        }
        toMove.position = finalPosition;

        lockedAction = false;
    }
}
