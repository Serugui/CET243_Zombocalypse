using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2DMovement : MonoBehaviour
{
    
    [SerializeField]
    private float speed;
    private float changeDirectionCooldown;

    [SerializeField]
    private float screenBorder;

    private Rigidbody2D rb;
    private Transform spriteTransform;
    private Camera cam;
    private PlayerAwarenessController playerAwarenessController;
    private Vector2 targetDirection;
    private Quaternion spriteInitialRotation; 

    private void Awake()
    {
        rb = GetComponent <Rigidbody2D>();
        spriteTransform = transform.GetChild(0); 
        spriteInitialRotation = spriteTransform.rotation; 
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        targetDirection = transform.up;
        cam = Camera.main;

    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        Flip();
        ChasePlayer();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();

        HandlePlayerTargeting();
        
        HandleEnemyOffScreen();
    }

    private void HandleEnemyOffScreen()
    {
        Vector2 screenPosition = cam.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < screenBorder && targetDirection.x < 0) || (screenPosition.x > cam.pixelWidth - screenBorder && targetDirection.x > 0))
        {
            targetDirection = new Vector2(-targetDirection.x, targetDirection.y);
        }

        if ((screenPosition.y < screenBorder && targetDirection.y < 0) || (screenPosition.y > cam.pixelHeight - screenBorder && targetDirection.y > 0))
        {
            targetDirection = new Vector2(targetDirection.x, -targetDirection.y);
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

    private void HandleRandomDirectionChange()
    {
        changeDirectionCooldown -= Time.deltaTime;

        if (changeDirectionCooldown <= 0 )
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            targetDirection = rotation * targetDirection;

            changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
        if (playerAwarenessController.AwareOfPlayer)
        {
            targetDirection = playerAwarenessController.DirectionToPlayer;
        }
    }
    private void ChasePlayer()
    {
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

