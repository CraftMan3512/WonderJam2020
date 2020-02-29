using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public int maxHealth=100;
    public int maxFrenezie = 100;
    
    private int health;

    public int Health
    {
        get => health;
    }

    public int Frenezie
    {
        get => frenezie;
    }


    private int frenezie;
    private float crateLevel = 0;
    private int score;
    
    
    public float movementSpeed;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 direction2;
    public Gun gunPrefab;
    private Vector2 hands;
    private GameObject gunModel;
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
        score = 0;
        health = maxHealth;
        frenezie = 0;
        crateLevel = 0;

        gloves = transform.GetChild(0).gameObject.GetComponent<Gun>();
        target = this.transform;
        hands=transform.Find("Hands").GetComponent<Transform>().localPosition;
        rb = GetComponent<Rigidbody2D>();
        if (gunPrefab)
        {
            EquipGun(gunPrefab.gameObject);
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
            if(gunModel) gunModel.GetComponent<Gun>().Shooting();
        }
        if (Input.GetButtonUp("submit"+player))
        {
            shooting = false;
            if(gunModel) gunModel.GetComponent<Gun>().Stopped();
        }
        
        if (Input.GetAxisRaw("rightx" + player) != 0 || Input.GetAxisRaw("righty" + player) != 0)
        {
            
            angle = Mathf.Atan2(Input.GetAxisRaw("rightx" + player), Input.GetAxisRaw("righty" + player)) * Mathf.Rad2Deg;
            angle -= 90;   
            
        }

        if (shooting.Equals(true))
        {
            if(!glovesOn)
            gunModel.GetComponent<Gun>().Shoot(angle);
            else
            gloves.Shoot(angle);
        }

    }

    private void Move(Vector2 direction)
    {
        this.direction = direction;
    }

    public bool GlovesOn()
    {
        return glovesOn;
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

    public void DestroyGun()
    {
        Destroy(gunModel.gameObject);
        glovesOn = true;
        gloves.gameObject.SetActive(true);
    }

    public void EquipGun(GameObject gun)
    {
        gloves.gameObject.SetActive(false);
        glovesOn = false;
        gunModel=Instantiate(gun,Vector3.zero,transform.rotation,transform);
        gunModel.transform.localPosition = new Vector3(hands.x,
            hands.y +
            gun.GetComponent<Gun>().gunLenght, transform.position.z);
    }

    public GameObject getGun()
    {
        return gunModel.gameObject;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health < 0)
            health = 0;
    }
    public int Score
    {
        get => score;
    }

    public void addScore(int nb)
    {
        score += nb;
    }
}
