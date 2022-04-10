using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Phase2Manager : MonoBehaviour
{
    public static bool scooped, instructed;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        scooped = false;
        instructed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (scooped && instructed)
        {
            if (Gamepad.current.startButton.wasPressedThisFrame)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
        }

    }
}
