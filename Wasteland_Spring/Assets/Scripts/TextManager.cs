using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{

    public TextMeshPro forestTextObject;
    public TextMeshPro mountainTextObject;
    public TextMeshPro glacierTextObject;
    public TextMeshPro waterfallTextObject;

    public Text_SO _text;

    // Start is called before the first frame update
    void Start()
    {
        forestTextObject.text = _text.forestText;
        mountainTextObject.text = _text.mountainText;
        glacierTextObject.text = _text.glacierText;
        waterfallTextObject.text = _text.waterfallText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
