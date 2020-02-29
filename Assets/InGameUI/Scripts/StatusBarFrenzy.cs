using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarFrenzy : MonoBehaviour
{

    private Transform bar;
    
    // Start is called before the first frame update
    private void Start()
    {
        try
        {
            bar = transform.Find("Bar");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public void updateSize(float newSize)
    {
        bar.localScale = new Vector3(newSize, 1f);
    }
    
}
