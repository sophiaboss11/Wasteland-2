using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseState : MonoBehaviour
{
    //public bool isActive = false;
    public GameObject hoseGO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHoseActive(bool active)
    {
        hoseGO.SetActive(active);        
    }
}
