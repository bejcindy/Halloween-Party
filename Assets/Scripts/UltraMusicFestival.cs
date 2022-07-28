using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UltraMusicFestival : MonoBehaviour
{
    public float fadeDuration;

    public AudioClip StartScene, Phase1, Phase2;

    public AudioMixerGroup bgm;

    float timeElapsed;
    AudioSource track1, track2;
    bool t1Playing;

    public static UltraMusicFestival instance = null;

    bool changed;
    bool played;
    bool beginning;

    string previousScene;

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        track1 = gameObject.AddComponent<AudioSource>();
        track2 = gameObject.AddComponent<AudioSource>();
        t1Playing = true;
        track1.clip = StartScene;
        track1.Play();
        track2.Stop();
        track1.loop = true;
        track2.loop = true;
        track1.outputAudioMixerGroup = bgm;
        track2.outputAudioMixerGroup = bgm;
        changed = false;
        played = false;
        beginning = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Title Scene":
                //add stuff here later related to replay start menu
                if (!beginning)
                {
                    CrossFade(StartScene);
                }
                previousScene = "Title Scene";
                break;
            case "intro1":
                /*if(previousScene!="Title Scene"|| previousScene != "Phase 1 Intro"|| previousScene != "Phase 2 Intro")
                {
                    CrossFade(StartScene);
                    if (changed)
                    {
                        previousScene = "intro1";
                        changed = false;
                    }
                    //previousScene = "intro1";
                }*/

                break;
            case "Phase 1 Intro":
                /*if (previousScene != "Title Scene" || previousScene != "intro1" || previousScene != "Phase 2 Intro")
                {
                    CrossFade(StartScene);
                    if (changed)
                    {
                        previousScene = "Phase 1 Intro";
                        changed = false;
                    }
                    
                }*/
                break;
            case "Phase 2 Intro":
                /*if (previousScene != "Title Scene" || previousScene != "intro1" || previousScene != "Phase 1 Intro")
                {
                    CrossFade(StartScene);
                    if (changed)
                    {
                        previousScene = "Phase 2 Intro";
                        changed = false;
                    }
                    
                }*/
                break;
            case "LIL Level 1":
                if (previousScene!= "LIL Level 1")
                {
                    CrossFade(Phase1);
                    if (changed)
                    {
                        previousScene = "LIL Level 1";
                        changed = false;
                    }
                }
                break;
            case "LIL Level 2":
                if (previousScene != "LIL Level 2")
                {
                    CrossFade(Phase1);
                    if (changed)
                    {
                        previousScene = "LIL Level 2";
                        changed = false;
                    }
                }
                break;
            case "LIL Level 3":
                if (previousScene != "LIL Level 3")
                {
                    CrossFade(Phase1);
                    if (changed)
                    {
                        previousScene = "LIL Level 3";
                        changed = false;
                    }
                }
                break;
            case "Phase 2":
                if (previousScene != "Phase 2")
                {
                    CrossFade(Phase2);
                    if (changed)
                    {
                        previousScene = "Phase 2";
                        changed = false;
                    }
                }
                break;
        }
    }

    void CrossFade(AudioClip nextA)
    {
        //Debug.Log("runnign");
        if (t1Playing)
        {
            track2.clip = nextA;
            if (!played)
            {
                track2.Play();
                played = true;
            }

            if (timeElapsed < fadeDuration)
            {
                track2.volume = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
                track1.volume = Mathf.Lerp(1, 0, timeElapsed / fadeDuration);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                track1.Stop();
                t1Playing = false;
                played = false;
                changed = true;
            }
        }
        else
        {
            track1.clip = nextA;
            if (!played)
            {
                track1.Play();
                played = true;
            }


            if (timeElapsed < fadeDuration)
            {
                track1.volume = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
                track2.volume = Mathf.Lerp(1, 0, timeElapsed / fadeDuration);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                track2.Stop();
                t1Playing = true;
                played = false;
                changed = true;
            }
        }
    }

}
