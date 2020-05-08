using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(1f, 1f);
    public Vector2 dir = new Vector2(0f, 0f);
    public bool[] wallHits = new bool [4];

    private void Start()
    {
        setWallHits();
    }

    public bool[] setWallHits()
    {
        for (int i = 0; i < 4; i++)
        {
            wallHits[i] = false;
        }
        return wallHits;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (coll.gameObject.tag == "vertical" && gameObject.transform.position.x > 0)
        {
            setHitRight();
        }

        if (coll.gameObject.tag == "vertical" && gameObject.transform.position.x < 0)
        {
            setHitLeft();
        }

        if (coll.gameObject.tag == "horizontal" && gameObject.transform.position.y > 0)
        {
            setHitTop();
        }

        if (coll.gameObject.tag == "horizontal" && gameObject.transform.position.y < 0)
        {
            setHitBottom();
        }
    }

    public bool[] setHitRight()
    {
        setWallHits();
        wallHits[1] = true;
        return wallHits;
    }

    public bool[] setHitLeft()
    {
        setWallHits();
        wallHits[0] = true;
        return wallHits;
    }

    public bool[] setHitTop()
    {
        setWallHits();
        wallHits[2] = true;
        return wallHits;
    }

    public bool[] setHitBottom()
    {
        setWallHits();
        wallHits[3] = true;
        return wallHits;
    }

    void Update()
    {
        if (wallHits[0] == true)
        {
            Vector3 movement = new Vector3(speed.x * 1, 0, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
        }

        if (wallHits[1] == true)
        {
            Vector3 movement = new Vector3(speed.x * -1, 0, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
        }

        if (wallHits[2] == true)
        {
            Vector3 movement = new Vector3(0, speed.y * -1, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
        }

        if (wallHits[3] == true)
        {
            Vector3 movement = new Vector3(0, speed.y * 1, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
        }

        GameObject player = GameObject.Find("Steve_Square");
        HealthScript steve = player.GetComponent<HealthScript>();
        if (steve.hp <= 1)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<StarScript>().enabled = false;
        }

        GameObject clock = GameObject.Find("Timer");
        TimerScript t = clock.GetComponent<TimerScript>();
        if (t.timer >= t.limit)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<StarScript>().enabled = false;
        }
    }
}
