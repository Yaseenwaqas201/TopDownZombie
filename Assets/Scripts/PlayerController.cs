using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxMoveSpeed = 5f;
    public float rotationSpeed = 200f;

    public float playerTotalHealth;
    public float playerRemainingHealth;
    public float enemyDetectionRadius;
    public float gemDetectionRadius;
    public Image healthBar;
    public GameObject radiusVisual;
    public WeaponManager weaponManager;
    
    private Collider2D nearestEnemy;
    public LayerMask enemyLayer;       
    public LayerMask gemLayer;       
    public JoystickController joystickController;

    
    private void Awake()
    {
        playerRemainingHealth = playerTotalHealth;
        EventManager.healthUpgrade += UpdateHealth;
        EventManager.rangeUpgrade += UpdatePlayerRadiusRange;
    }

    private void OnDestroy()
    {
        EventManager.healthUpgrade -= UpdateHealth;
        EventManager.rangeUpgrade -= UpdatePlayerRadiusRange;
    }

    private void Update()
    {
        PlayerEnemiesDetectionWithInRadius();
        DetectGemsNearToPlayer();
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
    }

    public void UpdateHealth()
    {
        playerRemainingHealth = playerTotalHealth;
        GiveDamageToPlayer(0);
    }

    public void UpdatePlayerRadiusRange()
    {
        enemyDetectionRadius += 0.5f;
    }


    public void PlayerEnemiesDetectionWithInRadius()
    {
        // Detect enemies within the specified radius
        radiusVisual.transform.localScale=new Vector3(enemyDetectionRadius,enemyDetectionRadius);
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyDetectionRadius, enemyLayer);
        if (nearestEnemy != null)
        {
            if (!nearestEnemy.gameObject.activeSelf || colliders.Length<=0)
                nearestEnemy = null;
        }

        // Iterate through the detected enemies
        foreach (Collider2D collider in colliders)
        {
            // Check if the detected object has the enemy tag or a specific script
            if (collider.CompareTag("Enemy"))
            {
                // Update the nearest enemy if it's closer than the current nearest enemy
                if (nearestEnemy == null || Vector2.Distance(transform.position, collider.transform.position) < Vector2.Distance(transform.position, nearestEnemy.transform.position))
                {
                    nearestEnemy = collider;
                    break;
                }
            }
        }

        // Perform actions with the nearest enemy (you can customize this part)
        if (nearestEnemy != null && nearestEnemy.gameObject.activeSelf)
        {
            Vector3 directionToEnemy =  nearestEnemy.transform.position - weaponManager.transform.position;
            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;

            // Rotate the gun towards the enemy
            weaponManager.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            weaponManager.isFireStart = true;
        }
        else
        {
            weaponManager.isFireStart = false;
        }
    }


    public void DetectGemsNearToPlayer()
    {
        // Detect enemies within the specified radius
        Collider2D[] gemColliders = Physics2D.OverlapCircleAll(transform.position, gemDetectionRadius, gemLayer);
        foreach (Collider2D gem in gemColliders)
        {
            gem.GetComponent<GemCollector>().AnimateCollectGem(transform);
        }
    }

    // Draw the detection radius and a line to the nearest enemy for better visualization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, gemDetectionRadius);
        if (nearestEnemy != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, nearestEnemy.transform.position);
        }
    }



    private void HandleMovementInput()
    {
        if (joystickController == null)
        {
            return;
        }

        float joystickDistance = joystickController.GetJoystickDistance();
        float normalizedSpeed = Mathf.Clamp01(joystickDistance / joystickController.GetMaxJoystickDistance());
        float currentMoveSpeed = maxMoveSpeed * normalizedSpeed;

        float horizontalInput = joystickController.GetHorizontalValue();
        float verticalInput = joystickController.GetVerticalValue();

        Vector2 inputDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // Code for Rotating all Direction
//        if (inputDirection != Vector2.zero)
//        {
//            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
//            transform.rotation = Quaternion.Euler(0f, 0f, angle);
//        }

        Vector3 movement = new Vector3(inputDirection.x, inputDirection.y, 0f) * currentMoveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
            enemy.MoveBackOnDamageOrCollide();
            GiveDamageToPlayer(enemy.damageToPlayer);
        }
    }

    public void GiveDamageToPlayer(float damageValue)
    {
        playerRemainingHealth -= damageValue;
        float healthBrValue = playerRemainingHealth /playerTotalHealth ;
        healthBar.transform.DOScale(new Vector3(healthBrValue, 1), 0.1f);
        if (healthBrValue <= 0.35)
        {
            healthBar.color = Color.red;
        }
        if (playerRemainingHealth<=0)
        {
            // Here  Thing done When Player Died
            EventManager.InvokeGameOverEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gem"))
        {
            GameConstant.TotalGems += 1;
            EventManager.InvokeUpdateGameEconomy();
            Destroy(other.gameObject);
        }
    }
}
