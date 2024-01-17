using UnityEngine;

[CreateAssetMenu(fileName = "ShortGun", menuName = "Weapons/ShortGun", order = 1)]
public class ShortGun : WeaponBase
{
    public override void Fire(Transform FirePoint)
    {
        float angle = -30;
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
                        FirePoint.transform.localRotation = Quaternion.Euler(0f, 0f, angle); 
                        bullet.FireBulletFromFirePos(FirePoint);
                        angle += 30;
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
        FirePoint.transform.rotation = Quaternion.Euler(0f, 0f, 0); 
    }
}
