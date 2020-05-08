using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float timer = 0.0f;
    public float limit = 10.0f;

    void Update()
    {
        timer += Time.deltaTime;

        GameObject player = GameObject.Find("Steve_Square");
        ScoreScript steve = player.GetComponent<ScoreScript>();
        if (steve.score == 3)
        {
            gameObject.GetComponent<TimerScript>().enabled = false;
        }

        HealthScript health = player.GetComponent<HealthScript>();
        if (health.hp <= 1)
        {
            gameObject.GetComponent<TimerScript>().enabled = false;
        }

        if (timer >= limit)
        {
            gameObject.GetComponent<TimerScript>().enabled = false;
        }
    }
}
