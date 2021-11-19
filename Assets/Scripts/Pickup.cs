using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected int score;
    private void OnTriggerEnter2D(Collider2D collision) // Adds a part to the snake and destroys itself
    {
        TriggerEffect(collision);
    }

    protected virtual void TriggerEffect(Collider2D collider) // Overridable to handle different items
    {

    }
}
