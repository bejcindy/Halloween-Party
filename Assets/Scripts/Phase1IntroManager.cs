using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Phase1IntroManager : MonoBehaviour
{
    public bool move;
    public bool moveL;
    float dist = -7.9f;
    int moveCounter;
    public GameObject thing;
    public Button tut;



    // Start is called before the first frame update
    void Start()
    {
        move = false;
        moveCounter = 1;
        tut.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (moveCounter < 9)
            {
                if (thing.transform.position.x > -7.9f * moveCounter)
                {
                    
                    thing.transform.Translate(Vector3.left *5f* Time.deltaTime);

                }
                else
                {
                    thing.transform.position = new Vector3(-7.9f * moveCounter, 0, 0);
                    moveCounter++;
                    move = false;
                    //Debug.Log("adding");
                }
            }
            else
            {
                SceneManager.LoadScene("LIL Level 1");
            }

        }
        if (moveL)
        {
            if (moveCounter >1 )
            {
                if (thing.transform.position.x < -7.9f * (moveCounter-2))
                {
                    
                    thing.transform.Translate(Vector3.right * 5f*Time.deltaTime);
                    
                }
                else
                {
                    thing.transform.position = new Vector3(-7.9f * (moveCounter-2), 0, 0);
                    moveCounter--;
                    moveL = false;
                    //Debug.Log("minus");
                }
            }
        }
        //Debug.Log(moveCounter);
    }

    public void Tutorial()
    {
        if (moveCounter == 1)
        {
            move = true;
        }
    }

    public void Level1()
    {
        if (moveCounter == 1)
        {
            SceneManager.LoadScene("LIL Level 1");
        }
    }
    public void Level2()
    {
        if (moveCounter == 1)
        {
            SceneManager.LoadScene("LIL Level 2");
        }
    }
    public void Level3()
    {
        if (moveCounter == 1)
        {
            SceneManager.LoadScene("LIL Level 3");
        }
    }

}
