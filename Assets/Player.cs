using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 direction2;
    public Gun gunPrefab;
    private Vector2 hands;
    private Gun gunModel;
    private bool glovesOn;
    
    //TURNING 
    private Vector3 mouse_pos;
    private Transform target; //Assign to the object you want to rotate
    private Vector3 object_pos;
    private float angle;
    private Gun gloves;

    private bool shooting;

    // Start is called before the first frame update
    void Start()
    {
        gloves = transform.GetChild(0).gameObject.GetComponent<Gun>();
        target = this.transform;
        hands=transform.Find("Hands").GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
        if (gunPrefab)
        {
            gunModel=Instantiate(gunPrefab,
                new Vector3(transform.position.x + hands.x + gunPrefab.gunLenght*Mathf.Cos((transform.eulerAngles.z+90)*Mathf.Deg2Rad), transform.position.y + hands.y+ gunPrefab.gunLenght*Mathf.Sin((transform.eulerAngles.z+90)*Mathf.Deg2Rad), transform.position.z),
                Quaternion.identity);
            gunModel.transform.parent = gameObject.transform;
            glovesOn = false;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            glovesOn = true;
        }
    }
    
    private void Update()
    {
        
        direction=new Vector2(Input.GetAxisRaw("x1"),Input.GetAxisRaw("y1"));
        if (Input.GetButtonDown("submit1"))
        {
            shooting = true;
        }
        if (Input.GetButtonUp("submit1"))
        {
            shooting = false;
        }
        
        if (Input.GetAxisRaw("rightx1") != 0 || Input.GetAxisRaw("righty1") != 0)
        {
            
            angle = Mathf.Atan2(Input.GetAxisRaw("rightx1"), Input.GetAxisRaw("righty1")) * Mathf.Rad2Deg;
            angle -= 90;   
            
        }

        if (shooting.Equals(true))
        {
            if(!glovesOn)
            gunModel.Shoot(angle);
            else
            gloves.Shoot(angle);
        }

    }

    private void Move(Vector2 direction)
    {
        this.direction = direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.position += direction * (movementSpeed * Time.fixedDeltaTime);
        
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
    }
}
