using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapScript : MonoBehaviour
{
    [SerializeField]
    private GameObject currentLocation;
    private GoInsideScript goInsideScript;

    public GameObject CurrentLocation { get => currentLocation; set => currentLocation = value; }
    bool isMoving = false;

    private void Awake()
    {
        transform.position = currentLocation.transform.position;
        
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
        isMoving = true;
        goInsideScript.ToggleVisibility();
        float _elapsedTime = 0;

        while(_elapsedTime < 3)
        {
            transform.position = Vector2.Lerp(currentLocation.transform.position, destination.transform.position, _elapsedTime / 3);
            _elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.position = destination.transform.position;
        currentLocation.GetComponent<LocationMarkerScript>().ToggleMarkers();
        currentLocation = destination;
        currentLocation.GetComponent<LocationMarkerScript>().ToggleMarkers();
        goInsideScript.LocationSceneNumber = currentLocation.GetComponent<LocationMarkerScript>().SceneNumber;
        goInsideScript.ToggleVisibility();
        isMoving = false;
    }
}
