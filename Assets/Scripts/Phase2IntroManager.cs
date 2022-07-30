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

    bool movingRight, movingLeft;

    // Start is called before the first frame update
    void Start()
    {
        move = false;
        moveL = false;
        moveCounter = 1;
        LButton.SetActive(false);
        movingRight = false;
        movingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(moveCounter);
        if (move && !movingLeft)
        {
            if (moveCounter < 8)
            {
                if (thing.transform.position.x > -2f * moveCounter)
                {

                    thing.transform.Translate(Vector3.left * 5f * Time.deltaTime);
                    movingRight = true;
                }
                else
                {
                    thing.transform.position = new Vector3(-2f * moveCounter, 0, 0);
                    moveCounter++;
                    move = false;
                    movingRight = false;
                    moveL = false;
                    //Debug.Log("adding");
                }
            }
            else
            {
                SceneManager.LoadScene("Phase 2");
            }

        }
        if (moveL && !movingRight)
        {
            if (moveCounter > 1)
            {
                if (thing.transform.position.x < -2f * (moveCounter - 2))
                {

                    thing.transform.Translate(Vector3.right * 5f * Time.deltaTime);
                    movingLeft = true;
                }
                else
                {
                    thing.transform.position = new Vector3(-2f * (moveCounter - 2), 0, 0);
                    moveCounter--;
                    moveL = false;
                    movingLeft = false;
                    move = false;
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
