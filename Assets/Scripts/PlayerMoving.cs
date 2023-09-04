using UnityEngine;

public class PlayerMoving : Moving
{

    private void Start()
    {
        moveDirection = Vector3.up;
        newMoveDirection = Vector3.up;
    }
    public void TurnLeft()
    {
        if (moveDirection == Vector3.up) { newMoveDirection = Vector3.left; }
        else if (moveDirection == Vector3.left) { newMoveDirection = Vector3.down; }
        else if (moveDirection == Vector3.down) { newMoveDirection = Vector3.right; }
        else if (moveDirection == Vector3.right) { newMoveDirection = Vector3.up; }

    }
    public void TurnRight()
    {
        if (moveDirection == Vector3.right) { newMoveDirection = Vector3.down; }
        else if (moveDirection == Vector3.down) { newMoveDirection = Vector3.left; }
        else if (moveDirection == Vector3.left) { newMoveDirection = Vector3.up; }
        else if (moveDirection == Vector3.up) { newMoveDirection = Vector3.right; }
    }
}
