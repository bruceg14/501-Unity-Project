using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public Text scoreText;  // public if you want to drag your text object in there manually
    int scoreCounter;
    private MovementCharacter Script;

    void Start()
    {
        scoreCounter = Script.score; 
        scoreText = GetComponent<Text>();  // if you want to reference it by code - tag it if you have several texts
    }

    void Update()
    {
        scoreText.text = scoreCounter.ToString();  // make it a string to output to the Text object
    }
}
