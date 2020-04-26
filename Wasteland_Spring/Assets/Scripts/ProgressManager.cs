using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{

    public KeyCode resetKeyCode;

    public TriggerAnimation[] triggers;
    public TriggerAnimator[] animatorTriggers;

    public bool usingAnimators = false;

    [RangeAttribute( 0, 100)]
    public int FinishAfterPercentDone;

    float numToFinish;
    int numDone;
    bool isFinished = false;

    GameObject gm;
    ccGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (usingAnimators)
        {
            numToFinish = (animatorTriggers.Length * FinishAfterPercentDone) / 100;
        }
        numToFinish = (triggers.Length * FinishAfterPercentDone) / 100;
        Debug.Log("numToFinish is: "+numToFinish);

        gm = GameObject.FindGameObjectWithTag("GameManager");//.GetComponent<ccGameManager>()

        gameManager = gm.GetComponent<ccGameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(resetKeyCode))
        {
            Reset();
        }
    }


    public void Reset()
    {
        foreach (TriggerAnimation t in triggers)
        {
            t.Reset();
        }
        numDone = 0;
        isFinished = false;
    }

    public void Triggered()
    {
        numDone++;

        if(!isFinished)
        {
            if (numDone > numToFinish)
            {
                isFinished = true;
                FinishThis();
            }
        }
        

        Debug.Log("numDone is: "+numDone);
    }

    public void FinishThis()
    {

        Debug.Log("This Section Has Been Finished!");

        if (usingAnimators)
        {
            foreach (TriggerAnimator t in animatorTriggers)
            {
                t.TriggerTheAnimator();
            }

        }
        else
        {
            foreach (TriggerAnimation t in triggers)
            {
                t.TriggerTheAnimation();
            }

        }

        //Call function to show text box here!
        gameManager.ProgressManagerFinished();
    }


}
