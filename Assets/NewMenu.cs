using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMenu : MonoBehaviour
{
    public GameObject playersMenu;
    public GameObject mainMenu;


    public void ToPlayerChoice()
    {
        
       playersMenu.SetActive(true);
       mainMenu.SetActive(false);
        
    }

}
    
