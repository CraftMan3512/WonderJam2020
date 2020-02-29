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
    
    //Player ID
    public int player;

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
            glovesOn = false;
            gunModel=Instantiate(gunPrefab,Vector3.zero,Quaternion.identity,transform);
            gunModel.transform.localPosition = new Vector3(0.2f,
                hands.y +
                gunPrefab.gunLenght * Mathf.Sin((transform.eulerAngles.z + 90) * Mathf.Deg2Rad), transform.position.z);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            glovesOn = true;
        }
    }
    
    private void Update()
    {
        
        //get new inputs
        //Debug.Log(Input.GetAxisRaw("x1"));
        direction=new Vector2(Input.GetAxisRaw("x"+player),Input.GetAxisRaw("y"+player));
        if (Input.GetButtonDown("submit"+player))
        {
            shooting = true;
        }
        if (Input.GetButtonUp("submit"+player))
        {
            shooting = false;
        }
        
        if (Input.GetAxisRaw("rightx" + player) != 0 || Input.GetAxisRaw("righty" + player) != 0)
        {
            
            angle = Mathf.Atan2(Input.GetAxisRaw("rightx" + player), Input.GetAxisRaw("righty" + player)) * Mathf.Rad2Deg;
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

    public void SetPlayerNumber(int nb)
    {

        this.player = nb;

    }
    
}
