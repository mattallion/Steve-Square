using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    Image timeBar;
    public float maxTime;
    public float timeLeft;

    void Start()
    {
        GameObject clock = GameObject.Find("Timer");
        TimerScript t = clock.GetComponent<TimerScript>();
        setMaxTime(t);
        timeBar = GetComponent<Image>();
    }

    public float setMaxTime(TimerScript t)
    {
        maxTime = t.limit;
        return maxTime;
    }

    void Update()
    {
        GameObject clock = GameObject.Find("Timer");
        TimerScript t = clock.GetComponent<TimerScript>();
        timeLeft = maxTime - t.timer;

        if (timeLeft > 0)
        {
            timeBar.fillAmount = (timeLeft / maxTime);
        }

        GameObject player = GameObject.Find("Steve_Square");
        ScoreScript steve = player.GetComponent<ScoreScript>();
        if (steve.score == 3)
        {
            gameObject.GetComponent<Image>().enabled = false;
        }

        HealthScript health = player.GetComponent<HealthScript>();
        if (health.hp <= 1)
        {
            gameObject.GetComponent<Image>().enabled = false;
        }

        if (t.timer >= t.limit)
        {
            gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
