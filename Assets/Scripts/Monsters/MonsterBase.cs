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

    [Header("Monster Stats")]
    [SerializeField] protected float defaultSpeed;
    [SerializeField] protected float adjustSpeed;
    [SerializeField] protected float currentSpeed;

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





}
