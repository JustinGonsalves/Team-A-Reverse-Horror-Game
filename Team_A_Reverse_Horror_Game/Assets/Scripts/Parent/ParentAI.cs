
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Enemy AI that patrols the floor and reacts dynamically based on KarmaManager and player interactions.
public class Enemy : MonoBehaviour
{
    public enum EnemyState { Idle, Patrol, StandAtStairs }
    public EnemyState state;

    public List<Transform> waypoints;
    private float sightRange = 10f;
    private float fovAngle = 140f;
    // public string gameOverSceneName = "GameOver";

    private int currentWaypoint = 0;
    private NavMeshAgent agent;
    private bool atWaypoint = false;
    public float timeSpentIdling = 0f;
    private bool canSeePlayer;
    private Transform player;
    public GameObject playerObject;
    private KarmaManager karmaManager;

    public Transform stairWaypoint;
    public bool standAtStairsTriggered = false;
    public float timeSpentAtStairs = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (playerObject != null)
        {
            player = playerObject.transform;
            karmaManager = playerObject.GetComponent<KarmaManager>();
        }
    }

    void Update()
    {
        PlayerInSight();

        if (canSeePlayer == true)
        {
            return;
        }

        if (standAtStairsTriggered)
            ChangeState(EnemyState.StandAtStairs);
        else if (atWaypoint)
            ChangeState(EnemyState.Idle);
        else
            ChangeState(EnemyState.Patrol);

        switch (state)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Patrol: Patrol(); break;
            case EnemyState.StandAtStairs: StandAtStairs(); break;
        }
    }

    private void PlayerInSight()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        float distance = Vector3.Distance(player.position, transform.position);
        canSeePlayer = false;

        if (distance <= sightRange && angle <= fovAngle / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, sightRange))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    canSeePlayer = true;
                    OnPlayerSpotted();
                }

            }
        }
    }

    private void Patrol()
    {
        agent.SetDestination(waypoints[currentWaypoint].position);

        float distanceToPoint = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if (distanceToPoint < 0.5f && !atWaypoint)
        {
            atWaypoint = true;
            timeSpentIdling = 0f;
            Debug.Log("Idling at waypoint: " + currentWaypoint);
        }
    }

    private void Idle()
    {
        agent.SetDestination(transform.position);
        timeSpentIdling += Time.deltaTime;

        float waitTime = 3f;

        if (currentWaypoint == waypoints.Count - 1) // determine if we're currently at the base
        {
            float karma = karmaManager != null ? karmaManager.totalKarma : 0f;
            if (karma >= 90f) waitTime = 30f;
            else if (karma >= 70f) waitTime = 60f;
            else if (karma >= 50f) waitTime = 75f;
            else waitTime = 90f;
        }

        if (timeSpentIdling >= waitTime)
        {
            atWaypoint = false;
            timeSpentIdling = 0f;
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    public void TriggerPatrol()
    {
        Debug.Log("Patrol triggered. Restarting route.");
        atWaypoint = false;
        currentWaypoint = 0;
    }

    public void StandAtStairs()
    {
        agent.SetDestination(stairWaypoint.position);
        atWaypoint = false;

        float distToStairs = Vector3.Distance(transform.position, stairWaypoint.position);
        if (distToStairs < 1.5f)
        {
            timeSpentAtStairs += Time.deltaTime;
            if (timeSpentAtStairs >= 10f)
            {
                standAtStairsTriggered = false;
                timeSpentAtStairs = 0f;
            }
        }
    }

    private void OnPlayerSpotted()
    {
        if (agent != null)
            agent.SetDestination(transform.position); // Stop moving

        Debug.LogWarning("Player spotted!"); // replace or remove later

        // Play animation
        // GetComponent<Animator>().SetTrigger("Gasp");

        // Optional: delay before scene switch (coroutine)
        // StartCoroutine(HandleGameOver());
    }

    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(2f); // wait for gasp animation to play
        // SceneManager.LoadScene(gameOverScene);
    }

    private void ChangeState(EnemyState newState)
    {
        if (state == newState) return;
        Debug.Log($"Changing state from {state} to {newState}");
        state = newState;
    }
}
