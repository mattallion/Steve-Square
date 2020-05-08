using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart1Script : MonoBehaviour
{
    void Update()
    {
        GameObject player = GameObject.Find("Steve_Square");
        HealthScript steve = player.GetComponent<HealthScript>();

        if (steve.hp == 1)
        {
            Destroy(gameObject);
        }
    }
}
