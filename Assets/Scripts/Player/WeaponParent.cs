using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponParent : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1000f;

    private Camera mainCamera;
    private PlayerInput playerInput;

    private Vector2 aimInput;

    private void Awake()
    {
        mainCamera = Camera.main;
        playerInput = GetComponentInParent<PlayerInput>(); // Look for the PlayerInput component in the parent

        // Register the input action callback
        playerInput.actions["WeaponPosition"].performed += OnAimInput;
    }

    private void OnAimInput(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (playerInput.currentControlScheme == "Keyboard&Mouse" || playerInput.currentControlScheme == "Gamepad")
        {
            // Normalize the input and then clamp it within a reasonable range
            Vector2 inputDirection = aimInput.normalized;
            inputDirection.x = Mathf.Clamp(inputDirection.x, -1f, 1f);
            inputDirection.y = Mathf.Clamp(inputDirection.y, -1f, 1f);

            if (inputDirection != Vector2.zero)
            {
                // Calculate the angle based on the input direction
                float targetRotation = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;

                // Smoothly rotate the weapon toward the target rotation
                float currentRotation = transform.eulerAngles.z;
                float newRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, newRotation);
            }
        }
        else
        {
            // Handle other control schemes or default behavior
        }
    }
}