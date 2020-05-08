using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Empty4Script : MonoBehaviour
{
    void Update()
    {
        GameObject player = GameObject.Find("Steve_Square");
        ScoreScript steve = player.GetComponent<ScoreScript>();
        if (SceneManager.GetActiveScene().buildIndex <= 5)
        {
            if (steve.score == 4)
            {
                Destroy(gameObject);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex > 5 && SceneManager.GetActiveScene().buildIndex <= 9)
        {
            if (steve.score == 3)
            {
                Destroy(gameObject);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex > 9)
        {
            if (steve.score == 2)
            {
                Destroy(gameObject);
            }
        }
    }
}

