using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed;

    [SerializeField]
    private float screenBorder;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Camera cam;

    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
        sr = GetComponentInChildren<SpriteRenderer>();
        cam = Camera.main;
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

        PreventPlayerGoingOffScreen();
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

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPosition = cam.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < screenBorder && rb.velocity.x < 0) || (screenPosition.x > cam.pixelWidth - screenBorder && rb.velocity.x > 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if ((screenPosition.y < screenBorder && rb.velocity.y < 0) || (screenPosition.y > cam.pixelHeight - screenBorder && rb.velocity.y > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}

