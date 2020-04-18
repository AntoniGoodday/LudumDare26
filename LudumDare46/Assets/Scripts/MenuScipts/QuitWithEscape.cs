using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitWithEscape : MonoBehaviour
{
    public static QuitWithEscape persistentQuit;
    // Update is called once per frame
    private void Awake()
    {
        if(!persistentQuit)
        {
            persistentQuit = this;
            DontDestroyOnLoad(gameObject);
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
