using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManger : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    private int score;
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EarnScore(int newScore)
    {
        score = score + newScore;
        scoreText.text = "Score: " + score;
    }

    public void LivesCounter(int newLife)
    {
        livesText.text = "Lives: " + newLife;
    }
}
