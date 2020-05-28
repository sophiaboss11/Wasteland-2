using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_delay : MonoBehaviour
{
    public int seconds;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //obj.GetComponent<GameObject>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(delayAnim());
    }

    IEnumerator delayAnim()
    {
        yield return new WaitForSeconds(seconds);
        anim.Play("Flowers_Grow");

        //GetComponent.<Animation>().Play("Flowers_Grow");
       // Debug.Log("BIN Released");
    }

}
