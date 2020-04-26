using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(SphereCollider))]
public class TriggerAnimation : MonoBehaviour
{

    public ProgressManager progressManager;
    public float randomAnimationDelay = 1.0f;
    public KeyCode keyCode;
    //public string colliderTag;

    bool noMeshRenderer = false;
    bool isTriggered = false;
    MeshRenderer meshRenderer;
    Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        animation = gameObject.GetComponent<Animation>();

        //check for mesh renderer
        meshRenderer = GetComponent<MeshRenderer>();
        
        //if no mesh renderer, disable children
        if(meshRenderer == null)
        {
            noMeshRenderer = true;
            foreach (Transform child in transform)
            {
                print("Foreach loop: " + child);
                child.gameObject.SetActive(false);
            }            
        }
        else
        {
            meshRenderer.enabled = false;
        }
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
            progressManager.Triggered();
            isTriggered = true;

            StartCoroutine("TriggerAnimationWithOffset");
        }
    }

    public void Reset()
    {
        if (meshRenderer == null)
        {
            noMeshRenderer = true;
            foreach (Transform child in transform)
            {
                print("Foreach loop: " + child);
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            meshRenderer.enabled = false;
        }
        isTriggered = false;
    }


    IEnumerator TriggerAnimationWithOffset()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, randomAnimationDelay));
        animation.Play();

        if (noMeshRenderer)
        {
            //enable children
            foreach (Transform child in transform)
            {
                print("Foreach loop: " + child);
                child.gameObject.SetActive(true);
            }

        }
        else
        {
            meshRenderer.enabled = true;
        }
    }
}
