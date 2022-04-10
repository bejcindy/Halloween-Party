using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject instruction;
    public GameObject phase1things;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void Help()
    {
        instruction.SetActive(true);
        Debug.Log("helped");
    }
    public void BackToTitle()
    {

    }
    public void Phase1Stuff()
    {
        phase1things.SetActive(true);
        Debug.Log("phase1ed");
    }
    
}
