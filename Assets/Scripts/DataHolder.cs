using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        public int StartNumber;
        public int RevistDecrease;
        
    }

    [Header("Player Related")]
    [SerializeField]
    float PlayerSpeed;
    public static float speed;

    [SerializeField]
    float PlayerRunSpeed;
    public static float runSpeed;

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
    public static List<int> p1, p2, p3, p4;

    public Text playerCandies;

    private void Awake()
    {
        speed = PlayerSpeed;
        runSpeed = PlayerRunSpeed;
        maxSteal = MaxSteal;
        minSteal = MinSteal;
        h1Pos = new List<float>(3);
        h2Pos = new List<float>(3);
        h3Pos = new List<float>(3);
        h1R = new List<int>(3);
        h2R = new List<int>(3);
        h3R = new List<int>(3);
        p1 = new List<int>(3);
        //Debug.Log("p1" + p1[0]);
        p2 = new List<int>(3);
        p3 = new List<int>(3);
        p4 = new List<int>(3);
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
        h1R.Add(House1.StartNumber);
        h1R.Add(House1.RevistDecrease);
        h2R.Add(House2.RevisitChance);
        h2R.Add(House2.StartNumber);
        h2R.Add(House2.RevistDecrease);
        h3R.Add(House3.RevisitChance);
        h3R.Add(House3.StartNumber);
        h3R.Add(House3.RevistDecrease);
        ////check how many of each type of houses are in scene right now
        //Debug.Log(h1Pos.Count);

    }
    private void Start()
    {
        p1 = new List<int> { 0, 0, 0 };
        p2 = new List<int> { 0, 0, 0 };
        p3 = new List<int> { 0, 0, 0 };
        p4 = new List<int> { 0, 0, 0 };
        //playerCandies.text = "Player 1 Candies: " + p1[0] + ", " + p1[1] + ", " + p1[2] + " Player 2 Candies: " + p2[0] + ", " + p2[1] + ", " + p2[2] + " Player 3 Candies: " + p3[0] + ", " + p3[1] + ", " + p3[2] + " Player 4 Candies: " + p4[0] + ", " + p4[1] + ", " + p4[2];
    }
    private void Update()
    {
        playerCandies.text = "Player 1 Candies: " + p1[0] + ", " + p1[1] + ", " + p1[2] + " Player 2 Candies: " + p2[0] + ", " + p2[1] + ", " + p2[2] + " Player 3 Candies: " + p3[0] + ", " + p3[1] + ", " + p3[2] + " Player 4 Candies: " + p4[0] + ", " + p4[1] + ", " + p4[2];
    }


}
