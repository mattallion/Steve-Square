using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBallsScript : MonoBehaviour
{
    public Vector3 pos = new Vector3();
    public float time = 0.0f;
    public float loadTime;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        float[] array = { -12f, -11f, -10f, -9f, -8f, -7f, -6f, 6f, 7f, 8f, 9f, 10f, 11f, 12f };
        int randx = Random.Range(0, 13);
        int randy = Random.Range(0, 13);
        float x = array[randx];
        float y = array[randy];
        pos = new Vector3(x, y, 0f);
        transform.position = pos;

        float[] timeArray = { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        int randt = Random.Range(0, 15);
        loadTime = timeArray[randt];
    }

    void Update()
    {
        GameObject playfield = GameObject.Find("Horizontal Playfield");
        RestrictScript field = playfield.GetComponent<RestrictScript>();

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, field.minX, field.maxX);
        clampedPosition.y = Mathf.Clamp(transform.position.y, field.minY, field.maxY);
        transform.position = clampedPosition;

        GameObject clock = GameObject.Find("Timer");
        TimerScript t = clock.GetComponent<TimerScript>();
        time = t.timer;
        if (time >= loadTime)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
        }

        GameObject player = GameObject.Find("Steve_Square");
        ScoreScript steve = player.GetComponent<ScoreScript>();
        if (steve.score == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SmallBallsScript>().enabled = false; //change the green portion between <> to current script name
        }

        HealthScript health = player.GetComponent<HealthScript>();
        if (health.hp <= 1)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SmallBallsScript>().enabled = false; //change the green portion between <> to current script name
        }

        if (t.timer >= t.limit)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SmallBallsScript>().enabled = false; //change the green portion between <> to current script name
        }
    }

    void OnCollisionEnter2D(Collision2D coll) //this portion of the code is what happens when Steve collides with the item
                                              // you will have to customize it to act according to the power-up or power-down
    {
        if (coll.gameObject.tag == "Player")
        {
            StarSound sound = GameObject.FindObjectOfType(typeof(StarSound)) as StarSound;
            sound.TriggerSound();
            MoveScript[] bouncers = FindObjectsOfType<MoveScript>();
            bouncers[0].smallBall();
            bouncers[1].smallBall();
            bouncers[2].smallBall();
            Destroy(gameObject);
        }
    }
}
