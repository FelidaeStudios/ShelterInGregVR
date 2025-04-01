using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public float attackRange = 2f;
    public float wallAttackTime = 2f;
    public int wallDamage = 10;
    public float playerChaseRange = 15f;

    private NavMeshAgent agent;
    private GameObject currentWall;
    private DoorController currentDoor;
    private float wallAttackTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float playerDist = Vector3.Distance(transform.position, player.position);

        // Check if a door is open
        DoorController door = FindOpenDoor();
        if (door != null)
        {
            agent.SetDestination(player.position); // Go through open door
            return;
        }

        // If no open door, check for obstacles
        if (playerDist < playerChaseRange && !CanReachPlayer())
        {
            AttackWall();
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    bool CanReachPlayer()
    {
        NavMeshPath path = new NavMeshPath();
        return agent.CalculatePath(player.position, path) && path.status == NavMeshPathStatus.PathComplete;
    }

    void AttackWall()
    {
        if (currentWall == null)
        {
            currentWall = FindBlockingWall();
        }

        if (currentWall != null)
        {
            agent.SetDestination(transform.position); // Stop moving
            wallAttackTimer += Time.deltaTime;

            if (wallAttackTimer >= wallAttackTime)
            {
                WallHealth wallHealth = currentWall.GetComponent<WallHealth>();
                if (wallHealth)
                {
                    wallHealth.TakeDamage(wallDamage);
                }
                wallAttackTimer = 0f;
            }
        }
    }

    GameObject FindBlockingWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, playerChaseRange))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    DoorController FindOpenDoor()
    {
        DoorController[] doors = FindObjectsOfType<DoorController>();
        foreach (DoorController door in doors)
        {
            if (door.open) return door;
        }
        return null;
    }
}
