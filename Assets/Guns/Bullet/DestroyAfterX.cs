using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterX : MonoBehaviour
{
    private float timer;

    public float timeTillDestroy=5;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeTillDestroy)
        {
            Destroy(gameObject);
        }
    }
}
