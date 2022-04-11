using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Phase2Manager : MonoBehaviour
{
    public static bool scooped, instructed;
    public static bool timeUp;
    public GameObject pauseMenu;
    public Text p1, p2, p3, p4;
    public Text c1, c2, c3, c4;
    //public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        scooped = false;
        instructed = false;
        timeUp = false;
        //PlayerInputManager.Instantiate(playerPrefab) as GameObject;
    }
    void OnPlayerJoined(PlayerInput playerInput)
    {

        Debug.Log("PlayerInput ID: " + playerInput.playerIndex);
        switch (playerInput.playerIndex)
        {
            case 0:
                playerInput.gameObject.GetComponent<Phase2PlayerController>().placement = p1;
                playerInput.gameObject.GetComponent<Phase2PlayerController>().confirm = c1;
                break;
            case 1:
                playerInput.gameObject.GetComponent<Phase2PlayerController>().placement = p2;
                playerInput.gameObject.GetComponent<Phase2PlayerController>().confirm = c2;
                break;
            case 2:
                playerInput.gameObject.GetComponent<Phase2PlayerController>().placement = p3;
                playerInput.gameObject.GetComponent<Phase2PlayerController>().confirm = c3;
                break;
            case 3:
                playerInput.gameObject.GetComponent<Phase2PlayerController>().placement = p4;
                playerInput.gameObject.GetComponent<Phase2PlayerController>().confirm = c4;
                break;
        }
        //playerInput.gameObject.GetComponent<Phase2PlayerController>().placement

    }
    // Update is called once per frame
    void Update()
    {
        if (scooped && instructed && !timeUp)
        {
            if (Gamepad.current.startButton.wasPressedThisFrame)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
        }

    }
}
