using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public float flashlightAngle = 35f;
    public float flashlightDistance = 5f;
    public LayerMask enemyLayer;

    private GameObject player;
    private string currentPlayerDirection = "Down";

    [SerializeField] Animator playerAnimator;


    // Keep track of monsters currently in the flashlight
    private HashSet<MonsterBase> monstersInLight = new HashSet<MonsterBase>();

    void Start()
    {
        player = transform.parent.gameObject;
        playerAnimator = player.GetComponent<Animator>();
    }

    void Update()
    {
        // Get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // Get direction
        Vector3 direction = mousePos - transform.position;

        // Get angle in degrees, convert to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Check if player is idle
        bool isIdle = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle");

        if (isIdle)
        {
            // allow free rotation
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            // limit flashlight rotation
            UpdateFlashlightDirection();
            if (IsInFlashlightRotationLimits(angle))
            {
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        // Check for enemies in the flashlight cone
        CheckForEnemies(direction);
    }

    void UpdateFlashlightDirection()
    {
        // Get player direction from blend tree
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Update player direction
        if (horizontal > 0)
            currentPlayerDirection = "Right";
        else if (horizontal < 0)
            currentPlayerDirection = "Left";
        else if (vertical > 0)
            currentPlayerDirection = "Up";
        else if (vertical < 0)
            currentPlayerDirection = "Down";
    }

    bool IsInFlashlightRotationLimits(float angle)
    {
        switch (currentPlayerDirection)
        {
            // Determine valid angles based on player direction
            case "Right":
                return angle > -90 && angle < 90;
            case "Left":
                return angle > 90 || angle < -90;
            case "Up":
                return angle > 0 && angle < 180;
            case "Down":
                return angle < 0 && angle > -180;
            default:
                return true;  // Default allows full rotation if something goes wrong
        }
    }

    void CheckForEnemies(Vector3 direction)
    {
        // Cast cone of rays
        int rayCount = 10;
        float angleStep = flashlightAngle / rayCount;

        HashSet<MonsterBase> monstersHitThisFrame = new HashSet<MonsterBase>();

        // for each ray
        for (int i = 0; i <= rayCount; i++)
        {
            // Calculate angle
            float angle = -flashlightAngle / 2 + angleStep * i;

            // Calculate direction
            Vector3 rayDirection = Quaternion.Euler(0, 0, angle) * direction;

            // Cast ray
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, flashlightDistance, enemyLayer);

            // Check if it hit an enemy
            if (hit)
            {
                // Calculate angle between flashlight direction and enemy direction
                Vector3 enemyDirection = hit.transform.position - transform.position;
                float angleToEnemy = Vector3.Angle(direction, enemyDirection);

                // Check if enemy is in flashlight cone
                if (angleToEnemy < flashlightAngle / 2f)
                {
                    MonsterBase monster = hit.collider.gameObject.GetComponent<MonsterBase>();
                    if (monster)
                    {
                        monster.changeSpeed = true;

                        // Add monster to this hit set
                        monstersHitThisFrame.Add(monster);
                    }

                }
            }
        }

        foreach (var monster in monstersInLight)
        {
            if (!monstersHitThisFrame.Contains(monster))
            {
                // Monster is no longer in the flashlight
                monster.changeSpeed = false; // Reset the monster's speed
            }
        }
        //Add to list
        monstersInLight = monstersHitThisFrame;
    }
}
