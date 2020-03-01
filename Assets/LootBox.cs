using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootBox : MonoBehaviour
{
    private GameObject gun;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player")&&other.gameObject.GetComponent<Player>().GlovesOn())
        {
            other.gameObject.GetComponent<Player>().EquipGun(gun);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("SFX/Shoot"));
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gun=Resources.LoadAll<GameObject>("Guns")[Random.Range(0, Resources.LoadAll<GameObject>("Guns").Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
