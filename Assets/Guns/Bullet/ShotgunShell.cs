﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShell : MonoBehaviour
{
    public float speed=1;
    public float angleRdm=10;
    public GameObject ply;

    public GameObject Ply
    {
        get => ply;
        set => ply = value;
    }

    public int damagePerBullet=1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<BulletScript>().damage = this.damagePerBullet;
            transform.GetChild(i).gameObject.GetComponent<BulletScript>().speed = this.speed;
            transform.GetChild(i).gameObject.GetComponent<BulletScript>().angleRdm = this.angleRdm;
            transform.GetChild(i).gameObject.GetComponent<BulletScript>().Ply = ply;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }   
    }
}
