using UnityEngine;
using System.Collections.Generic;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float playerAwarenessDistance;

    private List<Transform> players = new List<Transform>();

    private void Awake()
    {
        var playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var playerObject in playerObjects)
        {
            players.Add(playerObject.transform);
        }
    }

    void Update()
    {
        if (players.Count == 0)
        {
            AwareOfPlayer = false;
            DirectionToPlayer = Vector2.zero;
            return;
        }

        Transform closestPlayer = FindClosestPlayer();

        if (closestPlayer != null)
        {
            Vector2 enemyToPlayerVector = closestPlayer.position - transform.position;
            DirectionToPlayer = enemyToPlayerVector.normalized;

            if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
            {
                AwareOfPlayer = true;
            }
            else
            {
                AwareOfPlayer = false;
            }
        }
    }

    private Transform FindClosestPlayer()
    {
        Transform closestPlayer = null;
        float closestDistance = float.MaxValue;

        foreach (Transform player in players)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }
}

