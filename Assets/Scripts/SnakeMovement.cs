using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public void MoveHead(Vector3 newPos) // Moves the head depending on position
    {
        transform.position += newPos;
    }
    public void MoveBody(Vector3 newPos) // Moves the body to the position of the part in front
    {
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D other) // Check if head is colliding with body
    {
        if(other.CompareTag("Head"))
        {
            GetComponentInParent<Snake>().Death();
        }
    }
}
