using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoseScript : MonoBehaviour
{
    public Scene currentScene;
    public string firstScene;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        GameObject player = GameObject.Find("Steve_Square");
        HealthScript steve = player.GetComponent<HealthScript>();

        if (steve.hp <= 1)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Vector3 pos = new Vector3(0f, 0f, -5f);
            Quaternion rot = new Quaternion();
            transform.SetPositionAndRotation(pos, rot);
        }

        if (steve.hp <= 1 && Input.GetKeyDown("r"))
        {
            currentScene = SceneManager.GetActiveScene();

            if (currentScene.name != firstScene)
            {
                GameScore gScore = GameObject.FindObjectOfType(typeof(GameScore)) as GameScore;
                gScore.decrementScore();
            }

            GameScore gameScore = GameObject.FindObjectOfType(typeof(GameScore)) as GameScore;
            gameScore.resetScore();
            ScoreScript levelScore = GameObject.FindObjectOfType(typeof(ScoreScript)) as ScoreScript;
            levelScore.setScoreText();

            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

}
