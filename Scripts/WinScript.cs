using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        GameObject player = GameObject.Find("Steve_Square");
        ScoreScript steve = player.GetComponent<ScoreScript>();

        if (steve.score == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Vector3 pos = new Vector3(0f, 0f, -5f);
            Quaternion rot = new Quaternion();
            transform.SetPositionAndRotation(pos, rot);
        }

        if (steve.score == 3 && Input.GetKeyDown(KeyCode.Return))
        {
            nextLevel();
        }
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // loads current scene
    }

}
