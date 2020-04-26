using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ccLoadLevel : MonoBehaviour
{


    public string levelName;
    public KeyCode keyCode;

    public bool autoLoad;
    public float autoLoadTime;

    // Start is called before the first frame update
    void Start()
    {
        if (autoLoad)
        {
            StartCoroutine("AutoLoadNextScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (!autoLoad)
        //{
        if (Input.GetKeyDown(keyCode))
        {
            SceneManager.LoadSceneAsync(levelName);
        }
        //}


    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(levelName);
    }

    IEnumerator AutoLoadNextScene()
    {
        yield return new WaitForSeconds(autoLoadTime);


        LoadNextLevel();

    }


}
