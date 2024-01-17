using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float TotalHealth;
    private float healthRemaining;
    public float damageToPlayer;
    public float enemyMoveSpeed;
    public Transform backPos;
    public GameObject gemPrefab;
    public Transform player;

    public List<EnemyTxtAnimator> txtAnimators=new List<EnemyTxtAnimator>();
    private Rigidbody2D enemyRigidbody2D;
    public virtual void Awake()
    {
        healthRemaining = TotalHealth;
        enemyRigidbody2D=GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GamePlayManager.gameplayinstance.player;

    }

    private void FixedUpdate()
    {
        if (GamePlayManager.gameplayinstance.player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Normalize the direction vector to ensure consistent movement speed
            directionToPlayer.Normalize();

            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            backPos.parent.rotation = Quaternion.Euler(0f, 0f, angle);
            
            // Move the enemy towards the player
            transform.Translate(directionToPlayer * enemyMoveSpeed * Time.deltaTime);
        }
    }

    public float pushDistance=2f;
    public virtual void DamageTheEnemy(float damageValue, Vector3 colliderPos )
    {
        
        healthRemaining -= damageValue;
        
        #region Damage Health Text Animator
        foreach (EnemyTxtAnimator txtAnimator in txtAnimators)
        {
            if (!txtAnimator.gameObject.activeSelf)
            {
                txtAnimator.AnimateTxt(damageValue,transform);
                break;
            }
            {
                EnemyTxtAnimator enemyTxtAnimator =
                    Instantiate(txtAnimators[0], txtAnimators[0].transform.position, Quaternion.identity,txtAnimators[0].transform.parent);
                txtAnimators.Add(enemyTxtAnimator);
                enemyTxtAnimator.AnimateTxt(damageValue,transform);
                break;
            }
        }
        #endregion

        // Push away enemy when Hit Something that damage him
        MoveBackOnDamageOrCollide();
        
        if (healthRemaining <= 0)
        {
            // Here Enemy Die and We will do some Effect
            gameObject.SetActive(false);
           GameObject gem = Instantiate(gemPrefab, transform.position, Quaternion.identity, transform.parent);
           gem.SetActive(true);
           ResetEnemyData();
           GameConstant.noEnemiesKilled += 1;
           EventManager.InvokeEnemyKilledEvent();
        }
    }

    public void MoveBackOnDamageOrCollide()
    {
        transform.DOMove(backPos.position, 0.1f);

    }
    
    
    public virtual void ResetEnemyData()
    {
        healthRemaining = TotalHealth;
    }
    
}
