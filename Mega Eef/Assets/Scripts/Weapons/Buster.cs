using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buster : WeaponBase
{
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] float shotForce = 20f;
    [SerializeField] float firingSpeed = 0.25f;
    float timePassed;
    //public RaycastHit hit;

    private void Update()
    {
        timePassed += Time.deltaTime;
    }

    public override void TurnOn()
    {
        isActive = true;
        gameObject.SetActive(true);
    }

    public override void Use()
    {
        if (timePassed < firingSpeed)
            return;

        if (ammoAmount > 0)
        {
            ProcessRaycast();
            timePassed = 0;
        }
        //else
        //{
        //    //audioSource.PlayOneShot(emptySFX);
        //}
    }

    void ProcessRaycast()
    {
        BusterBullet bullet = objectPooler.SpawnFromPool("BusterBullet", transform.position, transform.rotation).GetComponent<BusterBullet>();
        bullet.Shoot(this, shotForce);
        //if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range))
        //{
        //    hit.collider.gameObject.GetComponent<ITakeDamage>()?.TakeDamage(damage);
        //}
        //else
        //{
        //    return;
        //}
    }

    public void BulletHit(Collider collision)
    {
        collision.gameObject.GetComponent<ITakeDamage>()?.TakeDamage(damage);
    }
}
