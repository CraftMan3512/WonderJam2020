using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Type d'arme")]
    public bool lightning;
    public bool water;

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
    [Header("Recoil Values")]
    public float recoilSpeed=0.03f;
    public float maxRecoilDistance = 0.18f;
    public float recoilSpeedBack = 0.15f;
    [Header("Bullet Override")]
    public int damagePerBullet = 0;
    public float speedPerBullet = 0f;
    public float anglePerBullet = 0f;
    //water vars
    private GameObject waterBullet;
    private bool justShot;

    public void Shoot(float angle)
    {
        if (coolDown >= 60 / rpm)
        {
            justShot = true;
            
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
            }else if (lightning)
            {
                GameObject temp =Instantiate(Resources.Load<GameObject>("Lightning"), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                temp.GetComponent<Lightning>().Ply=transform.parent.gameObject;
                usedAmmo++;
                checkAmmo();
            }else if (water)

            {
                
                if (waterBullet) {
                    
                    waterBullet.GetComponent<WaterGenerator>().AddPoint(
                    new Vector3(transform.position.x + barrelLenght * Mathf.Cos((angle) * Mathf.Deg2Rad),
                    transform.position.y + barrelLenght * Mathf.Sin((angle) * Mathf.Deg2Rad), 0),
                    angle
                    );

                    usedAmmo++;
                    checkAmmo();
                }
                else
                {

                    
                    Debug.Log("SPAWN NEW WATER");
                    waterBullet = Instantiate(bullet,transform.position,Quaternion.identity);
                    waterBullet.GetComponent<WaterGenerator>().SpawnWater(                
                        new Vector3(transform.position.x + barrelLenght * Mathf.Cos((angle) * Mathf.Deg2Rad),
                        transform.position.y + barrelLenght * Mathf.Sin((angle) * Mathf.Deg2Rad),0),
                        angle,
                        gameObject);
                }
                
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

                if (tempBullet.GetComponent<JarThrow>())
                {
                    tempBullet.GetComponent<JarThrow>().Ply = transform.parent.gameObject;
                }
                if (tempBullet.GetComponent<ShotgunShell>())
                {
                    tempBullet.GetComponent<ShotgunShell>().Ply = transform.parent.gameObject;
                    if(damagePerBullet!=0) 
                        tempBullet.GetComponent<ShotgunShell>().damagePerBullet = damagePerBullet;
                }

                usedAmmo++;
                checkAmmo();
                
            }
        }
            
    }

    public void Update()
    {
        if (justShot && !fist)
        {
            float distanceLeft = (maxRecoilDistance - (posWeaponOriginal.y - transform.localPosition.y));
            if (distanceLeft >= recoilSpeed)
            {
                //Debug.Log("Recule de recoilSpeed");
                transform.localPosition -= new Vector3(0, recoilSpeed);
            }
            else 
            if(distanceLeft >0)
            {
                //Debug.Log("Stay de recoilSpeed");
                transform.localPosition -= new Vector3(0, distanceLeft);
            }

            justShot = false;
        }
        else
        {
            if (transform.localPosition.y < posWeaponOriginal.y)
            {
                transform.localPosition += new Vector3(0,  recoilSpeedBack*0.01f);
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
             
             if (water)
             {
                 if (waterBullet)
                 {
                     Debug.Log("DECOUPLE WATER");
                     waterBullet.GetComponent<WaterGenerator>().shooting = false;
                     waterBullet = null;   
                     
                 }
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