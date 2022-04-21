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
        public float Broccoli;
        [Header("Same Player Revisiting")]
        public int RevisitChance;
        public int StartNumber;
        public int RevistDecrease;
        public float coolDown;
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

    [SerializeField]
    float cameraTurnSpeed;
    public static float camTurnSpeed;

    public static List<float> h1Pos, h2Pos, h3Pos;
    public static List<int> h1R, h2R, h3R;
    public static List<float> coolDownTime;

    //Each players' number of candies that they are carrying
    public static int p1, p2, p3, p4;
    //Each players' number of candies that they have in ATM
    public static int p1ATM, p2ATM, p3ATM, p4ATM;
    //Each players' number of candies in total
    public static int p1Total, p2Total, p3Total, p4Total;
    //Each players' number of candies in total
    public static List<int> Bro;

    //public Text playerCandies;
    //public Text atm;
    //public Text bro;
    //public Text p1txt, p2txt, p3txt, p4txt;
    public GameObject c1, c2, c3, c4;
    public static bool c1Taken, c2Taken, c3Taken, c4Taken;

    //timer related variables
    public Text Timer;
    [SerializeField]
    int GameTime;
    float t;
    bool screenShotTaken;

    public static DataHolder instance = null;

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        screenShotTaken = false;
        speed = PlayerSpeed;
        runSpeed = PlayerRunSpeed;
        maxSteal = MaxSteal;
        minSteal = MinSteal;
        h1Pos = new List<float>(4);
        h2Pos = new List<float>(4);
        h3Pos = new List<float>(4);
        h1R = new List<int>(3);
        h2R = new List<int>(3);
        h3R = new List<int>(3);
        coolDownTime = new List<float>(3);
        //p1 = new List<int>(3);
        //p2 = new List<int>(3);
        //p3 = new List<int>(3);
        //p4 = new List<int>(3);
        h1Pos.Add(House1.Candy1);
        h1Pos.Add(House1.Candy2);
        h1Pos.Add(House1.Candy3);
        h1Pos.Add(House1.Broccoli);
        h2Pos.Add(House2.Candy1);
        h2Pos.Add(House2.Candy2);
        h2Pos.Add(House2.Candy3);
        h2Pos.Add(House2.Broccoli);
        h3Pos.Add(House3.Candy1);
        h3Pos.Add(House3.Candy2);
        h3Pos.Add(House3.Candy3);
        h3Pos.Add(House3.Broccoli);
        h1R.Add(House1.RevisitChance);
        h1R.Add(House1.StartNumber);
        h1R.Add(House1.RevistDecrease);
        h2R.Add(House2.RevisitChance);
        h2R.Add(House2.StartNumber);
        h2R.Add(House2.RevistDecrease);
        h3R.Add(House3.RevisitChance);
        h3R.Add(House3.StartNumber);
        h3R.Add(House3.RevistDecrease);
        coolDownTime.Add(House1.coolDown);
        coolDownTime.Add(House2.coolDown);
        coolDownTime.Add(House3.coolDown);
        camTurnSpeed = cameraTurnSpeed;
        Bro = new List<int> { 0, 0, 0, 0 };
        t = GameTime;
        ////check how many of each type of houses are in scene right now
        //Debug.Log(h1Pos.Count);

    }
    private void Start()
    {
        
    }
    private void Update()
    {
       
        
    }


}
