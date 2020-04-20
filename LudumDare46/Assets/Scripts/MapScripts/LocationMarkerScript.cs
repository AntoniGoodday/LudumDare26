using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationMarkerScript : MonoBehaviour
{
    [SerializeField]
    int sceneNumber;
    [SerializeField]
    List<GameObject> connections;
    [SerializeField]
    Material lineMaterial;

    PlayerMapScript playerMapScript;

    [SerializeField]
    RandomEncounters randomEncounters;
    bool setEncounter = false;

    [SerializeField]
    GameObject goInside;

    public List<GameObject> Connections { get => connections; set => connections = value; }
    public int SceneNumber { get => sceneNumber; set => sceneNumber = value; }

    public string destinationScene;
    //the destination title is the words on the button you click to enter the scene
    public string destinationTitle;
    public bool markerActive = false;
    public bool markerClear = false;
    public int markerValue;
    public GameObject playerObject;

    [SerializeField]
    List<LineRenderer> lines;

    private void Awake()
    {
        playerMapScript = GameObject.Find("PlayerMarker").GetComponent<PlayerMapScript>();
        goInside = GameObject.Find("GoInside");


        int i = 0;
        foreach (GameObject g in connections)
        {
            GameObject _lineObject = new GameObject();
            _lineObject.name = "line_" + i;
            _lineObject.transform.parent = gameObject.transform;

            LineRenderer _lineRend = _lineObject.AddComponent<LineRenderer>();
            _lineRend.SetPosition(0, transform.position);
            _lineRend.SetPosition(1, g.transform.position);
            _lineRend.startWidth = 0.5f;
            _lineRend.endWidth = 0.5f;
            _lineRend.material = lineMaterial;
            _lineRend.textureMode = LineTextureMode.Tile;
            lines.Add(_lineRend);
            i++;       
        }
        ToggleMarkers();
    }

    private void OnMouseDown()
    {
        //make sure marker hasnt already been visited

        //need to check if the clicked marker is actually connected to the current marker
        if (playerObject.GetComponent<PlayerMapScript>().CurrentLocation.GetComponent<LocationMarkerScript>().connections.Contains(this.gameObject))
        {
            //now check if current marker is actually cleared so you dont just skip all the events
            if (playerObject.GetComponent<PlayerMapScript>().CurrentLocation.GetComponent<LocationMarkerScript>().markerClear == true)
            {
                playerMapScript.GoToDestination(gameObject);
                SetRandomEncounter();
                // markerActive = true;
                //when a map market is clicked, tell the GoInside script what scene is assigned to this marker
                //so that when we click "go inside" it knows wha scene to go to
                goInside.GetComponent<GoInsideScript>().destinationScene = destinationScene;
            }
        }
    }

    void Update()
    {
        //if no destination on this marker, it means it has been cleared, set to green
        if (destinationScene == "" && markerClear == false)
        {
            DisableMarkers();
            this.GetComponent<SpriteRenderer>().color = Color.green;
            markerClear = true;
            for (int i = 0; i < lines.Count; i++)
            {
                if (i > 0)
                {
                    lines[i].enabled = true;
                }
            }
        }
    }
    //choose random encounter, then remove from pool
    //Edit here if you'd like some prerequesites for particular random encounters
    void SetRandomEncounter()
    {
        if(randomEncounters != null && setEncounter == false)
        {
            List<int> _remainingEncounters = new List<int>();
            for(int i = 0; i < randomEncounters.usedEncounters.Count; i++)
            {
                if (randomEncounters.usedEncounters[i] == false)
                {
                    _remainingEncounters.Add(randomEncounters.encounterBuildIDs[i]);
                }
            }
            int _randomInt = (int)Random.Range(0, _remainingEncounters.Count);

            sceneNumber = _remainingEncounters[_randomInt];

            for (int i = 0; i < randomEncounters.usedEncounters.Count; i++)
            {
                if(randomEncounters.encounterBuildIDs[i] == sceneNumber)
                {
                    randomEncounters.usedEncounters[i] = true;
                }
            }
            setEncounter = true;
        }
    }

    public void ToggleMarkers()
    {
        foreach (LineRenderer l in lines)
        {
            l.enabled = !l.enabled; 
        }
        //if marker isnt clear, dont show lines in front of current marker as it hasnt been cleared yet
        if (playerObject.GetComponent<PlayerMapScript>().CurrentLocation.GetComponent<LocationMarkerScript>().markerClear == false)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if (i >= 0)
                {
                    lines[i].enabled = false;
                }
            }
        }
    }
    public void DisableMarkers()
    {
        foreach (LineRenderer l in lines)
        {
            l.enabled = false;
        }
    }
}
