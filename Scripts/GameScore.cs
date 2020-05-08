using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScore : MonoBehaviour
{
    public int total;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int incrementScore()
    {
        total = total + 1;
        return total;
    }

    public int decrementScore()
    {
        if (total >= 1)
        {
            total = total - 1;
        }

        else
        {
            total = 0;
        }

        return total;
    }

    public int resetScore()
    {
        ScoreScript levelScore = GameObject.FindObjectOfType(typeof(ScoreScript)) as ScoreScript;

        if (SceneManager.GetActiveScene().buildIndex <= 5)
        {

            if (levelScore.score == 1)
            {
                total = total - 1;
            }

            if (levelScore.score == 2)
            {
                total = total - 2;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex > 5 && SceneManager.GetActiveScene().buildIndex <= 9)
        {
            if (levelScore.score == 0)
            {
                total = total - 1;
            }

            if (levelScore.score == 1)
            {
                total = total - 2;
            }

            if (levelScore.score == 2)
            {
                total = total - 3;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex > 9)
        {
            if (levelScore.score == -1)
            {
                total = total - 1;
            }

            if (levelScore.score == 0)
            {
                total = total - 2;
            }

            if (levelScore.score == 1)
            {
                total = total - 3;
            }

            if (levelScore.score == 2)
            {
                total = total - 4;
            }
        }
        
        return total;
    }
}
