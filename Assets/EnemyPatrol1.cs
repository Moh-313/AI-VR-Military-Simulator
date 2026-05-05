using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    public float waitTime = 2f;
    public bool loop = true;

    [Header("Movement Settings")]
    public float stoppingDistance = 0.5f;
    public float rotationSpeed = 5f;

    private NavMeshAgent agent;
    private int currentPoint = 0;
    private float waitTimer;
    private bool waiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Safety check
        if (patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned!");
            return;
        }

        // Make sure agent starts properly
        agent.stoppingDistance = stoppingDistance;
        agent.updateRotation = false; // we rotate manually

        GoToNextPoint();
    }

    void Update()
    {
        if (agent == null || patrolPoints.Length == 0) return;

        // Smooth rotation toward movement direction
        if (agent.velocity.magnitude > 0.1f)
        {
            Quaternion lookRot = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);
        }

        // Check if reached destination
        if (!waiting && agent.remainingDistance <= agent.stoppingDistance)
        {
            waiting = true;
            waitTimer = waitTime;
        }

        // Handle waiting
        if (waiting)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0f)
            {
                GoToNextPoint();
                waiting = false;
            }
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[currentPoint].position);

        // Move to next point
        if (loop)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
        else
        {
            currentPoint++;
            if (currentPoint >= patrolPoints.Length)
                currentPoint = patrolPoints.Length - 1;
        }
    }
}