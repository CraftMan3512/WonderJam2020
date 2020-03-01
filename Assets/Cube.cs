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

    public void TakeDamage(int damage)
    {
        
        GameObject.Find("Main Camera").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("SFX/BoxDmg"));

        Hp -= damage;
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }

        float pourcent = ((float) Hp / (float) MaxHp)*100f;
        if (pourcent >= 67f)
        {
            
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Boxes/" + GetComponent<SpriteRenderer>().sprite.name.Substring(0, GetComponent<SpriteRenderer>().sprite.name.Length - 1) + "1");   
            
        }else if (pourcent >= 33f) GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Boxes/" + GetComponent<SpriteRenderer>().sprite.name.Substring(0, GetComponent<SpriteRenderer>().sprite.name.Length - 1) + "2");

        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        transform.position += new Vector3(0.05f, 0.05f, 0);
        yield return new WaitForSeconds(0.1f);
        transform.position -= new Vector3(0.05f, 0.05f, 0);
    }
}
