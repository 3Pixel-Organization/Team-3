using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RedRain // Enemy Handler
// First Started - 3/3/21




// The EnemyState will determine what actions the Enemy will take.
public enum EnemyState
{
    idle,
    walking,
    chasingPlayer,
    returningHome,
    attacking,
    stagger
   
}
// DefaultMovementType will determine how the enemy moves when in the walking state.
public enum DefaultMovementType
{
    standingStill,
    randomMovement,
    patrolXAxis,
    patrolYAxis
}


public class Enemy : MonoBehaviour
{
    [Header("Do not assign values below. They are set as needed or will be assigned by the script.")]
    public EnemyState currentState;
    public Rigidbody2D enemyRb;
    Vector2 startPosition;
    Vector2 movementDirection;
    bool patrolDirectionPositive = true;   
    // player is target by default. This will determine who the enemy checking distance from and/or chasing/attacking.
    private Transform target;

    [Header("The values below should be adjusted as needed. Hover over a value for more info.")]
    [Tooltip("Select if the enemy will stand still, move randomly, or patrol up/down or left/right when not chasing or attacking the player.")]
    public DefaultMovementType movementType;
    public float moveSpeed;
    [Tooltip("How far the enemy can move on the x axis from their spawn position. If Patrolling Up/Down, this should be Zero.")]
    public int xRange;
    [Tooltip("How far the enemy can move on the y axis from their spawn position. If Patrolling Left/Right, this should be Zero.")]
    public int yRange;
    [Tooltip("How far the Player can be before the enemy will chase them.")]
    public float chaseRadius;
    [Tooltip("How far the Player can be before the enemy will attack them.")]
    public float attackRadius;
    [Tooltip("This must match the Tag on the Player. This can be removed and the actual tag placed in script if requested.")]
    public string playerTag;


 



    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }   
    void Start()
    {
        target = GameObject.FindWithTag(playerTag).transform;
        SetStartPos();
        currentState = EnemyState.walking;
        // coroutine below is used to redirect enemy movement on random intervals when walking with randomMovement type.
        if (movementType == DefaultMovementType.randomMovement)
        {
            InvokeRepeating("RandomMovementDirection", .1f, (Random.Range(.8f, 2f)));
        }
    }  
    void Update()
    {
        // Check Distance, and set enemy to chase or attack the player if needed.
        CheckDistance();        
        // if not attacking/chasing player, then will check if outside movement range. will set state to ReturningHome and then back to walking once done.
        if (currentState != EnemyState.attacking || currentState != EnemyState.chasingPlayer)
        {
            CheckReturnHome();
        }
    }
    void FixedUpdate()
    {
        // process player movement/animation based on currentState
        switch (currentState)
        {
            case EnemyState.walking:
                MakeNpcWalk();
                break;
            case EnemyState.idle:
                ChangeState(EnemyState.walking);
                break;
            case EnemyState.chasingPlayer:
                ChasePlayer();
                break;
            case EnemyState.returningHome:
                ReturnHome();
                break;
            case EnemyState.attacking:
                AttackCycle();
                break;
            default: break;
        }
    }
    void OnEnable()
    {
        // Reset startPos as enemy should only be renabled when set at a new location. Also ensure Enemy is walking.
        SetStartPos();
        ChangeState(EnemyState.walking);
    }

    private void SetStartPos()
    {
        startPosition = transform.position;
    }
    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
    private void RandomMovementDirection()
    {
        if (currentState == EnemyState.walking)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    // Random Movement in any direction.
                    movementDirection = new Vector2(RandomNonZero(), RandomNonZero());
                    break;
                case 1:
                    // Random Movement up or down.
                    movementDirection = new Vector2(0, RandomNonZero());
                    break;
                case 2:
                    // Random Movement left or right.
                    movementDirection = new Vector2(RandomNonZero(), 0);
                    break;
                case 3:
                    // Stand Still.
                    movementDirection = Vector2.zero;
                    break;
                default:
                    movementDirection = Vector2.zero;
                    break;
            }
        }
    }
    private int RandomNonZero()
    {
        int randomNum = 0;
        while (randomNum == 0)
        {
            randomNum = Random.Range(-1, 2);
        }
        return randomNum;
    }
    private void CheckDistance()
    {
        // if within Chasing distance but not withing attacking distance, then will chase player.
        if (DistanceFromTarget() <= chaseRadius && DistanceFromTarget() > attackRadius)
        {
            ChangeState(EnemyState.chasingPlayer);
        }
        // else if distance is less then attacking distance, then will attack player.
        else if (DistanceFromTarget() <= attackRadius)
        {
            ChangeState(EnemyState.attacking);
        }
        // else will walk
        else ChangeState(EnemyState.walking);
    }
    private float DistanceFromTarget()
    {
        return Vector2.Distance(target.position, transform.position);
    }
    private void CheckReturnHome()
    {
        // if npc is outside the movementRange set by start position, then will set currentState to returningHome
        if (currentState == EnemyState.walking && (Mathf.Abs(enemyRb.position.x) > startPosition.x + xRange || Mathf.Abs(enemyRb.position.y) > startPosition.y + yRange))
        {
            ChangeState(EnemyState.returningHome);
        }
        // if npc is returning to start position, and their position is within inner half of movement range, set returnToStartPos to false.
        else if (currentState == EnemyState.returningHome && Mathf.Abs(enemyRb.position.x) < startPosition.x + (xRange / 2) && Mathf.Abs(enemyRb.position.y) < startPosition.y + (yRange / 2))
        {
            ChangeState(EnemyState.walking);
        }
    }
    private void MakeNpcWalk()
    {
        switch (movementType)
        {
            case DefaultMovementType.randomMovement:
                MoveEnemy(movementDirection);
                break;

            case DefaultMovementType.standingStill:
                MoveEnemy(Vector2.zero);
                break;

            case DefaultMovementType.patrolXAxis:
                PatrolX();
                break;

            case DefaultMovementType.patrolYAxis:
                PatrolY();
                break;
            default: break;
        }
    }
    private void PatrolY()
    {
        if (patrolDirectionPositive)
        {
            movementDirection = Vector2.up;
            if (enemyRb.position.y >= startPosition.y + yRange)
            {
                patrolDirectionPositive = false;
            }
        }
        else if (!patrolDirectionPositive)
        {
            movementDirection = Vector2.down;
            if (enemyRb.position.y <= startPosition.y - yRange)
            {
                patrolDirectionPositive = true;
            }
        }
        MoveEnemy(movementDirection);
    }
    private void PatrolX()
    {
        if (patrolDirectionPositive)
        {
            movementDirection = Vector2.right;
            if (enemyRb.position.x >= startPosition.x + xRange)
            {
                patrolDirectionPositive = false;
            }
        }
        else if (!patrolDirectionPositive)
        {
            movementDirection = Vector2.left;
            if (enemyRb.position.x <= startPosition.x - xRange)
            {
                patrolDirectionPositive = true;
            }
        }
        MoveEnemy(movementDirection);
    }
    private void MoveEnemy(Vector2 movementDirection)
    {
        enemyRb.MovePosition(enemyRb.position + (movementDirection.normalized * moveSpeed * Time.fixedDeltaTime));
    }
    private void ChasePlayer()
    {
        Vector3 Direction = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
        enemyRb.MovePosition(Direction);
        ChangeState(EnemyState.chasingPlayer);
    }
    private void ReturnHome()
    {
        if (movementType == DefaultMovementType.randomMovement)
        {
            //Random.Range used to set target destination to a random spot inside the movement range.
            movementDirection.x = (startPosition.x + Random.Range(-xRange, xRange)) - enemyRb.position.x;
            movementDirection.y = (startPosition.y + Random.Range(-yRange, yRange)) - enemyRb.position.y;
        }
        else
        {
            movementDirection.x = (startPosition.x) - enemyRb.position.x;
            movementDirection.y = (startPosition.y) - enemyRb.position.y;
        }
        MoveEnemy(movementDirection);
    }
    public void AttackCycle()
    {
        // Currently will just stand still when attacking.
        movementDirection = Vector2.zero;
    }
}
