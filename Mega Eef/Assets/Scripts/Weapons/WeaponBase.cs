using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class WeaponBase : MonoBehaviour
{
    public abstract void Use();
    public abstract void TurnOn();

    public bool isActive;

    public int ammoAmount;
    public int maxAmmo;

    public bool isShooting;
    public bool isReloading;
    public float reloadTime = 1f;
    public float range = 8f;

    public int damage = 1;

    public AudioSource audioSource;
    public AudioClip shootSFX;
    public AudioClip emptySFX;
    public AudioClip reloadingSFX;

    public Camera FPSCamera;
    
    
}
