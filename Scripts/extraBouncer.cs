using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraBouncer : MonoBehaviour
{
    
    void Start()
    {
        GameObject bouncer4 = GameObject.Find("Bouncer_4");
        bouncer4.GetComponent<MoveScript>().enabled = false;
        bouncer4.GetComponent<SpriteRenderer>().enabled = false;
        bouncer4.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void activate()
    {
        GameObject bouncer4 = GameObject.Find("Bouncer_4");
        bouncer4.GetComponent<MoveScript>().enabled = true;
        bouncer4.GetComponent<SpriteRenderer>().enabled = true;
        bouncer4.GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(timeActive());
    }

    private IEnumerator timeActive()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
