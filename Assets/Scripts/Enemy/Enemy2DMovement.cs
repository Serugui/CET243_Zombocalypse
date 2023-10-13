using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2DMovement : MonoBehaviour
{
    
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;
    private Transform spriteTransform;
    private PlayerAwarenessController playerAwarenessController;
    private Vector2 targetDirection;
    private Quaternion spriteInitialRotation; 

    private void Awake()
    {
        rb = GetComponent <Rigidbody2D>();
        spriteTransform = transform.GetChild(0); 
        spriteInitialRotation = spriteTransform.rotation; 
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        Flip();
        ChasePlayer();
    }

    private void UpdateTargetDirection()
    {
        if (playerAwarenessController.AwareOfPlayer)
        {
            targetDirection = playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            targetDirection = Vector2.zero;
        }
    }

    private void Flip()
    {
        if (rb.velocity.x >= 0)
        {
            spriteTransform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            spriteTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void ChasePlayer()
    {
        if (targetDirection == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = targetDirection * speed;
            RotateTowardsTarget(targetDirection);
        }
    }

    private void RotateTowardsTarget(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        spriteTransform.rotation = spriteInitialRotation; 
    }

}

