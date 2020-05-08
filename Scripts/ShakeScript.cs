using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    public Transform transform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.05f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 0.25f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    public bool canShake = true;

    void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (canShake)
        {
            if (shakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }
        
    }

    public void TriggerShake()
    {
        shakeDuration = 0.25f;
    }

    public void TriggerShake(int a)
    {
        shakeDuration = 0.25f;
        StartCoroutine(dampingTimer());
    }

    private IEnumerator dampingTimer()
    {
        yield return new WaitForSeconds(1.5f);
        canShake = false;
    }
}
