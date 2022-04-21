using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase1UIManager : MonoBehaviour
{
    public Text p1txt, p2txt, p3txt, p4txt;
    public Text Timer;
    float t = 120;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (p1txt)
        {
            p1txt.text = "Candy Carrying: " + DataHolder.p1 + "\n" + "Candy Stored: " + DataHolder.p1ATM;
        }
        if (p2txt)
        {
            p2txt.text = "Candy Carrying: " + DataHolder.p2 + "\n" + "Candy Stored: " + DataHolder.p2ATM;
        }
        if (p3txt)
        {
            p3txt.text = "Candy Carrying: " + DataHolder.p3 + "\n" + "Candy Stored: " + DataHolder.p3ATM;
        }
        if (p4txt)
        {
            p4txt.text = "Candy Carrying: " + DataHolder.p4 + "\n" + "Candy Stored: " + DataHolder.p4ATM;
        }
        if (Timer)
        {
            if (t > 0)
            {
                t -= Time.deltaTime;
            }
            else
            {
                t = 0;

            }
            Timer.text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(t / 60), Mathf.FloorToInt(t % 60));
        }

    }
}
