using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Flip();

    }

    private void Movement()
    {
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
        rb.velocity = smoothedMovementInput * playerSpeed;
    }

    private void Flip()
    {
        if (rb.velocity.x >= 0)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }

    private void OnMove(InputValue inputValue) 
    {
       movementInput =  inputValue.Get<Vector2>();
    }

}

