using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ccGameManager : MonoBehaviour
{

    public string sceneName;
    public ProgressManager[] progressManagers;
    public float waitToLoadNextLevel = 3.0f;

    int numOfManagersLeft = 0;


    GameObject hose;

    // Start is called before the first frame update
    void Start()
    {
        hose = GameObject.FindWithTag("Hose");
        hose.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ProgressManagerFinished()
    {
        numOfManagersLeft++;
        Debug.Log("Num of managers left is: " + numOfManagersLeft);
        if(numOfManagersLeft >= progressManagers.Length)
        {
            StartCoroutine("LevelFinished");
            
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LevelFinished()
    {
        yield return new WaitForSeconds(waitToLoadNextLevel);
        hose.SetActive(false);
        LoadNextScene();
    }
}
