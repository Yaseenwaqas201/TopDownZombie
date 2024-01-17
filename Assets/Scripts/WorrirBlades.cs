using System;
using UnityEngine;

public class WorrirBlades : MonoBehaviour
{
    public float damageToEnemy;
    public float rotationSpeed; 
    public float health; 
    
    
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.GetComponent<EnemyBase>();
            enemy.DamageTheEnemy(damageToEnemy,transform.position);
        }
    }
}
