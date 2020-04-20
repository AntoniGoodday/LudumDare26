using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapScript : MonoBehaviour
{
    [SerializeField]
    private GameObject currentLocation;
    private GoInsideScript goInsideScript;

    [SerializeField]
    RandomEncounters randomEncounters;

    AudioSource footsteps;

    public GameObject CurrentLocation { get => currentLocation; set => currentLocation = value; }
    public bool isMoving = false;

    private void Awake()
    {
        transform.position = currentLocation.transform.position;
        footsteps = GetComponent<AudioSource>();
        //resettin the random encounters on new game
        for (int j = 0; j < randomEncounters.usedEncounters.Count; j++)
        {
            randomEncounters.usedEncounters[j] = false;
        }

    }

    private void Start()
    {
        currentLocation.GetComponent<LocationMarkerScript>().ToggleMarkers();
        goInsideScript = GameObject.Find("GoInside").GetComponent<GoInsideScript>();
        goInsideScript.LocationSceneNumber = currentLocation.GetComponent<LocationMarkerScript>().SceneNumber;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void GoToDestination(GameObject destination)
    {
        if(currentLocation.GetComponent<LocationMarkerScript>().Connections.Contains(destination))
        {
            if (isMoving == false)
            {
                StartCoroutine(LerpToDestination(destination));
            }
        }
    }
    IEnumerator LerpToDestination(GameObject destination)
    {
        footsteps.Play();
        isMoving = true;
        goInsideScript.DisableVisibility();
        float _elapsedTime = 0;

        float _distance = Vector3.Distance(currentLocation.transform.position, destination.transform.position);

        while(_elapsedTime < _distance)
        {
            transform.position = Vector2.Lerp(currentLocation.transform.position, destination.transform.position, _elapsedTime / _distance);
            _elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.position = destination.transform.position;
        currentLocation = destination;
        goInsideScript.LocationSceneNumber = currentLocation.GetComponent<LocationMarkerScript>().SceneNumber;
        currentLocation.GetComponent<LocationMarkerScript>().markerActive = true;
        isMoving = false;
        footsteps.Stop();
    }
}
