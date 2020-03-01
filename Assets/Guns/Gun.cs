﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool fist = false;
    public float rpm = 120;

    public GameObject bullet;
    private ArrayList bullets;
    public float barrelLenght;
    public float gunLenght;
    private bool rightP=true;
    public int ammoMax=10;
    private int usedAmmo = 0;

    public int UsedAmmo
    {
        get => usedAmmo;
        set => usedAmmo = value;
    }

    private GameObject RightPunch;
    private GameObject LeftPunch;
    private float originalPos;
    private float coolDown;

    private bool isShooting;
    private Vector3 posWeaponOriginal;
    public float recoilSpeed=1f;
    public float maxRecoilDistance = 1f;
    public float recoilSpeedBack = 1f;
    public int damagePerBullet = 0;
    public float speedPerBullet = 0f;
    public float anglePerBullet = 0f;

    public void Shoot(float angle)
    { 
        isShooting = false;
        if (coolDown >= 60 / rpm)
        {
            
            coolDown = 0;
            if(fist)
            {
                GameObject currHand;
                if (rightP)
                    currHand = RightPunch;
                else
                    currHand = LeftPunch;
                currHand.GetComponent<GlovesScript>().Shoot();
                if (rightP)
                    rightP = false;
                else
                    rightP = true;
            }
            else
            {
                //BASIC WEAPON
                
                GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("SFX/Pickup"),0.5f);
                
                GameObject tempBullet = Instantiate(bullet,
                    new Vector3(transform.position.x + barrelLenght * Mathf.Cos((angle) * Mathf.Deg2Rad),
                        transform.position.y + barrelLenght * Mathf.Sin((angle) * Mathf.Deg2Rad), transform.position.z),
                    transform.rotation);
                if (tempBullet.GetComponent<BulletScript>())
                {
                    tempBullet.GetComponent<BulletScript>().Ply = transform.parent.gameObject;
                    if(damagePerBullet!=0) 
                        tempBullet.GetComponent<BulletScript>().damage = damagePerBullet;
                    if(speedPerBullet!=0) 
                        tempBullet.GetComponent<BulletScript>().speed = speedPerBullet;
                    if(anglePerBullet!=0) 
                        tempBullet.GetComponent<BulletScript>().angleRdm = anglePerBullet;
                }

                if (tempBullet.GetComponent<ShotgunShell>())
                {
                    tempBullet.GetComponent<ShotgunShell>().Ply = transform.parent.gameObject;
                    if(damagePerBullet!=0) 
                        tempBullet.GetComponent<ShotgunShell>().damagePerBullet = damagePerBullet;
                }

                usedAmmo++;
                checkAmmo();
                isShooting = true;
            }
        }
    }

    public void Update()
    {
        if (isShooting&&!fist)
        {
            if (transform.localPosition.y > posWeaponOriginal.y - maxRecoilDistance)
            {
                transform.localPosition -= new Vector3(0, Time.deltaTime * recoilSpeed*50f);
            }else transform.localPosition=new Vector3(posWeaponOriginal.x,posWeaponOriginal.y-maxRecoilDistance);
        }
        else
        {
            if (transform.localPosition.y < posWeaponOriginal.y)
            {
                transform.localPosition += new Vector3(0, Time.deltaTime * recoilSpeedBack*0.5f);
            }
            else
                transform.localPosition = posWeaponOriginal;
        }
        coolDown += Time.deltaTime;
    }

    public void Stopped()
         {
             isShooting = false;
             if (GetComponent<Animator>())
             {
                 GetComponent<Animator>().SetFloat("shooting", 0f);
            
             }
         }
    public void Shooting()
    {
        isShooting = true;

        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetFloat("shooting", 1.0f);
        }
        
    }
    public void Start()
    {
        isShooting = false;
        if (fist)
        {
            RightPunch = transform.GetChild(1).gameObject;
            LeftPunch = transform.GetChild(0).gameObject;
            originalPos = transform.GetChild(0).localPosition.y;
        }
        posWeaponOriginal = transform.localPosition;
    }

    private void checkAmmo()
    {
        if(usedAmmo>=ammoMax) ((Player) transform.parent.gameObject.GetComponent<Player>()).DestroyGun();
    }
}