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
    public Sprite markerSprite;
    public SpriteRenderer markerSpriteRender;
    public GameObject playerObject;
    public GameObject randomizerHandler;

    [SerializeField]
    List<LineRenderer> lines;

    void Start()
    {
        markerSpriteRender = GetComponent<SpriteRenderer>();
        if (markerSprite != null)
        {
            markerSpriteRender.sprite = markerSprite;
        }
        markerSpriteRender.color = Color.white;

    }


    void LateUpdate()
    { 
        //ok this is very very messy
        //basically its going to check what scene name was given to it by the randomizer and change its sprite, title, etc appropriately
        if (destinationScene == "BanditTrio")
        {
            destinationTitle = "A Grand Company";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[0];
        }
        if (destinationScene == "WaterPuddle")
        {
            destinationTitle = "A Puddle Full Of Promise";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[1];
        }
        if (destinationScene == "Arcadia")
        {
            destinationTitle = "Et In 'Arcadia' Ego";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[2];
        }
        if (destinationScene == "Fox")
        {
            destinationTitle = "FriendShip";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[3];
        }
        if (destinationScene == "SnakeSalesman")
        {
            destinationTitle = "A Snake Oil Salesman";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[4];
        }
        if (destinationScene == "WiseDeceased")
        {
            destinationTitle = "The Wise Deceased";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[6];
        }
        if (destinationScene == "Birds")
        {
            destinationTitle = "It's Good Looking And Good Tasting";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[7];
        }
        if (destinationScene == "PeaceWalker")
        {
            destinationTitle = "Peace Walker";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[8];
        }
        if (destinationScene == "Ant")
        {
            destinationTitle = "A Little Company";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[9];
        }
        if (destinationScene == "Feast")
        {
            destinationTitle = "A Feast!";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[11];
        }
        if (destinationScene == "WastelandFigure")
        {
            destinationTitle = "The WasteLand's Figure";
            markerSpriteRender.sprite = randomizerHandler.GetComponent<EventRandomizer>().markerIcons[5];
        }
    }

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


        //if someone causes a future scene to change, such as the fake oasis event, check it here
        if (GameObject.Find("CharacterIcons").GetComponent<HealthStates>().hasFakeOasis == true && destinationScene == "Birds")
        {
            destinationScene = "ThisAgain";
            destinationTitle = "This, Again!?";
        }

        if (GameObject.Find("CharacterIcons").GetComponent<HealthStates>().hasSkeleton == true && destinationScene == "WiseDeceased")
        {
            destinationScene = "GraveSituation";
            destinationTitle = "A Grave Situation";
        }

        if (GameObject.Find("CharacterIcons").GetComponent<HealthStates>().hasFox == true && destinationScene == "Fox")
        {
            destinationScene = "Treachery";
            destinationTitle = "Treachery!";
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
