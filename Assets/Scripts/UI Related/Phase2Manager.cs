using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Phase2Manager : MonoBehaviour
{
    public static bool scooped, instructed;
    public static bool timeUp;
    public GameObject pauseMenu;
    public Text p1, p2, p3, p4;
    public Text c1, c2, c3, c4;
    public Text scoops;

    //public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        scooped = false;
        instructed = false;
        timeUp = false;

        scoops.text = "P1: " + ScoopCalculator(DataHolder.p1ATM) + " P2: " + ScoopCalculator(DataHolder.p2ATM) + " P3: " + ScoopCalculator(DataHolder.p3ATM) + " P4: " + ScoopCalculator(DataHolder.p4ATM);
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
        if (c1.gameObject.activeSelf && c2.gameObject.activeSelf && c3.gameObject.activeSelf && c4.gameObject.activeSelf)
        {
            DataHolder.p1 = 0;
            DataHolder.p2 = 0;
            DataHolder.p3 = 0;
            DataHolder.p4 = 0;
            DataHolder.p1ATM = 0;
            DataHolder.p2ATM = 0;
            DataHolder.p3ATM = 0;
            DataHolder.p4ATM = 0;
            DataHolder.p1Total = 0;
            DataHolder.p2Total = 0;
            DataHolder.p3Total = 0;
            DataHolder.p4Total = 0;
            SceneManager.LoadScene(0);

        }
    }

    string ScoopCalculator(int n)
    {
        if (n <= 25)
        {
            return "2 Scoop";
        }else if (n > 25 && n <= 50)
        {
            return "3 Scoops";
        }else if (n > 50 && n <= 75)
        {
            return "4 Scoops";
        }else
        {
            return "5 Scoops";
        }
    }
}
