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
    GameObject goInside;

    public List<GameObject> Connections { get => connections; set => connections = value; }
    public int SceneNumber { get => sceneNumber; set => sceneNumber = value; }

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
        playerMapScript.GoToDestination(gameObject);
    }

    public void ToggleMarkers()
    {
        foreach (LineRenderer l in lines)
        {
            l.enabled = !l.enabled;
        }

    }
}
