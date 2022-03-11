using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public List<float> possibility;
    public List<int> revisits;
    public int[] rTimes;

    public int totalRtime;
    float t;
    float coolDown;
    public bool CanCandy;
    Renderer r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<MeshRenderer>();
        CanCandy = true;
        rTimes = new int[] { 0, 0, 0, 0 };
        switch (gameObject.tag)
        {
            case "House1":
                possibility = DataHolder.h1Pos;
                revisits = DataHolder.h1R;
                coolDown = DataHolder.coolDownTime[0];
                break;
            case "House2":
                possibility = DataHolder.h2Pos;
                revisits = DataHolder.h2R;
                coolDown = DataHolder.coolDownTime[1];
                break;
            case "House3":
                possibility = DataHolder.h3Pos;
                revisits = DataHolder.h3R;
                coolDown = DataHolder.coolDownTime[2];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        totalRtime = rTimes[0] + rTimes[1] + rTimes[2] + rTimes[3];
        //if (totalRtime != 0)
        //{
        //    Debug.Log(rTimes[0]);
        //}
        if (totalRtime >= revisits[0])
        {
            t += Time.deltaTime;
            Debug.Log("t"+t);
            r.enabled = false;
            rTimes = new int[] { 0, 0, 0, 0 };
            CanCandy = false;
            if (t >= coolDown)
            {
                CanCandy = true;
                r.enabled = true;
                //rTimes= new int[] { 0, 0, 0, 0 };
                t = 0;
            }
        }
        
    }
}
