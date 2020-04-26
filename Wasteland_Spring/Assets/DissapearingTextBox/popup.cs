using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : RaySelect
{

    MeshRenderer meshRenderer;
    protected override void OnRaycastEnter( GameObject target ) {
        
        if( target.tag == "TextBoxTrigger") {
            meshRenderer = target.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
            Debug.Log("I see it");
        }
        

    }

    /** When no longer seen, shrink to 0 */
    protected override void OnRaycastLeave( GameObject target ) {

        if( target.tag == "TextBoxTrigger") {
            //check for mesh renderer
            meshRenderer = target.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            Debug.Log("Don't see it");
        }

        Debug.Log("Don't see it");
        
    }

    protected override void OnRaycast( GameObject target ) {}
}
