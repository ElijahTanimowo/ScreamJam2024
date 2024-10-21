using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Luminovore : MonsterBase
{
    Vector2 movement;
    float lastMovement;

    protected override void Awake()
    {
        base.Awake();
  

    }

    protected override void Start()
    {
        base.Start();
        lastMovement = -1;
    }

    protected override void Update()
    {
        base.Update();

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

        // if luminovore is almost idle
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

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


}
