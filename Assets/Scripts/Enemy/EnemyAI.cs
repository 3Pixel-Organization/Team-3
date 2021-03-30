using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform player;
    public LayerMask ground, playerMask;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    public float walkPointRange;
    private bool walkPointSet;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    [Header("State")]
    public float sightRange;
    public float attackRange;
	public bool playerInSightRange, playerInAttackRange;

	private void Awake()
	{
        agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

		if (!playerInSightRange && !playerInAttackRange) Patroling();
		if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distToWayPoint = transform.position - walkPoint;

		if (distToWayPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint()
	{
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, ground))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);

		if (!alreadyAttacked)
		{
            alreadyAttacked = true;
            Invoke(nameof( RestAttack ), timeBetweenAttacks);
		}
    }

    private void RestAttack()
	{
        alreadyAttacked = false;
	}

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
	}
}