using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Pickup
{
    [Range(1,20)]
    float minLifeTime = 5f;
    [Range(1, 20)]
    float maxLifeTime = 15f;

    float lifeTime;

    protected override void TriggerEffect(Collider2D collider)
    {
        //collider.GetComponentInParent<Snake>().DestroyParts(); // Disabled until I can figure out how to stop parts from moving with the snake
        SnakeGameManager.Instance.AddScore(-score);
        Destroy(gameObject);
    }

    private void Start()
    {
        LifeTime();
        Destroy(gameObject, lifeTime);
    }

    private void LifeTime() // Destroys the bomb after a random amount of time if not picked up
    {
        lifeTime = Random.Range(minLifeTime, maxLifeTime);
    }
}
