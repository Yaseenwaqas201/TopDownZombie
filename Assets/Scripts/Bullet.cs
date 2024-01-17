using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed=5;
    public float bulletDamage=10;

    public float lifetime = 2f; // Adjust the lifetime as needed

    private Vector2 initialPostion;
    private Transform initialParent;
    private void Awake()
    {
        initialParent = transform.parent;
    }

    private void OnEnable()
    {
        initialPostion = initialParent.position;
        transform.SetParent(null);
        transform.rotation = initialParent.rotation;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right*bulletSpeed*Time.deltaTime);
        
        // Reduce the lifetime
        lifetime -= Time.deltaTime;

        // Disable the bullet if its lifetime is over
        if (lifetime <= 0f)
        {
            ResetToInitialPosition();
        }
    }


    public void ResetToInitialPosition()
    {
        lifetime = 2;
        transform.SetParent(initialParent);
        gameObject.SetActive(false);
    }

    public void FireBulletFromFirePos(Transform firePosition)
    {
        transform.rotation = firePosition.rotation;
        transform.position = firePosition.position;
        gameObject.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemy =other.GetComponent<EnemyBase>();
            enemy.DamageTheEnemy(bulletDamage,transform.position);
            gameObject.SetActive(false);
        }
    }
}
