using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    int score = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    public void AddPoint()
    {
        score += 1;
    }

    void Update()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }
}
