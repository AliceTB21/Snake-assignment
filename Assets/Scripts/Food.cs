using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Pickup
{
    protected override void TriggerEffect(Collider2D collider) // Adds a part to the snake and incrases score
    {
        SnakeGameManager.Instance.SpawnFood();
        collider.GetComponentInParent<Snake>().AddPart();
        SnakeGameManager.Instance.AddScore(score);
        Destroy(gameObject);
    }
}
