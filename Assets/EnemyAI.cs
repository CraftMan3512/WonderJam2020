using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

  
    private int hp;
    private int maxHp;
    private int damage;
    private GameObject target;

    public int Hp { get => hp; set => hp = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Damage { get => damage; set => damage = value; }
    public GameObject Target { get => target; set => target = value; }

    // Start is called before the first frame update
    void Start()
    {
        maxHp = 20;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }


}
