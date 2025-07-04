
using StarterAssets;
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
    private float sightRange = 5f;
    private float fovAngle = 140f;
    
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

    public GameObject gameOverMenu;

    private Animator animator;
    private bool isWalkingAnim = false;

    public bool aiEnabled = false;

    private AudioSource audioSource;
    public AudioClip walkingClip;

    private bool playerSpotted = false;
    private float timeSinceSpotted = 0f;
    public float gracePeriod = 1f; // 1 second delay
    private bool gameOverTriggered = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        state = EnemyState.Idle;

        if (playerObject != null)
        {
            player = playerObject.transform;
            karmaManager = playerObject.GetComponent<KarmaManager>();
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.clip = walkingClip;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (!aiEnabled) return;

        PlayerInSight();

        if (canSeePlayer)
        {
            if (!playerSpotted)
            {
                playerSpotted = true;
                timeSinceSpotted = 0f;
            }
            else
            {
                timeSinceSpotted += Time.deltaTime;

                if (timeSinceSpotted >= gracePeriod && !gameOverTriggered)
                {
                    gameOverTriggered = true;
                    OnPlayerSpotted();
                }
            }

            return;
        }
        else
        {
            // Reset if player is no longer in sight
            playerSpotted = false;
            timeSinceSpotted = 0f;
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
            Vector3 origin = transform.position + Vector3.up * 1f;
            Vector3 directionP = (player.position - origin).normalized;
            if (Physics.Raycast(origin, directionP, out hit, sightRange))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    canSeePlayer = true;
                }

            }
        }
    }

    private void Patrol()
    {
        agent.speed = 2f;
        agent.SetDestination(waypoints[currentWaypoint].position);

        SetWalkingAnimation(true);

        float distanceToPoint = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if (distanceToPoint < 1.5f && !atWaypoint)
        {
            atWaypoint = true;
            timeSpentIdling = 0f;
            Debug.Log("Idling at waypoint: " + currentWaypoint);
        }
    }

    private void Idle()
    {
        agent.SetDestination(transform.position); // stops moving
        timeSpentIdling += Time.deltaTime; // track time spent in this waypoint

        SetWalkingAnimation(false);

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

    public void StandAtStairs()
    {
        agent.SetDestination(stairWaypoint.position);
        atWaypoint = false;

        float distToStairs = Vector3.Distance(transform.position, stairWaypoint.position);

        if (distToStairs < 0.5f)
        {
            SetWalkingAnimation(false);
            timeSpentAtStairs += Time.deltaTime;

            if (timeSpentAtStairs >= 10f)
            {
                standAtStairsTriggered = false;
                timeSpentAtStairs = 0f;
            }
        }
        else
        {
            SetWalkingAnimation(true); // still walking toward the stairs
        }
    }


    private void OnPlayerSpotted()
    {
        if (agent != null)
            agent.SetDestination(transform.position); // Stop moving

        Debug.LogWarning("Player spotted!");
        SetWalkingAnimation(false);

        //Set the game over menu to active and display menu
        gameOverMenu.SetActive(true);

        //Find the player controller and disable it
        FindAnyObjectByType<FirstPersonController>().enabled = false;

        //Find the player footsteps script and disable it
        FindAnyObjectByType<Footsteps>().enabled = false;

        //Release the mouse so it can interact with the menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;                
    }
        
    private void ChangeState(EnemyState newState)
    {
        if (state == newState) return;
        Debug.Log($"Changing state from {state} to {newState}");
        state = newState;
    }

    private void SetWalkingAnimation(bool walking)
    {
        if (animator != null && isWalkingAnim != walking)
        {
            animator.SetBool("isWalking", walking);
            isWalkingAnim = walking;
        }

        HandleFootsteps(walking);
    }

    public void EnableAI()
    {
        aiEnabled = true;
        Debug.Log("AI has been enabled.");
    }

    private void HandleFootsteps(bool shouldPlay)
    {
        if (audioSource == null) return;

        if (shouldPlay && !audioSource.isPlaying)
            audioSource.Play();
        else if (!shouldPlay && audioSource.isPlaying)
            audioSource.Stop();
    }

}
