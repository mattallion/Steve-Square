using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(0.05f, 0.05f);
    public Vector2 dir = new Vector2(-1, 0);

    void Update()
    {
        Vector3 movement = new Vector3(speed.x * dir.x, speed.y * dir.y, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);

        GameObject player = GameObject.Find("Steve_Square");
        ScoreScript steve = player.GetComponent<ScoreScript>();

        if (steve.score == 3)
        {
            gameObject.GetComponent<RightScript>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}