using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHider : MonoBehaviour
{

    public GameObject canvasp1;
    public GameObject canvasp2;
    public GameObject canvasp3;
    public GameObject canvasp4;

    private bool isShowing;
    
    // Start is called before the first frame update
    void Start()
    {
        isShowing = false;
        //canvasp1.SetActive(isShowing);
        canvasp2.SetActive(isShowing);
        canvasp3.SetActive(isShowing);
        canvasp4.SetActive(isShowing);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
