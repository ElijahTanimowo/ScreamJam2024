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

    [Header("Monster Stats")]
    [SerializeField] float defaultSpeed;
    [SerializeField] float adjustSpeed;
    float currentSpeed;


    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        monsterCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
}
