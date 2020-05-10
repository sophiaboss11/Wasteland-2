using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHoseState : MonoBehaviour
{
    public bool active = false;
    GameObject player;
    HoseState hoseState;

    // Start is called before the first frame update
    void Start()
    {
        var Players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject playerObject in Players)
        {
            hoseState = playerObject.GetComponentInChildren<HoseState>();
            if(hoseState != null)
            {
                hoseState.SetHoseActive(active);
            }
            
        }
//        player = GameObject.FindGameObjectWithTag("Player");
//        hoseState = player.GetComponent<HoseState>();
//        hoseState.SetHoseActive(active);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
