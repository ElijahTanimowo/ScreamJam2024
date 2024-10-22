using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nyctostalker : MonsterBase
{

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

    protected override void PlaySound()
    {
        if (!monsterNoise.isPlaying)
        {
            monsterNoise.PlayOneShot(audioClips[1]);
        }
    }
}
