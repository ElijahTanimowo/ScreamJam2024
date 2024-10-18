using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public CapsuleCollider2D monsterCollider { get; private set; }
    public BoxCollider2D attackRangeCollider { get; private set; }

    [Header("Other Information")]
    public float stoppingDis;
    Transform player;
    

    [Header("Monster Stats")]
    public float defaultSpeed;
    public float adjustSpeed;
    public float currentSpeed;


    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        monsterCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        

    }

    protected virtual void Start()
    {
        currentSpeed = defaultSpeed;
        rb.freezeRotation = true;
        player = PlayerManager.instance.player.transform;
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float disToPlayer = Vector2.Distance(transform.position, player.position);
        
        //if distance bigger, keep moving
        if (disToPlayer > stoppingDis)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            Debug.DrawRay(transform.position, dir * disToPlayer, Color.red);
            rb.MovePosition(rb.position + dir * adjustSpeed * Time.fixedDeltaTime);
        }
        //Stop monster moving
        else
        {
            rb.velocity = Vector2.zero;
        }
    }





}
