using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimeline : MonoBehaviour
{

    public KeyCode playKeyCode;
    public KeyCode pauseKeyCode;
    public KeyCode stopKeyCode;
    public KeyCode resumeKeyCode;
    PlayableDirector director;
	public bool warpTime = false;
	public int timeScale;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(playKeyCode))
        {
			if (warpTime)
			{
				PlayAtSpeed(director, timeScale);
				//director.SetSpeed(timeScale);
			}
			else{
				
            	director.Play();
			}
        }
        if (Input.GetKeyDown(pauseKeyCode))
        {
            director.Pause();
        }
        if (Input.GetKeyDown(stopKeyCode))
        {
            director.Stop();
        }
        if (Input.GetKeyDown(resumeKeyCode))
        {
            director.Resume();
        }
    }


	void PlayAtSpeed(PlayableDirector playableDirector, float speed)
	{
		playableDirector.RebuildGraph(); // the graph must be created before getting the playable graph
		playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(speed);
		playableDirector.Play();
	}
}
