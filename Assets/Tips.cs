using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Tips : MonoBehaviour
{

    private string[] tips =
    {
        "Use your Frenzy when surrounded by a horde of zombies for maximum damage!",
        "Making hallways with boxes is a useful way to slow down zombies!",
        "Block the passageways to gain time to prepare for the invasion!",
        "You can steal your opponent's guns, just give them a little punch!",
        "This game was made in 48 hours!",
        "Don't forget: the other players are opponents!",
        "The more you wait, the sturdier boxes you get!",
        "When you see a foe going for a steal, use your frenzy; They can't steal that!",
        "If you're the last one standing, beware of ninjas!",
        "Your score depends on the number of kills you get, so go all in and get some kills!"
    };

    private void Start()
    {

        GetComponent<TextMeshProUGUI>().text = "Tip: " + tips[Random.Range(0,tips.Length)];

    }
}
