using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [SerializeField]
        House House1, House2, House3;
    [System.Serializable]
    public class House
    {
        [Header("Possibilities of Giving Candy (Less Than 1)")]
        public float Candy1;
        public float Candy2;
        public float Candy3;
        [Header("Same Player Revisiting")]
        public int RevisitChance;
        public int RevistDecreaseMin;
        public int RevistDecreaseMax;
        
    }

    [Header("Player Related")]
    [SerializeField]
    float PlayerSpeed;
    public static float speed;

    [SerializeField]
    float MaxSteal;
    public static float maxSteal;

    [SerializeField]
    float MinSteal;
    public static float minSteal;

    //[HideInInspector]
    public static List<float> h1Pos, h2Pos, h3Pos;
    public static List<int> h1R, h2R, h3R;

    //display each players' number of candies

    private void Awake()
    {
        speed = PlayerSpeed;
        maxSteal = MaxSteal;
        minSteal = MinSteal;
        h1Pos = new List<float>(3);
        h2Pos = new List<float>(3);
        h3Pos = new List<float>(3);
        h1R = new List<int>(3);
        h2R = new List<int>(3);
        h3R = new List<int>(3);
        h1Pos.Add(House1.Candy1);
        h1Pos.Add(House1.Candy2);
        h1Pos.Add(House1.Candy3);
        h2Pos.Add(House2.Candy1);
        h2Pos.Add(House2.Candy2);
        h2Pos.Add(House2.Candy3);
        h3Pos.Add(House3.Candy1);
        h3Pos.Add(House3.Candy2);
        h3Pos.Add(House3.Candy3);
        h1R.Add(House1.RevisitChance);
        h1R.Add(House1.RevistDecreaseMin);
        h1R.Add(House1.RevistDecreaseMax);
        h2R.Add(House2.RevisitChance);
        h2R.Add(House2.RevistDecreaseMin);
        h2R.Add(House2.RevistDecreaseMax);
        h3R.Add(House3.RevisitChance);
        h3R.Add(House3.RevistDecreaseMin);
        h3R.Add(House3.RevistDecreaseMax);
        ////check how many of each type of houses are in scene right now
        //Debug.Log(h1Pos.Count);

    }

    

}
