using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public GameObject scoreText;
    public int score;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        // Get the score from the GameManager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        score = 0;       
    }

    // Update is called once per frame
    void Update()
    {
        Text text;
        // Update the score
        score = gameManager.getScore();
        text = scoreText.GetComponent<Text>(); 
        text.text = "SCORE: " + score.ToString();     
    }
}
