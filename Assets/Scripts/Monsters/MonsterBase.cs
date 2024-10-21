using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public CapsuleCollider2D monsterCollider { get; private set; }
    public BoxCollider2D attackRangeCollider { get; private set; }

    [Header("Other Information")]
    public float stoppingDistance;
    Transform player;
    //Check State of monster speed
    public bool changeSpeed = false;
    public float lifeTime = 15f;

    [Header("Monster Stats")]
    [SerializeField] protected float defaultSpeed;
    [SerializeField] protected float adjustSpeed;
    [SerializeField] protected float currentSpeed;

    [Header("Movement")]
    protected Vector2 movement;
    protected float lastMovement = -1;

    [Header("AI")]
    protected NavMeshAgent agent;


    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        monsterCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
    }
    protected virtual void Start()
    {
        currentSpeed = defaultSpeed;
        player = PlayerManager.instance.player.transform;
        rb.freezeRotation = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected virtual void Update()
    {
        UpdateMovement();
    }

    protected virtual void FixedUpdate()
    {
        PathFinding();
    }

    private void PathFinding()
    {
        //Get Direction of player
        Vector2 direction = (player.position - transform.position).normalized;
        //Get Distance from monster to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        //Stopping distance
        Debug.DrawRay(transform.position, direction * stoppingDistance, Color.red);
        
        //Distance big, close the gap between monster and player
        if (distanceToPlayer > stoppingDistance)
        {

            Vector3 stopPosition = player.position - (Vector3)direction * stoppingDistance;
            agent.SetDestination(stopPosition);
            agent.isStopped = false;
        }
        //Monster is at player pos, monster stop
        else
        {
            agent.isStopped = true;
        }
    }

    protected void UpdateMovement()
    {
        if (changeSpeed)
        {
            currentSpeed = adjustSpeed;
        }
        else
        {
            currentSpeed = defaultSpeed;
        }
        agent.speed = currentSpeed;

        Vector2 velocity = new Vector2(agent.velocity.x, agent.velocity.y);
        movement = velocity.normalized;

        // if nightmare is almost idle
        if (velocity.sqrMagnitude < 0.01f)
        {
            if (movement.y == 0)
                // keep last direction
                anim.SetFloat("Vertical", lastMovement);
            if (movement.x == 0)
                // keep last direction
                anim.SetFloat("Horizontal", lastMovement);
        }
        else
        {
            // update direction
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
        }

        anim.SetFloat("Speed", velocity.sqrMagnitude);
    }
}