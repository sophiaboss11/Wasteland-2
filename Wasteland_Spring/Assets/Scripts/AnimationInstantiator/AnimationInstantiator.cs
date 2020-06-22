using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationInstantiator : MonoBehaviour
{
    // Animatable properties
    [Header("Animatable Properties")]
    [Tooltip("If this is true during play mode, Update() will transition to the next state and reset this property")]
    public bool PlayNextState = false;       

    [Tooltip("If this is true during play mode, Update() will reset all animators")]
    public bool Reset = false;       

    [Header("Configuration")]
    [Tooltip("Display debug messages in log")]
    public bool DebugLog = false;           

    [Tooltip("Loop animation when finished")]
    public bool Loop = false;               

    [Tooltip("Begin playing as soon as play mode is entered")]
    public bool PlayOnStart = false;        // Begin playing as soon as play mode is entered

    [Tooltip("Random delay before starting next animation state")]
    public bool UseAnimDelay = false;        

    [Tooltip("Seed to feed random number generator")]
    public int RandomSeed = 0;              

    [Tooltip("// Minimum time before starting next animation state if playing")]
    public float MinAnimDelay = 0.0f;       

    [Tooltip("Max time before starting next animation state if playing")]
    public float MaxAnimDelay = 0.0f;        

    [Tooltip("String name of next state trigger in animator controller ")]
    public string NextStateTriggerName = "GotoNextState";    

    [Tooltip("String name of reset trigger in animator controller ")]
    public string ResetTriggerName = "Reset";

    private List<Animator> ChildAnimators;

    // Start is called before the first frame update
    void Start()
    {
        ChildAnimators = new List<Animator>();
        PlayNextState = PlayOnStart;
        SetChildAnimators();
    }

    private void OnDisable()
    {
        ChildAnimators.Clear();
    }

    private void SetChildAnimators()
    {
        Animator[] animators = this.gameObject.GetComponentsInChildren<Animator>();
        if (animators.Length <= 0)
        {
            Debug.LogWarning("No animators found in children.");
        }
        ChildAnimators.AddRange(animators);
//        foreach(var animator in animators)
//        {
//            ChildAnimators.Add(animator);
//        }
        if (ChildAnimators.Count <= 0)
        {
            Debug.LogWarning("No animators added to childAnimatorControllers!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        DoStateTransitionsIfPlaying();
    }

    // Handles state transitions for animations
    private void DoStateTransitionsIfPlaying()
    {
        // Only run while in a Player or Play mode
        if (!Application.isPlaying)
        {
            return;
        }

        // Reset and return
        if(Reset)
        {
            Reset = false;
            ResetAllAnimators();
            return;
        }

        // State transitions
        if(PlayNextState)
        {
            PlayNextState = false;
            PlayNextAnimationState();
        }
    }

    // Delegates for UnityEvent triggers
    public void PlayNextAnimationState()
    {
        foreach(Animator animator in ChildAnimators)
//        ChildAnimators.ForEach(delegate (Animator animator)
        {
            if(animator != null)
            {
                if(UseAnimDelay)
                {
                    AnimatorPlayNextStateAfterDelay(animator);
                }
                else
                {
                    AnimatorPlayNextStateImmediate(animator);
                }
            }
        }
    }
    private void AnimatorSetTriggerParameter(Animator animator, string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    private void AnimatorPlayNextStateImmediate(Animator animator)
    {
        AnimatorSetTriggerParameter(animator, NextStateTriggerName);
    }
    private void AnimatorResetImmediate(Animator animator)
    {
        AnimatorSetTriggerParameter(animator, ResetTriggerName);
    }

    private void AnimatorPlayNextStateAfterDelay(Animator animator)
    {
        StartCoroutine("CoroutineDelayedPlayNextState", animator);
    }
    public void ResetAllAnimators()
    {
        foreach(Animator animator in ChildAnimators)
//        ChildAnimators.ForEach(delegate (Animator animator)
        {
            if(animator != null)
            {
                AnimatorResetImmediate(animator);            
            }
        }
    }
    
    // Returns random delay in range
    private float AnimationRandomDelay()
    {
        int baseTime = (int) (Time.realtimeSinceStartup * 100.0);
//        UnityEngine.Random.InitState(baseTime + RandomSeed);
        return UnityEngine.Random.Range(MinAnimDelay, MaxAnimDelay);
    }

    IEnumerator CoroutineDelayedPlayNextState(Animator animator)
    {
        yield return new WaitForSeconds(AnimationRandomDelay());
        AnimatorPlayNextStateImmediate(animator);
    }
}
