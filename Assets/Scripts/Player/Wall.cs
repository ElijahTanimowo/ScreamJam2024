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
        centerX = transform.position.x + boxCollider.bounds.extents.x;
        centerY = transform.position.y - boxCollider.bounds.extents.y;
        left = transform.position.x;
        top = transform.position.y;
        right = transform.position.x + boxCollider.bounds.size.x;
        bottom = transform.position.y - boxCollider.bounds.size.y;
    }
}