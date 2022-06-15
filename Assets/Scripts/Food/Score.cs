using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;

    [SerializeField] float pointsPerFood = 100;
    [SerializeField] float pointsMult = 1.0f;
    [SerializeField] Text scoreText = null;
    [SerializeField] Text highscoreText = null;
    public float score = 0;
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
        score += pointsPerFood * pointsMult;
        scoreText.text = "Score: " + score.ToString();
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", (int)score);
    }

    public void pointsMultiplier(float mult)
    {
        pointsMult *= mult;
    }

    public void setMultiplier(float mult)
    {
        pointsMult = mult;
    }
}
