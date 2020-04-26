using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melter : MonoBehaviour
{
    private float mSize = 0.0f;
    private bool pressedSpace = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
    }

    void Scale()
    {
        if(mSize >= 100.0f){
            CancelInvoke("Scale");
        }

        GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, mSize++);
    }
    // Update is called once per frame
    void Update()
    {
        if (!pressedSpace)
        {
            if (Input.GetKeyDown(KeyCode.Space)){
                InvokeRepeating("Scale", 0.0f, 0.02f);
                pressedSpace = true;
            }
        }
        
            
    }


}
