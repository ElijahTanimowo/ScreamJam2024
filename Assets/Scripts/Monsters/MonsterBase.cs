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
    public float stoppingDis;
    public float rayDistance = 1f;
    Transform player;

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
        agent.SetDestination(player.position);
    }





}
