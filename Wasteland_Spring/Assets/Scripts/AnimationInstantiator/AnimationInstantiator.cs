using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationInstantiator : MonoBehaviour
{
    // Animation Instantiator
    // This script looks for Animators in each child object in the hierarchy under it
    // and sets triggers animation parameter triggers based on the animation it's playing.
    // Look at the example scene in Scenes/AnimationInstantiator_Test for an example of usage.
    // Jason Wang - 6/22/2020

    #region === PUBLIC VARIABLES ===
    // PUBLIC VARIABLES

    // Animatable properties
    [Header("Animatable Properties")]
    [Tooltip("If this is true during play mode, Update() will transition to the next state and reset this property")]
    public bool PlayNextState = false;       

    [Tooltip("If this is true during play mode, Update() will reset all animators")]
    public bool Reset = false;       

    [Header("Configuration")]
    [Tooltip("Display debug messages in log")]
    public bool DebugLog = false;           

    [Tooltip("Random delay before starting next animation state")]
    public bool UseAnimDelay = false;        

    [Tooltip("// Minimum time before starting next animation state if playing")]
    public float MinAnimDelay = 0.0f;       

    [Tooltip("Max time before starting next animation state if playing")]
    public float MaxAnimDelay = 0.0f;        

    [Tooltip("String name of next state trigger in animator controller ")]
    public string NextStateTriggerName = "GotoNextState";    

    [Tooltip("String name of reset trigger in animator controller ")]
    public string ResetTriggerName = "Reset";

//      Not needed: Use animation controller state for loops
//    [Tooltip("Loop animation when finished")]
//    public bool Loop = false;               

//      Not needed: Use the animation controller to 
//    [Tooltip("Begin playing as soon as play mode is entered")]
//    public bool PlayOnStart = false;        // Begin playing as soon as play mode is entered

//      TODO: This is a problem since this is called once
//    [Tooltip("Seed to feed random number generator")]
//    public int RandomSeed = 0;              

    #endregion

    #region === PRIVATE VARIABLES ===
    private List<Animator> ChildAnimators;
    #endregion

    #region === UNITY METHODS ===

    void Start()
    {
        ChildAnimators = new List<Animator>();
        PlayNextState = false;
        Reset = false;
        SetChildAnimators();
    }

    private void OnDisable()
    {
        ChildAnimators.Clear();
    }

    private void Update()
    {
        DoStateTransitionsIfPlaying();
    }

    #endregion

    #region === DELEGATES ===

    // Delegates for UnityEvent triggers
    public void PlayNextAnimationState()
    {
        if(DebugLog)
        {
            Debug.Log("Playing next animation state!");
        }

        foreach(Animator animator in ChildAnimators)
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

    public void ResetAllAnimators()
    {
        if(DebugLog)
        {
            Debug.Log("Resetting all child animators!");
        }

        foreach(Animator animator in ChildAnimators)
        {
            if(animator != null)
            {
                AnimatorResetImmediate(animator);            
            }
        }
    }

    private void AnimatorPlayNextStateImmediate(Animator animator)
    {
        AnimatorSetTriggerParameter(animator, NextStateTriggerName);
    }

    private void AnimatorResetImmediate(Animator animator)
    {
        AnimatorSetTriggerParameter(animator, ResetTriggerName);
        AnimatorResetTriggerParameter(animator, NextStateTriggerName);
    }

    private void AnimatorPlayNextStateAfterDelay(Animator animator)
    {
        StartCoroutine("CoroutineDelayedPlayNextState", animator);
    }

    #endregion

    #region === PRIVATE METHODS ===

    private void SetChildAnimators()
    {
        Animator[] animators = this.gameObject.GetComponentsInChildren<Animator>();
        if (animators.Length <= 0)
        {
            Debug.LogWarning("No animators found in children.");
        }

        ChildAnimators.AddRange(animators);

        if (ChildAnimators.Count <= 0)
        {
            Debug.LogWarning("No animators added to childAnimatorControllers!");
        }
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

    // Returns random delay in range
    private float AnimationRandomDelay()
    {
//        int baseTime = (int) (Time.realtimeSinceStartup * 100.0);
//        UnityEngine.Random.InitState(baseTime + RandomSeed);
        return UnityEngine.Random.Range(MinAnimDelay, MaxAnimDelay);
    }

    private void AnimatorSetTriggerParameter(Animator animator, string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
    private void AnimatorResetTriggerParameter(Animator animator, string triggerName)
    {
        animator.ResetTrigger(triggerName);
    }

    #endregion

    #region === COROUTINES ===
    IEnumerator CoroutineDelayedPlayNextState(Animator animator)
    {
        yield return new WaitForSeconds(AnimationRandomDelay());
        AnimatorPlayNextStateImmediate(animator);
    }
    #endregion
}
