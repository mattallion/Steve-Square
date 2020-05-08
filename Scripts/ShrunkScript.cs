using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ShrunkScript : MonoBehaviour
{
    public Scene currentScene;
    public string firstScene;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        GameObject clock = GameObject.Find("Timer");
        TimerScript t = clock.GetComponent<TimerScript>();

        if (t.timer >= t.limit)
        {
            ShakeScript cam = GameObject.FindObjectOfType(typeof(ShakeScript)) as ShakeScript;
            cam.TriggerShake(1);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Vector3 pos = new Vector3(0f, 0f, -5f);
            Quaternion rot = new Quaternion();
            transform.SetPositionAndRotation(pos, rot);
        }

        if (t.timer >= t.limit && Input.GetKeyDown("r"))
        {
            currentScene = SceneManager.GetActiveScene();

            if (currentScene.name != firstScene)
            {
                GameScore gScore = GameObject.FindObjectOfType(typeof(GameScore)) as GameScore;
                gScore.decrementScore();
            }

            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

}
