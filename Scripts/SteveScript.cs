using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveScript : MonoBehaviour
{
    public int gameScore = 0;
    public Vector2 speed = new Vector2(12, 12);
    public Vector2 fast = new Vector2(30, 30);
    public Vector2 slow = new Vector2(5, 5);
    private Vector3 movement = new Vector3();
    public Vector2 dir = new Vector2();
    public bool canMove = true;
    public bool speedy = false;
    public bool slowly = false;
    public Sprite sprite1; // Drag your first sprite here
    public Sprite sprite2; // Drag your second sprite here
    public Sprite sprite3;
    public Sprite sprite4;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = sprite1; // set the sprite to sprite1
    }

    void Update()
    {
        if (canMove)
        {
            GameObject playfield = GameObject.Find("Horizontal Playfield");
            RestrictScript field = playfield.GetComponent<RestrictScript>();

            if(speedy == true)
            {
                float inputX = Input.GetAxis("Horizontal");
                float inputY = Input.GetAxis("Vertical");
                setDirection(inputX, inputY);

                movement = new Vector3(fast.x * inputX, fast.y * inputY, 0);
            }

            if(slowly == true)
            {
                float inputX = Input.GetAxis("Horizontal");
                float inputY = Input.GetAxis("Vertical");
                setDirection(inputX, inputY);

                movement = new Vector3(slow.x * inputX, slow.y * inputY, 0);
            }

            if (speedy != true && slowly != true)
            {
                float inputX = Input.GetAxis("Horizontal");
                float inputY = Input.GetAxis("Vertical");
                setDirection(inputX, inputY);

                movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);
            }

            movement *= Time.deltaTime;
            transform.Translate(movement);

            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(transform.position.x, field.minX + 1.3f, field.maxX - 1.3f);
            clampedPosition.y = Mathf.Clamp(transform.position.y, field.minY + 1.60f, field.maxY - 1.05f);
            transform.position = clampedPosition;
        }

        GameObject player = GameObject.Find("Steve_Square");
        ScoreScript steve = player.GetComponent<ScoreScript>();
        if (steve.score == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SteveScript>().enabled = false;
        }

        HealthScript health = player.GetComponent<HealthScript>();
        if (health.hp <= 1)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SteveScript>().enabled = false;
        }

        GameObject clock = GameObject.Find("Timer");
        TimerScript t = clock.GetComponent<TimerScript>();
        if (t.timer >= t.limit)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SteveScript>().enabled = false;
        }
    }

    public Vector2 setDirection(float x, float y)
    {
        dir.x = x;
        dir.y = y;
        return dir;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (canMove && coll.gameObject.tag == "bouncer")
        {
            StartCoroutine(moveTimer());
        }

        if (coll.gameObject.tag == "star")
        {
            StartCoroutine(Flasher());
            StarSound sound = GameObject.FindObjectOfType(typeof(StarSound)) as StarSound;
            sound.TriggerSound();
        }

        if (coll.gameObject.tag == "powerup")
        {
            StartCoroutine(UpFlash());
        }

        if (coll.gameObject.tag == "powerdown")
        {
            StartCoroutine(DownFlash());
        }
    }

    private IEnumerator moveTimer()
    {
        canMove = false;
        yield return new WaitForSeconds(0.75f);
        canMove = true;
    }

    private IEnumerator Flasher()
    {
        spriteRenderer.sprite = sprite2;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = sprite1;
    }

    private IEnumerator UpFlash()
    {
        spriteRenderer.sprite = sprite3;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = sprite1;
    }

    private IEnumerator DownFlash()
    {
        spriteRenderer.sprite = sprite4;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = sprite1;
    }

    public void speedIncrease()
    {
        StartCoroutine(speedUp());
    }

    public IEnumerator speedUp()
    {
        speedy = true;
        yield return new WaitForSeconds(2.5f);
        speedy = false;
    }

    public void speedDecrease()
    {
        StartCoroutine(speedDown());
    }

    public IEnumerator speedDown()
    {
        slowly = true;
        yield return new WaitForSeconds(2.5f);
        slowly = false;
    }

    public void tiny()
    {
        StartCoroutine(smaller());
    }

    public IEnumerator smaller()
    {
        transform.localScale =  new Vector3(0.1f, 0.1f);
        yield return new WaitForSeconds(2.5f);
        transform.localScale = new Vector3(0.25f, 0.25f);
    }

    public void jumbo()
    {
        StartCoroutine(larger());
    }

    public IEnumerator larger()
    {
        transform.localScale = new Vector3(0.5f, 0.5f);
        yield return new WaitForSeconds(2.5f);
        transform.localScale = new Vector3(0.25f, 0.25f);
    }
}
