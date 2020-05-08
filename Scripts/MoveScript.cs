using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(1.1f, 1.1f);
    public Vector2 dir = new Vector2();
    public bool slow = false;
    public bool fast = false;
    public SpriteRenderer render;
    public Vector2 startSpeed = new Vector2();
    public Vector2 downSpeed = new Vector2();
    public Vector2 upSpeed = new Vector2();

    void Start()
    {
        float[] array = { -3, -2, -1, 1, 2, 3 };
        int randx = Random.Range(0, 5);
        int randy = Random.Range(0, 5);

        float x = array[randx];
        float y = array[randy];
        dir = newDirection(x, y);
        getStartSpeed();
        setDownSpeed();
        setUpSpeed();
    }

    public Vector2 getStartSpeed()
    {
        startSpeed.x = speed.x;
        startSpeed.y = speed.y;
        return startSpeed;
    }

    public Vector2 setDownSpeed()
    {
        downSpeed.x = startSpeed.x * 0.5f;
        downSpeed.y = startSpeed.y * 0.5f;
        return downSpeed;
    }

    public Vector2 setUpSpeed()
    {
        upSpeed.x = startSpeed.x * 2f;
        upSpeed.y = startSpeed.y * 2f;
        return upSpeed;
    }

    void Update()
    {
        if (slow == true)
        {
            Vector3 movement = new Vector3(downSpeed.x * dir.x, downSpeed.y * dir.y, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
        }

        if (fast == true)
        {
            Vector3 movement = new Vector3(upSpeed.x * dir.x, upSpeed.y * dir.y, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
        }

        if (slow != true && fast != true)
        {
            Vector3 movement = new Vector3(speed.x * dir.x, speed.y * dir.y, 0);
            movement *= Time.deltaTime;
            transform.Translate(movement);
        }

        GameObject player = GameObject.Find("Steve_Square");
        ScoreScript steve = player.GetComponent<ScoreScript>();
        if (steve.score == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<MoveScript>().enabled = false;
        }

        HealthScript health = player.GetComponent<HealthScript>();
        if (health.hp <= 1)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<MoveScript>().enabled = false;
        }

        GameObject clock = GameObject.Find("Timer");
        TimerScript t = clock.GetComponent<TimerScript>();
        if (t.timer >= t.limit)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<MoveScript>().enabled = false;
        }
    }

    public bool slowSpeed()
    {
        StartCoroutine(SlowDown());
        return slow;
    }

    private IEnumerator SlowDown()
    {
        slow = true;
        fast = false;
        yield return new WaitForSeconds(2.5f);
        slow = false;
        fast = false;
    }

    public bool fastSpeed()
    {
        StartCoroutine(SpeedUp());
        return slow;
    }

    private IEnumerator SpeedUp()
    {
        fast = true;
        slow = false;
        yield return new WaitForSeconds(2.5f);
        fast = false;
        slow = false;
    }

    public void initiateRemove()
    {
        StartCoroutine(removeBall());
    }

    public IEnumerator removeBall()
    {
        render.color = new Color(1f, 1f, 1f, .5f);
        gameObject.layer = LayerMask.NameToLayer("removeBall");
        yield return new WaitForSeconds(5f);
        render.color = new Color(1f, 1f, 1f, 1f);
        gameObject.layer = LayerMask.NameToLayer("Bouncer");
    }

    public void smallBall()
    {
        StartCoroutine(smaller());
    }

    public IEnumerator smaller()
    {
        transform.localScale = new Vector3(0.5f, 0.5f);
        yield return new WaitForSeconds(5f);
        transform.localScale = new Vector3(1.0f, 1.0f);
    }

    public void largeBall()
    {
        StartCoroutine(larger());
    }

    public IEnumerator larger()
    {
        transform.localScale = new Vector3(1.25f, 1.25f);
        yield return new WaitForSeconds(5f);
        transform.localScale = new Vector3(1.0f, 1.0f);
    }

    public Vector2 OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "horizontal")
        {
            dir = newDirection(dir.x, -(dir.y));
        }

        if (coll.gameObject.tag == "vertical")
        {
            dir = newDirection(-(dir.x), dir.y);
        }

        if (coll.gameObject.tag == "bouncer")
        {
            MoveScript bouncer1 = gameObject.GetComponent<MoveScript>();
            Vector2 position1 = gameObject.transform.position;
            Vector2 position2 = coll.gameObject.transform.position;
            Vector2 normal = (position2 - position1); // normal of collision plane (not normalised).
            float nn = Vector2.Dot(normal, normal); // square length of normal of collision
            float vn = Vector2.Dot(normal, bouncer1.dir); // impact velocity (collision impulse).
            bouncer1.dir -= (2.0f * (vn / nn)) * normal; // reflect along the collision plane. 
            dir = bouncer1.dir;
        }

        if (coll.gameObject.tag == "Player")
        {
            MoveScript bouncer1 = gameObject.GetComponent<MoveScript>();
            Vector2 pos1 = gameObject.transform.position;
            Vector2 pos2 = coll.gameObject.transform.position;
            Vector2 normal = (pos2 - pos1); // normal of collision plane (not normalised).
            float nn = Vector2.Dot(normal, normal); // square length of normal of collision
            float vn = Vector2.Dot(normal, bouncer1.dir); // impact velocity (collision impulse).

            Vector2 dir1 = dir;
            GameObject player = GameObject.Find("Steve_Square");
            SteveScript steve = player.GetComponent<SteveScript>();
            if (steve.dir.x > 0f)
            {
                steve.dir.x = 0.75f;
            }
            if (steve.dir.x < 0f)
            {
                steve.dir.x = -0.75f;
            }
            if (steve.dir.y > 0f)
            {
                steve.dir.y = 0.75f;
            }
            if (steve.dir.y < 0f)
            {
                steve.dir.y = -0.75f;
            }
            if (((dir.x > 0 && steve.dir.x > 0) && ((dir.y < 0 && steve.dir.y < 0) || (dir.y > 0 && steve.dir.y > 0))) && (pos1.x > pos2.x))
            {
                return bouncer1.dir = (2.0f * (vn / nn)) * normal; // reflect along the collision plane.
            }

            if (((dir.x < 0 && steve.dir.x < 0) && ((dir.y < 0 && steve.dir.y < 0) || (dir.y > 0 && steve.dir.y > 0))) && (pos1.x < pos2.x))
            {
                return bouncer1.dir = (2.0f * (vn / nn)) * normal; // reflect along the collision plane.
            }

            if (((dir.y > 0 && steve.dir.y > 0) && ((dir.x < 0 && steve.dir.x < 0) || (dir.x > 0 && steve.dir.x > 0))) && (pos1.y < pos2.y))
            {
                return bouncer1.dir = (2.0f * (vn / nn)) * normal; // reflect along the collision plane. 
            }

            if (((dir.y < 0 && steve.dir.y < 0) && ((dir.x < 0 && steve.dir.x < 0) || (dir.x > 0 && steve.dir.x > 0))) && (pos1.y > pos2.y))
            {
                return bouncer1.dir = (2.0f * (vn / nn)) * normal; // reflect along the collision plane. 
            }

            else
            {
                return bouncer1.dir -= (2.0f * (vn / nn)) * normal; // reflect along the collision plane. 
            }
        }
        return dir;
    }

    public Vector2 newDirection(float x, float y)
    {
        dir.x = x;
        dir.y = y;
        return dir;
    }

}