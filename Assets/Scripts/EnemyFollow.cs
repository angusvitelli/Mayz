using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;                
    public float speed = 5f;                
    public float followRange = 10f;         
    public float rotationSpeed = 5f;        
    public Transform[] patrolPoints;        
    public float patrolSpeed = 2f;          
    public float patrolWaitTime = 2f;       
    public AudioClip followSound; 

    private int currentPoint = 0;           
    private NavMeshAgent agent;             
    private bool isFollowing = false;      
    private bool waiting = false;           
    private AudioSource audioSource;        

    public float minEnemyDistance = 3f;

	private bool isStunned = false;
	private float stunDuration = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false; // Prevent stopping between waypoints
        audioSource = GetComponentInChildren<AudioSource>();

        if(patrolPoints.Length > 0)
            {
                GoToNextPatrolPoint();
            }
    }

    void Update()
    {
		if (isStunned)
		{
			stunDuration -= Time.deltaTime;
			if (stunDuration <= 0)
			{
				isStunned = false;
				agent.isStopped = false;
			}
			else
			{
				agent.isStopped = true;
			}
		}
		else
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
					if(!waiting && !agent.pathPending && agent.remainingDistance < 0.5f)
                    {
                        StartCoroutine(WaitAndMoveToNextPatrolPoint());
                    }
				}
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

   IEnumerator WaitAndMoveToNextPatrolPoint()
   {
        waiting = true;
        yield return new WaitForSeconds(patrolWaitTime);
        GoToNextPatrolPoint();
        waiting=false;
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

	public void StunEnemy(float duration)
	{
		isStunned = true;
		stunDuration = duration;
	}
}
