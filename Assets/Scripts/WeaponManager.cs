using DG.Tweening;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    public GameObject bulletFillBar;
    public Transform bulletInPos;
    public WeaponBase firstWeapon;
    public WeaponBase secondWeapon;

    public GameObject warrierBlade;
    public WeaponBase selectedWeapon;
    

    public bool isFireStart = false;

    private float delayTimeOfFire;

    private void Start()
    {
        EventManager.warrierBladeUpgrade += ActivateWarierUpgrade;
        EventManager.weaponUpgrade += WeaponUpgrade;
    }

    private void OnDestroy()
    {
        EventManager.warrierBladeUpgrade -= ActivateWarierUpgrade;
        EventManager.weaponUpgrade -= WeaponUpgrade;
    }

    private void Update()
    {
        StartFiring();
    }

    public void StartFiring()
    {
        delayTimeOfFire -= Time.deltaTime;
        if (delayTimeOfFire <= 0)
        {
            delayTimeOfFire = selectedWeapon.delayTimeBtwBullets;
            if (isFireStart)
            {
                bulletFillBar.transform.localScale=new Vector3(0,1);
                bulletFillBar.transform.DOScale(Vector3.one, delayTimeOfFire);
                selectedWeapon.Fire(bulletInPos);
            }
        }

    }

    public void ActivateWarierUpgrade()
    {
        warrierBlade.SetActive(true);
    }

    public void WeaponUpgrade()
    {
        selectedWeapon = secondWeapon;
    }
}