using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    [Range(0.1f, 2f)]
    public float frequency;
    private float lastEvent;
    private TMP_Text text;
    private bool active = true;
    public string startingText;
    private string hiddenText = "";

    void Start()
    {
        text = GetComponent<TMP_Text>();
        startingText = text.text;
        lastEvent = Time.time;
    }

    void Update()
    {
        if(Time.time - lastEvent > frequency){
            active = !active;
            lastEvent = Time.time;
        }

        if(active){
            text.text = startingText;
        }else{
            text.text = hiddenText;
        }
    }
}
