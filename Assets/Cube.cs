using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private int hp;
    private int maxHp;

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Hp { get => hp; set => hp = value; }

    // Start is called before the first frame update
    void Start()
    {
        MaxHp = 10;
        Hp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damage)
    {

        Hp -= damage;
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        transform.position += new Vector3(0.05f, 0.05f, 0);
        yield return new WaitForSeconds(0.1f);
        transform.position -= new Vector3(0.05f, 0.05f, 0);
    }
}
