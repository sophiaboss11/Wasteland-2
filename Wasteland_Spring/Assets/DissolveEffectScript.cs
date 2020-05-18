using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class DissolveEffectScript : MonoBehaviour
{

    public UnityEvent triggerEvent;

    [Range(0.0f, 9.9f)]
    public float RandomTriggerDelay = 0.5f;
    public int TriggerEventThreshold = 1; // How many times a trigger needs to be collided with to trigger event
    public bool TriggerOnlyOnce = true;
    private int timesTriggered = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(triggerEvent == null)
        {
            Debug.LogError("Error: triggerEvent is null!");
        }
    }

    bool IsTriggered()
    {
        return (timesTriggered >= TriggerEventThreshold);
    }

    public void OnTriggerEnter(Collider collider)
    {

        // Check if the progress counter is full
        if(TriggerOnlyOnce && IsTriggered())
        {
            return;
        }

        // Increment progress counter
        timesTriggered += 1;

        // Check again after incrementing
        if(IsTriggered())
        {
            // Trigger event
            if(triggerEvent != null)
            {
                StartCoroutine("TriggerEventWithRandomOffset");
            }
        }
    }

    public void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Reset()
    {
        timesTriggered = 0;
        StopAllCoroutines();
    }

    IEnumerator TriggerEventWithRandomOffset()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, RandomTriggerDelay));
        triggerEvent.Invoke();    
    }
}
