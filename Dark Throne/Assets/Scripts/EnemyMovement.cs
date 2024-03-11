using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 10f;
    public Transform targetPlayer;
    public float groundCheckDistance = 1.5f; 
    public LayerMask groundLayer; 
    public float minimumGroundAdjustDistance = 0.1f; 

    void Update()
    {
        if (targetPlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
            if (distanceToPlayer <= detectionRange)
            {
                Vector3 moveDirection = (targetPlayer.position - transform.position).normalized;
                Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
                
                newPosition = AdjustPositionToGround(newPosition, out bool isGrounded);

                if (isGrounded)
                {
                    transform.position = newPosition;
                }
                else
                {
                    transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
                }
            }
        }
    }

    Vector3 AdjustPositionToGround(Vector3 proposedPosition, out bool isGrounded)
    {
        RaycastHit hit;
        isGrounded = false;
        if (Physics.Raycast(proposedPosition + Vector3.up * 0.1f, Vector3.down, out hit, groundCheckDistance + 0.1f, groundLayer))
        {
            float distanceToGround = hit.distance - 0.1f; 
            if (distanceToGround <= minimumGroundAdjustDistance)
            {
                isGrounded = true;
                proposedPosition.y = hit.point.y + minimumGroundAdjustDistance; 
            }
        }
        return proposedPosition;
    }
}
