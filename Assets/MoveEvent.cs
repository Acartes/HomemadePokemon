using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    UP,
    DOWN,
    RIGHT,
    LEFT,
    STOP
}

[Serializable]
public class MoveStackArguments {
    public MoveStackArguments (Transform _toMove, Direction _direction) {
        toMove = _toMove;
        direction = _direction;
    }
    public Transform toMove;
    public Direction direction;
}

public class MoveEvent : MonoBehaviour {

    [SerializeField]
    public List<MoveStackArguments> moveStack = new List<MoveStackArguments> ();
    public int steps = 20;
    bool lockedAction;

    public void Move (Transform toMove, Direction direction, bool forced = false) {
        if ((moveStack.Count > 0 || lockedAction) && !forced)
            return;
        moveStack.Add (new MoveStackArguments (toMove, direction));
    }

    public void StopMove () {
        moveStack.Clear ();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update () {
        CheckStacks ();
    }

    void CheckStacks () { //Updated function
        if(moveStack.Count > 0 && !lockedAction){
            StartCoroutine (MoveAction (moveStack.PeekPop ()));
        }
    }

    IEnumerator MoveAction (MoveStackArguments arguments) {
        Direction direction = arguments.direction;
        Transform toMove = arguments.toMove;

        lockedAction = true;

        Vector3 vectorDirection = Vector3.zero;
        switch (direction) {
            case Direction.UP:
                vectorDirection = new Vector3 (0, 1, 0);
                break;
            case Direction.DOWN:
                vectorDirection = new Vector3 (0, -1, 0);
                break;
            case Direction.RIGHT:
                vectorDirection = new Vector3 (1, 0, 0);
                break;
            case Direction.LEFT:
                vectorDirection = new Vector3 (-1, 0, 0);
                break;
            case Direction.STOP:
                moveStack.Clear ();
                yield return null;
                break;
        }

        Vector3 basePosition = arguments.toMove.position;
        Vector3 finalPosition = vectorDirection + arguments.toMove.position;

        if (GridRules.Instance.blockedPosition.Contains (finalPosition)) {
            Debug.Log ("DUNG");
            lockedAction = false;
            yield break;
        }

        GridRules.Instance.blockPosition ((int) finalPosition.x, (int) finalPosition.y);
        for (int i = 0; i < steps; i++) {
            toMove.position += vectorDirection / steps;
            yield return new WaitForEndOfFrame ();
        }
        GridRules.Instance.unblockPosition ((int) basePosition.x, (int) basePosition.y);
        toMove.position = finalPosition;

        lockedAction = false;
    }
}

public static class MyExtensions {
    public static T PeekPop<T> (this List<T> list) {
        if (list.Count == 0)
            return default (T);
        // omits validation, etc.
        T item = list[0];
        list.RemoveAt (0);
        return item;
    }
}