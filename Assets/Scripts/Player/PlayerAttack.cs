using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObject; // Reference to the enemy GameObject

    public InputActionReference fireAction; // Reference to the fire action

    private void OnEnable()
    {
        fireAction.action.performed += Fire;
    }

    private void OnDisable()
    {
        fireAction.action.performed -= Fire;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        // Check if the weapon has collided with the enemy's collider
        if (enemyObject != null && IsCollidingWithEnemy())
        {
            KillEnemy();
        }
    }

    private bool IsCollidingWithEnemy()
    {
        // Check for collision with the enemy's collider
        // You can use any method to check for collision, such as OnTrigger or OnCollision
        // For example, if the weapon uses a collider and rigidbody, you can use OnTriggerEnter.
        // Make sure the enemy's collider is set up as a trigger as well.

        Collider[] colliders = Physics.OverlapBox(
            enemyObject.transform.position,
            enemyObject.transform.localScale / 2f,
            enemyObject.transform.rotation,
            LayerMask.GetMask("EnemyLayer")
        );

        return colliders.Length > 0;
    }

    private void KillEnemy()
    {
        // Implement logic to kill the enemy here
        // You can destroy the enemy game object or deactivate it, for example.
        Destroy(enemyObject);
    }

}
