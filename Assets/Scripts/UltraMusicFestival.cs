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
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                //add stuff here later related to replay start menu
                if (!beginning)
                {
                    CrossFade(StartScene);
                }
                break;
            case 1:
                if (!changed)
                {
                    CrossFade(Phase1);
                }
                break;
            case 2:
                if (!changed)
                {
                    CrossFade(Phase1);
                }
                break;
            case 3:
                if (!changed)
                {
                    CrossFade(Phase1);
                }
                break;
            case 4:
                if (changed)
                {
                    CrossFade(Phase2);
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
                changed = !changed;
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
                changed = !changed;
            }
        }
    }

}
