using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Happen2Play1 : MonoBehaviour
{
    public AudioClip bumpSound;

    AudioSource aud;

    public bool p1Bump, p2Bump, p3Bump, p4Bump;

    int bumpCount;

    // Start is called before the first frame update
    void Start()
    {
        p1Bump = false;
        p2Bump = false;
        p3Bump = false;
        p4Bump = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
