using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InGameEntity : MonoBehaviour {
    public Transform entitySprite;
[SerializeField]
    private Direction _facingDirection;
    public Direction facingDirection {
        get => _facingDirection;
        set {
            _facingDirection = value;
            RotateSprite (_facingDirection);
        }
    }

    void RotateSprite (Direction newFacingDirection) {
        switch (newFacingDirection) {
            case Direction.UP:
                entitySprite.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.DOWN:
                entitySprite.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.RIGHT:
                entitySprite.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.LEFT:
                entitySprite.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
    }
}