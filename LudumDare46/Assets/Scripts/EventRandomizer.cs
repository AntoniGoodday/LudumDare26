using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRandomizer : MonoBehaviour
{

    //the markers that we want to randomize
    public GameObject[] mapMarkers;
    //list of scenes that will be mixed across the markers
    public string[] sceneNames;
    public string tempName;
    public int randomSceneNumber1;
    public int randomSceneNumber2;

    public Sprite[] markerIcons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            Randomize();
            Debug.Log("Shuffling");
        }

        for (int i = 0; i < 13; i++)
        {
            mapMarkers[i].GetComponent<LocationMarkerScript>().destinationScene = sceneNames[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Randomize()
    {
        //this whole thing swaps 2 scenes
        randomSceneNumber1 = Random.Range(0, 13);
        randomSceneNumber2 = Random.Range(0, 13);
        tempName = sceneNames[randomSceneNumber1];

        sceneNames[randomSceneNumber1] = sceneNames[randomSceneNumber2];
        sceneNames[randomSceneNumber2] = tempName;
    }
}
