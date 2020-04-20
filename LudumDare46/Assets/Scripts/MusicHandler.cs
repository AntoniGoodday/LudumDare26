using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour
{
    public static MusicHandler musicHandler;

    [SerializeField]
    List<AudioSource> audioSources;
    [SerializeField]
    AudioSource currentBackgroundEffect = null;
    [SerializeField]
    float volumeChangesPerSecond = 10;
    float fadeoutDuration = 0.3f;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!musicHandler)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            musicHandler = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneUnloaded(Scene scene)
    {
        if(scene.buildIndex == 0 || scene.buildIndex == 1 || scene.buildIndex == 2)
        {
            
        }
        else if(scene.buildIndex != 3)
        {
            StartCoroutine(FadeAudioSource(audioSources[1], fadeoutDuration, 1));

            for (int i = 2; i < audioSources.Count; i++)
            {
                StartCoroutine(FadeAudioSource(audioSources[i], fadeoutDuration, 0));
            }
        }

    }



    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        StopAllCoroutines();
        switch(scene.buildIndex)
        {
            case (0):
                {
                    for (int i = 0; i < audioSources.Count-1; i++)
                    {
                        StartCoroutine(FadeAudioSource(audioSources[i], fadeoutDuration, 0));
                    }
                    StartCoroutine(FadeAudioSource(audioSources[4], fadeoutDuration, 1));
                    break;
                }
            case (3):
                {
                    if(audioSources[1].isPlaying == false)
                    {
                        audioSources[1].Play();
                    }

                    for(int i = 2; i < audioSources.Count; i++)
                    {
                        StartCoroutine(FadeAudioSource(audioSources[i], fadeoutDuration, 0));
                    }

                    StartCoroutine(FadeAudioSource(audioSources[1], fadeoutDuration, 1));

                    break;
                }
            case (4):
                {
                    ReplaceWindWith(2);
                    break;
                }
            case (18):
                {
                    ReplaceWindWith(3);
                    break;
                }
            case (21):
                {
                    StartCoroutine(SlowFadeAudioSource(audioSources[4], 3, 0));
                    StartCoroutine(SlowFadeAudioSource(audioSources[0], 3, 1));
                    break;
                }
            case (22):
                {
                    StartCoroutine(FadeAudioSource(audioSources[0], fadeoutDuration, 0));
                    
                    break;
                }
            default:
                {
                    StartCoroutine(FadeAudioSource(audioSources[1], fadeoutDuration, 0));
                    break;
                }
        }


    }

    

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Ending")
        {
            StartCoroutine(FadeAudioSource(audioSources[0], fadeoutDuration, 0));
        }
        if (SceneManager.GetActiveScene().name == "CreditsScene")
        {
            StartCoroutine(FadeAudioSource(audioSources[0], fadeoutDuration, 1));
            StartCoroutine(FadeAudioSource(audioSources[2], fadeoutDuration, 0));
        }
        //dont play wind ambience when inside a scenario event
        /*if (GameObject.Find("GoInside").GetComponent<GoInsideScript>().insideScenario == true && windAudio.isPlaying)
        {
            windAudio.Pause();
        }
        if (GameObject.Find("GoInside").GetComponent<GoInsideScript>().insideScenario == false && !windAudio.isPlaying)
        {
            windAudio.UnPause();
        }*/
    }

    public void ReplaceWindWith(int audioSourceNumber)
    {
        StartCoroutine(FadeAudioSource(audioSources[1], fadeoutDuration, 0));
        StartCoroutine(FadeAudioSource(audioSources[audioSourceNumber], fadeoutDuration, 1));
    }


    IEnumerator FadeAudioSource(AudioSource audio, float duration, float targetVolume)
    {
        int _steps = (int)(volumeChangesPerSecond * duration);
        float _stepTime = duration / _steps;
        float _stepSize = (targetVolume - audio.volume) / _steps;

        for(int i = 1; i < _steps; i++)
        {
            audio.volume += _stepSize;
            yield return new WaitForSeconds(_stepTime);
        }

        audio.volume = targetVolume;
    }

    IEnumerator SlowFadeAudioSource(AudioSource audio, float duration, float targetVolume)
    {
        int _steps = (int)(10 * duration);
        float _stepTime = duration / _steps;
        float _stepSize = (targetVolume - audio.volume) / _steps;

        for (int i = 1; i < _steps; i++)
        {
            audio.volume += _stepSize;
            yield return new WaitForSeconds(_stepTime);
        }

        audio.volume = targetVolume;
    }

}
