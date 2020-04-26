using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimator : MonoBehaviour
{

    public string AnimTrigger;
    public KeyCode keyCode;
    public ProgressManager progressManager;

    bool isTriggered = false;

    // Collider collider;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            TriggerTheAnimator();
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (!isTriggered)
        {
            TriggerTheAnimator();
            isTriggered = true;
        }
        
    }

    public void TriggerTheAnimator()
    {
        progressManager.Triggered();
        animator.SetTrigger(AnimTrigger);
    }
}
