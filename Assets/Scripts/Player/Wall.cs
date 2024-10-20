using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [HideInInspector] public float centerX, centerY, left, top, right, bottom;
    BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        centerX = boxCollider.bounds.center.x;
        centerY = boxCollider.bounds.center.y;
        left = boxCollider.bounds.min.x;
        top = boxCollider.bounds.max.y;
        right = boxCollider.bounds.max.x;
        bottom = boxCollider.bounds.min.y;
    }
}