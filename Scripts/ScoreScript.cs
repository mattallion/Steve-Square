using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public int score;
    public Text scoreText;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex <= 5)
        {
            score = 0;
        }

        if (SceneManager.GetActiveScene().buildIndex > 5 && SceneManager.GetActiveScene().buildIndex <= 9)
        {
            score = -1;
        }

        if (SceneManager.GetActiveScene().buildIndex > 9)
        {
            score = -2;
        }

        setScoreText();
    }

    public int OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "star")
        {
            score = score + 1;
            GameScore gScore = GameObject.FindObjectOfType(typeof(GameScore)) as GameScore;
            gScore.incrementScore();
            setScoreText();
        }
        return score;
    }

    public void setScoreText()
    {
        GameScore gScore = GameObject.FindObjectOfType(typeof(GameScore)) as GameScore;
        scoreText.text = "STAR COLLECTION: " + gScore.total.ToString();
    }
}
