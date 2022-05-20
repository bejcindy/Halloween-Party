using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phase2IntroManager : MonoBehaviour
{
    public bool move;
    public bool moveL;
    float dist = -2f;
    int moveCounter;
    public GameObject thing;
    public GameObject LButton;

    // Start is called before the first frame update
    void Start()
    {
        move = false;
        moveL = false;
        moveCounter = 1;
        LButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (moveCounter < 8)
            {
                if (thing.transform.position.x > -2f * moveCounter)
                {

                    thing.transform.Translate(Vector3.left * 5f * Time.deltaTime);

                }
                else
                {
                    thing.transform.position = new Vector3(-2f * moveCounter, 0, 0);
                    moveCounter++;
                    move = false;
                    //Debug.Log("adding");
                }
            }
            else
            {
                SceneManager.LoadScene("Phase 2");
            }

        }
        if (moveL)
        {
            if (moveCounter > 1)
            {
                if (thing.transform.position.x < -2f * (moveCounter - 2))
                {

                    thing.transform.Translate(Vector3.right * 5f * Time.deltaTime);

                }
                else
                {
                    thing.transform.position = new Vector3(-2f * (moveCounter - 2), 0, 0);
                    moveCounter--;
                    moveL = false;
                    //Debug.Log("minus");
                }
            }
        }
        if (moveCounter > 1)
        {
            LButton.SetActive(true);
        }
        else
        {
            LButton.SetActive(false);
        }
    }
    public void SkipIntro()
    {
        SceneManager.LoadScene("Phase 2");
    }
}
