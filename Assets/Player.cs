using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    
    public int maxHealth=100;
    public float maxFrenezie = 100;
    
    private int health;

    public int Health
    {
        get => health;
    }

    public float Frenezie
    {
        get => frenezie;
    }

    private bool inFrenezie;
    private float frenezie;
    private int score;
    private GameObject backGun;
    
    
    public float movementSpeed=5f;
    public float mvmtSpdModWithWp=0.8f;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 direction2;
    public GameObject gunPrefab;
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

    private List<GameObject> allFrenezieGuns;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        health = maxHealth;
        frenezie = 0;

        gloves = transform.GetChild(0).gameObject.GetComponent<Gun>();
        target = transform;
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

        allFrenezieGuns=Resources.LoadAll<GameObject>("FrenezieGuns").ToList();
    }

    private void Update()
    {
        //Check si on tire si oui quel arme ou gloves
        if (shooting.Equals(true))
        {
            if(!glovesOn)
                gunModel.GetComponent<Gun>().Shoot(angle);
            else
                gloves.Shoot(angle);
        }
        //get new inputs
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
        //Frenzy
        if (Input.GetButtonDown("cancel" + player)&&maxFrenezie<=frenezie)
        {
            inFrenezie = true;
            GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("SFX/Frenzy"));
            if (!glovesOn)
            {
                backGun = Instantiate(gunModel);
                backGun.GetComponent<Gun>().UsedAmmo = getGun().GetComponent<Gun>().UsedAmmo;
                backGun.SetActive(false);
                DestroyGun();
            }
            EquipGun(FrenezieGunRandom());
        }

        if (!inFrenezie)
        {
            if (frenezie < maxFrenezie)
            {
                frenezie += 1f * Time.deltaTime;
            }
            else
                frenezie = maxFrenezie;
        }
        else
        {
            if (frenezie > 0)
            {
                frenezie -= 10f * Time.deltaTime;
            }
            else
            {
                frenezie = 0;
                DestroyGun();
                if (backGun)
                {
                    backGun.SetActive(true);
                    EquipGun(backGun);
                    getGun().GetComponent<Gun>().UsedAmmo = backGun.GetComponent<Gun>().UsedAmmo;
                }
                Destroy(backGun);
                backGun = null;
                inFrenezie = false;
            }
        }
            
    }

    private GameObject FrenezieGunRandom()
    {
        int i = Random.Range(0, allFrenezieGuns.Count);
        GameObject temp = allFrenezieGuns[i];
        return temp;
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
        float modifier;
        if (!glovesOn)
            modifier = mvmtSpdModWithWp;
        else
            modifier = 1;

        rb.position += direction * (modifier * (movementSpeed * Time.fixedDeltaTime));
        
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
        return gunModel;
    }

    public void TakeDamage(int dmg)
    {
        //Debug.Log("PLPAYER TAKE DAMANGEE!!!! : " + dmg);
        GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("SFX/PlayerHit"),1.5f);
        
        health -= dmg;
        if (health < 0)
        {
            
            health = 0;
            //death
            DeathCounter.PlayerDied(player,score);
            Destroy(gameObject);
            
        }
    }
    public int Score
    {
        get => score;
    }

    public void addScore(int nb)
    {
        score += nb;
        if(!inFrenezie)
            frenezie += 0.2f * nb;
        else
        {
            if (frenezie < maxFrenezie)
                frenezie += 0.06f * nb;
            else
                frenezie = maxFrenezie;

        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public bool InFrenezie
    {
        get => inFrenezie;
    }
}
