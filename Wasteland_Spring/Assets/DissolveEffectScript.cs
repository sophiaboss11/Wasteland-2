using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class DissolveEffectScript : MonoBehaviour
{

    public UnityEvent triggerEvent;
    public ProgressManager progressManager;

    [Range(0.0f, 9.9f)]
    public float RandomTriggerDelay = 0.0f;
    public int TriggerEventThreshold = 1; // How many times a trigger needs to be collided with to trigger event
    public bool TriggerOnlyOnce = true; // If true, triggers the event only once
    public int TimesTriggeredToTriggerProgressManager = 60; // Triggers needed to trigger progress manager

    private int timesTriggered = 0; // Number of times triggered
    private bool progressManagerTriggered = false; // Whether or not the progress manager has been triggered

    // Start is called before the first frame update
    void Start()
    {
        if(triggerEvent == null)
        {
            Debug.LogError("Error: triggerEvent is null!");
        }
        if(progressManager == null)
        {
            Debug.LogError("Error: progressManager is null!");
        }
    }

    bool IsTriggered()
    {
        return (timesTriggered >= TriggerEventThreshold);
    }

    // Only triggers the progress manager once
    private void TriggerProgressManager()
    {
        if(timesTriggered >= TimesTriggeredToTriggerProgressManager &&
            progressManager != null && !progressManagerTriggered)
        {
            Debug.Log("Triggering progress manager");
            progressManager.Triggered();
            progressManagerTriggered = true;
        }
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

        // This section is called each time
        // Check again after incrementing
        if(IsTriggered())
        {
            TriggerProgressManager();
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
        progressManagerTriggered = false;
        StopAllCoroutines();
    }

    IEnumerator TriggerEventWithRandomOffset()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.0f, RandomTriggerDelay));
        triggerEvent.Invoke();    
    }
}
