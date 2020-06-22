using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationInstantiator : MonoBehaviour
{
    
    public bool DebugLog = false;           // Display debug messages in log
    public bool Loop = false;               // Loop animation when finished
    public bool PlayOnStart = false;        // Begin playing as soon as play mode is entered
    public bool UseAnimDelay = false;       // Random delay before starting next animation state
    public int RandomSeed = 0;              // Seed to feed random number generator
    public float MinAnimDelay = 0.0f;       // Minimum time before starting next animation state if playing
    public float MaxAnimDelay = 0.0f;       // Max time before starting next animation state if playing
    public string NextStateTriggerName = "GotoNextState";   // Name of trigger parameter in animator controller 

    private List<Animator> ChildAnimators;
    private List<AnimatorController> ChildAnimatorControllers;

    // Start is called before the first frame update
    void Start()
    {
        SetChildAnimators();
    }

    private void SetChildAnimators()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();
        AnimatorController[] animControllers = GetComponentsInChildren<AnimatorController>();

        if (animators.Length <= 0)
        {
            Debug.LogWarning("No animators found in children.");
        }
        if (animControllers.Length <= 0)
        {
            Debug.LogWarning("No animator controllers found in children.");
        }

        ChildAnimators.AddRange(animators);
        ChildAnimatorControllers.AddRange(animControllers);

        if (ChildAnimators.Count <= 0)
        {
            Debug.LogWarning("No animators added to childAnimatorControllers!");
        }
        if (ChildAnimatorControllers.Count <= 0)
        {
            Debug.LogWarning("No animator controllers added to childAnimatorControllers!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Delegates for UnityEvent triggers
    public void PlayAnimation()
    {
    }

    public void PlayNextAnimationState()
    {
        ChildAnimators.ForEach(delegate (Animator animator)
        {
            if(UseAnimDelay)
            {
                AnimatorPlayNextStateAfterDelay(animator);
            }
            else
            {
                AnimatorPlayNextStateImmediate(animator);
            }
        });
    }

    private void AnimatorPlayNextStateImmediate(Animator animator)
    {
        animator.SetTrigger(NextStateTriggerName);
    }


    private void AnimatorPlayNextStateAfterDelay(Animator animator)
    {
        StartCoroutine("CoroutineDelayedPlayNextState", animator);
    }
    
    private float AnimationRandomDelay()
    {
        int baseTime = (int) (Time.realtimeSinceStartup * 100.0);
        UnityEngine.Random.InitState(baseTime + RandomSeed);
        return UnityEngine.Random.Range(MinAnimDelay, MaxAnimDelay);
    }

    IEnumerator CoroutineDelayedPlayNextState(Animator animator)
    {
        yield return new WaitForSeconds(AnimationRandomDelay());
        AnimatorPlayNextStateImmediate(animator);
    }

    public void ResetAnimators()
    {

    }
}
