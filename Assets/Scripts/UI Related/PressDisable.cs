using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PressDisable : MonoBehaviour
{
    public GameObject next;
    public GameObject pauseMenu;
    public Button button;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        //{
            //Debug.Log("pressed");
        if (!Phase2Manager.scooped && !Phase2Manager.instructed)
        {
            if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
            {
                Phase2Manager.scooped = true;
                gameObject.SetActive(false);
                if (next)
                {
                    next.SetActive(true);
                }
                Debug.Log("scooped is " + Phase2Manager.scooped + "; instructed is " + Phase2Manager.instructed);
            }
        }
        else if(Phase2Manager.scooped && !Phase2Manager.instructed)
        {
            if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
            {
                Phase2Manager.instructed = true;
                gameObject.SetActive(false);
                if (next)
                {
                    next.SetActive(true);
                }
                Debug.Log("scooped is " + Phase2Manager.scooped + "; instructed is " + Phase2Manager.instructed);
        }
        }
        else
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                gameObject.SetActive(false);
                if (pauseMenu)
                {
                    pauseMenu.SetActive(true);
                    button.Select();
                }
                Debug.Log("scooped is " + Phase2Manager.scooped + "; instructed is " + Phase2Manager.instructed);
            }
        }


            //gameObject.SetActive(false);
            //if (next)
            //{
            //    next.SetActive(true);
            //}
            //Debug.Log("called");
        //}
        
    }
    //public void DisableSelf()
    //{
    //    gameObject.SetActive(false);
    //    if (next)
    //    {
    //        next.SetActive(true);
    //    }
    //    Debug.Log("called");
    //}
    
}
