using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int hp = 4;
    public bool isSteve = true;
    public bool canTakeDamage = true;

    public Sprite sprite1; // Drag your first sprite here
    public Sprite sprite2; // Drag your second sprite here

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = sprite1; // set the sprite to sprite1
    }

    public int OnCollisionEnter2D(Collision2D coll)
    {
        if (canTakeDamage && coll.gameObject.tag == "bouncer")
        {
            hp = hp - 1;
            ShakeScript cam = GameObject.FindObjectOfType(typeof(ShakeScript)) as ShakeScript;
            cam.TriggerShake();
            StartCoroutine(damageTimer());
            StartCoroutine(Flasher());
            BouncerSound sound = GameObject.FindObjectOfType(typeof(BouncerSound)) as BouncerSound;
            sound.TriggerSound();
        }
        return hp;
    }

    private IEnumerator damageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1.5f);
        canTakeDamage = true;
    }

    private IEnumerator Flasher()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.sprite = sprite2;
            yield return new WaitForSeconds(.2f);
            spriteRenderer.sprite = sprite1;
            yield return new WaitForSeconds(.1f);
        }
    }
}
