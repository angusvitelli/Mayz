using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;                // Reference to the player's transform
    public float speed = 5f;                // Speed at which the enemy follows the player
    public float followRange = 10f;         // Range within which the enemy starts following
    public float rotationSpeed = 5f;        // Speed of the enemy's rotation
    public Transform[] patrolPoints;        // Array of waypoints for patrolling
    public float patrolSpeed = 2f;          // Speed at which the enemy patrols
    public float patrolWaitTime = 2f;       // Time to wait at each patrol point
    public AudioClip followSound; 

    private int currentPoint = 0;           // Index of the current patrol point
    private NavMeshAgent agent;             // Reference to the NavMeshAgent component
    private bool isFollowing = false;       // Flag to check if the enemy is currently following the player
    private bool waiting = false;           // Flag to check if the enemy is waiting at a patrol point
    private AudioSource audioSource;        // Reference to the AudioSource 

    public float minEnemyDistance = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false; // Prevent stopping between waypoints
        audioSource = GetComponentInChildren<AudioSource>();
        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= followRange)
            {
                if (!isFollowing) // Play the sound only when AI starts following
                {
                    audioSource.PlayOneShot(followSound);
                }

                isFollowing = true;
                FollowPlayer();
            }
            else
            {
                isFollowing = false;
                PatrolArea();
            }
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.speed = patrolSpeed; // Set patrol speed
        agent.destination = patrolPoints[currentPoint].position;
        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }

    void PatrolArea()
    {
        GoToNextPatrolPoint();
    }

    void FollowPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        agent.speed = speed;
        agent.destination = player.position;
    }
}
