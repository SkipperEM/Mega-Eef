using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterBullet : MonoBehaviour
{
    public Buster buster;

    public void Shoot(Buster _buster, float shotForce)
    {
        transform.SetParent(null);
        buster = _buster;
        transform.GetComponent<Rigidbody>().AddForce(buster.FPSCamera.transform.forward * shotForce);
    }

    private void OnTriggerEnter(Collider collision)
    {
        buster.BulletHit(collision);
        transform.SetParent(buster.transform);
        gameObject.SetActive(false);
    }
}
