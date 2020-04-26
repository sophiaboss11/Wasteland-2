using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  Virtual methods to select items with a ray.
 *  Used for textbox popup and backup for garden hose
 *  Used code from: https://www.youtube.com/watch?v=2kV-6pZJwrs
 */
public class RaySelect : MonoBehaviour
{
    public float selectionDistance = 1f;
    //public int layerMask = ~( 1<<2 ); // Create a layer mask to avoid layer 2 (avoid raycast)

    private GameObject currentTarget;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        
        // Send out a ray and store data to hit
        if ( Physics.Raycast( transform.position, transform.forward, out hit, selectionDistance)){
            
            // If needed, call RaycastLeave for old object, and enter for new
            if ( currentTarget == null ){
                currentTarget = hit.transform.gameObject;
                OnRaycastEnter(currentTarget);
            } else if( currentTarget !=  hit.transform.gameObject) {
                OnRaycastLeave(currentTarget);
                currentTarget = hit.transform.gameObject;
                OnRaycastEnter(currentTarget);
            }

            // Call ongoing raycast function for object
            OnRaycast(hit.transform.gameObject);

        } else if (currentTarget != null ) {

            // If it leaves to nothing, call leave and null out
            OnRaycastLeave(currentTarget);
            currentTarget = null;
        }

       // Debug.Log( "" + this.gameObject.name + " sees " + currentTarget.name);
        
    }

    /** Trigger on enter of raycast */
    protected virtual void OnRaycastEnter(GameObject target){}
    /** Trigger on leaving raycast */
    protected virtual void OnRaycastLeave(GameObject target){}

    /** Trigger while raycasted */
    protected virtual void OnRaycast(GameObject target){}

}
