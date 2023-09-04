using UnityEngine;

public class SnakeSegment : Moving
{
    public Transform targetSegment;


    public override void Move()
    {
        if (targetSegment != null && Vector2Int.FloorToInt(transform.position) != Vector2Int.FloorToInt(targetSegment.position))
        {
            newMoveDirection = targetSegment.position - transform.position;
            base.Move();
        }
    }

}