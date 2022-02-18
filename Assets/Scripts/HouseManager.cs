using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public List<float> possibility;
    public List<int> revisits;
    public int[] rTimes;

    // Start is called before the first frame update
    void Start()
    {
        rTimes = new int[] { 0, 0, 0, 0 };
        switch (gameObject.tag)
        {
            case "House1":
                possibility = DataHolder.h1Pos;
                revisits = DataHolder.h1R;
                break;
            case "House2":
                possibility = DataHolder.h2Pos;
                revisits = DataHolder.h2R;
                break;
            case "House3":
                possibility = DataHolder.h3Pos;
                revisits = DataHolder.h3R;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
