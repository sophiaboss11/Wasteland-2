using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float lifespan = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifespan);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
