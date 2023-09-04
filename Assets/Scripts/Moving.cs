using UnityEngine;

public class Moving : MonoBehaviour
{
    public float moveSpeed = 1.0f; // Скорость движения змейки

    protected Vector3 moveDirection;
    protected Vector3 newMoveDirection = Vector3.up;

    public virtual void Move()
    {
        moveDirection = newMoveDirection;
        transform.Translate(moveDirection * moveSpeed);
    }
}
