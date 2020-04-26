using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class TriggerPopup : MonoBehaviour
{

    public ProgressManager progressManager;
    public KeyCode keyCode;
    //public string colliderTag;
    bool isTriggered = false;
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            TriggerTheAnimation();
        }   
    }


    public void OnTriggerEnter(Collider collider)
    {
        //if (collider.tag(colliderTag))
        //{

        Debug.Log("OnTriggerEnter Called!");

        if(!isTriggered)
        {
            TriggerTheAnimation();
            isTriggered = true;

        }
        //}
    }

    public void TriggerTheAnimation()
    {

        if (!isTriggered)
        {
            //progressManager.Triggered();
            isTriggered = true;
            Destroy(this.gameObject);
            
        }
    }

}
