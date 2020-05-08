using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkScript : MonoBehaviour
{
    public GameObject playingfield;
    public float growRate = -0.1f;

    void Update()
    {
        playingfield.transform.localScale += new Vector3(0.1f, 0.1f, 1) * growRate * Time.deltaTime;

        GameObject player = GameObject.Find("Steve_Square");
        ScoreScript steve = player.GetComponent<ScoreScript>();
        if (steve.score == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<ShrinkScript>().enabled = false;
        }

        HealthScript health = player.GetComponent<HealthScript>();
        if (health.hp <= 1)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<ShrinkScript>().enabled = false;
        }

        GameObject clock = GameObject.Find("Timer");
        TimerScript t = clock.GetComponent<TimerScript>();
        if (t.timer >= t.limit)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<ShrinkScript>().enabled = false;
        }
    }
}
