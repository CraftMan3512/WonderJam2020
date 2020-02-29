using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Controls controls;
    public Gun gunPrefab;
    private Vector2 hands;
    private Gun gunModel;
    
    //TURNING 
    private Vector3 mouse_pos;
    private Transform target; //Assign to the object you want to rotate
    private Vector3 object_pos;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        target = this.transform;
        hands=transform.Find("Hands").GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
        if (gunPrefab)
        {
            gunModel=Instantiate(gunPrefab,
                new Vector3(transform.position.x + hands.x + gunPrefab.gunLenght*Mathf.Cos((transform.eulerAngles.z+90)*Mathf.Deg2Rad), transform.position.y + hands.y+ gunPrefab.gunLenght*Mathf.Sin((transform.eulerAngles.z+90)*Mathf.Deg2Rad), transform.position.z),
                Quaternion.identity);
            gunModel.transform.parent = gameObject.transform;
        }
        else
        {
            
        }
    }

    private void Awake()
    {
        controls = new Controls();
        controls.PlayerControls.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.PlayerControls.Enter.performed += ctx => gunModel.Shoot(transform.eulerAngles.z); 
    }
    
    private void Move(Vector2 direction)
    {
        this.direction = direction;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void Update()
    {
        
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
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
    }
}
