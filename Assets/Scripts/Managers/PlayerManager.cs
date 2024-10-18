using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    private void Awake()
    {
        //Game Manager in world, destory
        if (!instance)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
