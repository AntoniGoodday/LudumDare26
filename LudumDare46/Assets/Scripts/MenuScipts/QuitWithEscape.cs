using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitWithEscape : MonoBehaviour
{
    public static QuitWithEscape persistentQuit;
    [SerializeField]
    Texture2D cursor;
    // Update is called once per frame
    private void Awake()
    {
        if(!persistentQuit)
        {
            persistentQuit = this;
            DontDestroyOnLoad(gameObject);
            Cursor.SetCursor(cursor, new Vector2(0, 100), CursorMode.ForceSoftware);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
