using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public float flashlightAngle = 35f;
    public float flashlightDistance = 5f;
    public LayerMask enemyLayer;

    void Update()
    {
        // Get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // Get direction
        Vector3 direction = mousePos - transform.position;

        // Get angle in degrees, convert to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Check for enemies in the flashlight cone
        CheckForEnemies(direction);
    }

    void CheckForEnemies(Vector3 direction)
    {
        // Cast cone of rays
        int rayCount = 10;
        float angleStep = flashlightAngle / rayCount;

        // for each ray
        for (int i = 0; i <= rayCount; i++)
        {
            // Calculate angle
            float angle = -flashlightAngle / 2 + angleStep * i;

            // Calculate direction
            Vector3 rayDirection = Quaternion.Euler(0, 0, angle) * direction;

            // Cast ray
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, flashlightDistance, enemyLayer);

            // check if it hit enemy
            if (hit)
            {
                // Calculate angle between flashlight direction and enemy direction
                Vector3 enemyDirection = hit.transform.position - transform.position;
                float angleToEnemy = Vector3.Angle(direction, enemyDirection);

                // check if enemy is in flashlight cone
                if (angleToEnemy < flashlightAngle / 2f)
                {
                    Debug.Log("Enemy detected: " + hit.transform.name);
                }
            }
        }
    }
}
