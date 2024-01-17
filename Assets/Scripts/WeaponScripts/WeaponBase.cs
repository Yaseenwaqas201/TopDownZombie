using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/BaseWeapon", order = 1)]
public class WeaponBase : ScriptableObject , WeaponInterface 
{
    public float delayTimeBtwBullets;
    public int noOfBulletAtOneShot;
    public int speedOfBullet;
    public int lifeTimeOfBullet;
    public  int damageOfBullet;
    public Bullet bulletPrefab;
    public List<Bullet> bullets=new List<Bullet>();

    private void OnEnable()
    {
        bullets.Clear(); // Remove Garbage Reference 
        UpdateBulletValue();

    }

    void UpdateBulletValue()
    {
        bulletPrefab.bulletSpeed = speedOfBullet;
        bulletPrefab.lifetime = lifeTimeOfBullet;
        bulletPrefab.bulletDamage = damageOfBullet;
    }
    public virtual void Fire(Transform FirePoint )
    {
        // Implement base firing logic here
        Bullet bulletAssign=null;
           
        for (int i = 0; i < noOfBulletAtOneShot; i++)
        {
            bulletAssign = null;
            if (bullets.Count > 0)
            {
                foreach (Bullet bullet in bullets)
                {
                    if (!bullet.gameObject.activeSelf)
                    {
                        bulletAssign = bullet;
                        bullet.FireBulletFromFirePos(FirePoint);
                        break;
                    }
                }
            }

            if (bulletAssign == null)
            {
                Bullet newbullet = Instantiate(bulletPrefab, FirePoint.position, Quaternion.identity,FirePoint);
                bullets.Add(newbullet);
            }
              
        } 

    }
}
