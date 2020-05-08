using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSound : MonoBehaviour
{
    public AudioSource audio;

    public void TriggerSound()
    {
        audio.Play();
    }
}
