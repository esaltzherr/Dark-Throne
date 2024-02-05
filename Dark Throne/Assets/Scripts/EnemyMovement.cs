using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;  // Speed at which the enemy moves towards the player
    public Transform targetPlayer; // Reference to the player's transform

    void Update()
    {
        if (targetPlayer != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 moveDirection = (targetPlayer.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}
