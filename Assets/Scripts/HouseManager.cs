using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    //public List<float> possibility;
    //public List<int> revisits;

    public int candyAmount;
    int spewAmount;

    public int[] rTimes;

    public int totalRtime;
    float t;
    public float coolDownTime;
    public bool stopGiving;
    public bool CanCandy;
    public GameObject door;
    public GameObject otherDoor;
    public Transform spewPos;
    public GameObject[] lightOn;
    public GameObject[] lightOff;
    bool opened;
    Renderer r;
    // Start is called before the first frame update
    void Start()
    {
        //r = GetComponent<MeshRenderer>();
        CanCandy = true;
        rTimes = new int[] { 0, 0, 0, 0 };
        opened = false;
        switch (gameObject.tag)
        {
            case "House1":
                candyAmount = 3;
                break;
            case "House2":
                candyAmount = 6;
                break;
            case "House3":
                candyAmount = 10;
                break;
        }
        spewAmount = 2 * candyAmount;
        //Debug.Log(candyAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (stopGiving)
        {
            t += Time.deltaTime;
            rTimes = new int[] { 0, 0, 0, 0 };
            CanCandy = false;
            if (!opened)
            {
                //door animaiton
                door.GetComponent<Animator>().SetBool("open", true);
                if (otherDoor)
                {
                    otherDoor.GetComponent<Animator>().SetBool("open", true);
                }

                //spew candy, right now equals candyAmount
                for(int i = 0; i < spewAmount; i++)
                {
                    int candyType = Random.Range(0, 3);
                    string candyName = "Candy" + (candyType + 1);
                    float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
                    Instantiate(Resources.Load(candyName), spewPos.position, Quaternion.Euler(0, randomEulerY, 0));
                }

                //turn off light
                for(int i = 0; i < lightOff.Length; i++)
                {
                    lightOff[i].SetActive(true);
                }
                for(int i = 0; i < lightOn.Length; i++)
                {
                    lightOn[i].SetActive(false);
                }

                opened = true;
            }
            if (t >= coolDownTime)
            {
                CanCandy = true;
                stopGiving = false;
                opened = false;
                //turn on light
                for (int i = 0; i < lightOff.Length; i++)
                {
                    lightOff[i].SetActive(false);
                }
                for (int i = 0; i < lightOn.Length; i++)
                {
                    lightOn[i].SetActive(true);
                }
                t = 0;
            }
        }
        
    }
}
