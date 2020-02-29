using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    private void OnBecameInvisible()
    {
        Destroy(this);
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(100*speed*Mathf.Cos((transform.eulerAngles.z+90)*Mathf.Deg2Rad),100*speed*Mathf.Sin((transform.eulerAngles.z+90)*Mathf.Deg2Rad)));
    }
}
