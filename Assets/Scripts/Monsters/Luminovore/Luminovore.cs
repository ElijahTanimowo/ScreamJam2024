using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Luminovore : MonsterBase
{
    //Check State of monster speed
    public bool changeSpeed = false;


    protected override void Awake()
    {
        base.Awake();
  

    }

    protected override void Start()
    {
        base.Start();
        

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
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


}
