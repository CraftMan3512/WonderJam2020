using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool fist = false;
    public bool spread = false; //TODO 
    public float rpm = 120;
    public int dmgPerBullet = 1;

    public string name;
    public GameObject bullet;
    private ArrayList bullets;
    public float barrelLenght;
    public float gunLenght;

    public void Shoot(float angle)
    {
        Debug.Log("Shot");
        GameObject tempBullet = Instantiate(bullet,
            new Vector3(transform.position.x+barrelLenght*Mathf.Cos((angle+90)*Mathf.Deg2Rad), transform.position.y+barrelLenght*Mathf.Sin((angle+90)*Mathf.Deg2Rad), transform.position.z),
            transform.rotation);
        tempBullet.GetComponent<BulletScript>().damage = dmgPerBullet;
    }
}