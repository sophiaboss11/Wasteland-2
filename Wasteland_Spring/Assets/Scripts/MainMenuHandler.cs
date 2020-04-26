using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public string startString = "PlayButton";
    public string optionsString = "OptionsButton";
    public string quitString = "QuitButton";
    public SteamVR_LaserPointer menuPointer;
    public string act1_scene;

    // Init on wakeup
    void Awake()
    {
        menuPointer.PointerIn += PointerInside;
        menuPointer.PointerOut += PointerOutside;
        menuPointer.PointerClick += PointerClick;
    }
    
    // Update is called once per frame
    public void PointerClick(object sender, PointerEventArgs e) {
        if( e.target.name == startString ) {
            SceneManager.LoadScene(act1_scene);
        }

        if( e.target.name == quitString ) {
            Application.Quit();
        }
    }

    public void PointerInside( object sender, PointerEventArgs e) {

    }

    public void PointerOutside( object sender, PointerEventArgs e ){

    }
}
