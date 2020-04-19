using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioSource windAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //dont play wind ambience when inside a scenario event
        if (GameObject.Find("GoInside").GetComponent<GoInsideScript>().insideScenario == true && windAudio.isPlaying)
        {
            windAudio.Pause();
        }
        if (GameObject.Find("GoInside").GetComponent<GoInsideScript>().insideScenario == false && !windAudio.isPlaying)
        {
            windAudio.UnPause();
        }
    }
}
