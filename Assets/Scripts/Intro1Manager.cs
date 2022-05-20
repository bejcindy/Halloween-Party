using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro1Manager : MonoBehaviour
{
    public GameObject first, second;

    public bool L1,R1;

    float t;
    bool timer;

    // Start is called before the first frame update
    void Start()
    {
        L1 = false;
        R1 = false;
        second.SetActive(false);
        timer = false;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (R1 && first.gameObject.activeSelf)
        {
            first.SetActive(false);
            second.SetActive(true);
            R1 = false;
            timer = true;
        }
        if (timer)
        {
            if (t <= .5f)
            {
                t += Time.deltaTime;
            }
        }
        if (second.gameObject.activeSelf && t > .5f)
        {
            Debug.Log("here");
            if (L1)
            {
                SceneManager.LoadScene("Phase 1 Intro");
                Debug.Log("L1");
            }
            if (R1)
            {
                SceneManager.LoadScene("Phase 2 Intro");
                Debug.Log("R1");
            }
        }
    }
}
