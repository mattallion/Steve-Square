using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictScript : MonoBehaviour
{
    public SpriteRenderer field;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    public bool isWallHit = false;

    void Start()
    {
        maxX = 16;
        minX = -16;
        maxY = 16;
        minY = -16;
        field = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        maxX = field.bounds.extents.x;
        minX = -field.bounds.extents.x;
        maxY = field.bounds.extents.y;
        minY = -field.bounds.extents.y;

        GameObject player = GameObject.Find("Steve_Square");
        ScoreScript steve = player.GetComponent<ScoreScript>();
        if (steve.score == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<RestrictScript>().enabled = false;
        }

        HealthScript health = player.GetComponent<HealthScript>();
        if (health.hp <= 1)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<RestrictScript>().enabled = false;
        }

        GameObject clock = GameObject.Find("Timer");
        TimerScript t = clock.GetComponent<TimerScript>();
        if (t.timer >= t.limit)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<RestrictScript>().enabled = false;
        }
    }
}
