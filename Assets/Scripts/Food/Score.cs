using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;

    [SerializeField] int pointsPerFood = 100;
    [SerializeField] Text scoreText = null;
    [SerializeField] Text highscoreText = null;
    public int score = 0;
    public int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore");
        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "Highscore: " + highscore.ToString();
    }

    public void AddPoint()
    {
        score += pointsPerFood;
        scoreText.text = "Score: " + score.ToString();
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }

    public void SubtractPoint()
    {
        if(score >= pointsPerFood)
        {
            score -= pointsPerFood;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
