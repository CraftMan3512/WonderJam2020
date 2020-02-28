using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTester : MonoBehaviour
{
    private Controls controls;
    private Vector2 direction;
    public GameObject[] xy;
    public GameObject enter;
    public GameObject cancel;

    void Awake()
    {

        controls = new Controls();
        controls.PlayerControls.Movement.performed += (context) =>
        {

            direction = context.ReadValue<Vector2>();

        };
        controls.PlayerControls.Enter.performed += ctx => CheckEnter(ctx.ReadValue<float>());
        controls.PlayerControls.Cancel.performed += ctx => CheckCancel(ctx.ReadValue<float>());

    }

    private void CheckEnter(float val)
    {
        if (val == 1) enter.GetComponent<Image>().color = Color.white;
        else enter.GetComponent<Image>().color = Color.black;
    }
    
    private void CheckCancel(float val)
    {
        if (val == 1) cancel.GetComponent<Image>().color = Color.white;
        else cancel.GetComponent<Image>().color = Color.black;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //input visualization
        if (direction.x == 1) xy[3].GetComponent<Image>().color = Color.white;
        else xy[3].GetComponent<Image>().color = Color.black;
        if (direction.x == -1) xy[2].GetComponent<Image>().color = Color.white;
        else xy[2].GetComponent<Image>().color = Color.black;
        if (direction.y == 1) xy[0].GetComponent<Image>().color = Color.white;
        else xy[0].GetComponent<Image>().color = Color.black;
        if (direction.y == -1) xy[1].GetComponent<Image>().color = Color.white;
        else xy[1].GetComponent<Image>().color = Color.black;

    }

    private void OnEnable()
    {
        
        controls.Enable();
        
    }

    private void OnDisable()
    {
        
        controls.Disable();
        
    }
}
