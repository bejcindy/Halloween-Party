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
    public static List<int> p1, p2, p3, p4;
    //Each players' number of candies that they have in ATM
    public static List<int> p1ATM, p2ATM, p3ATM, p4ATM;
    //Each players' number of candies in total
    public static List<int> p1Total, p2Total, p3Total, p4Total;
    //Each players' number of candies in total
    public static List<int> Bro;

    public Text playerCandies;
    public Text atm;
    public Text bro;

    //timer related variables
    public Text Timer;
    [SerializeField]
    int GameTime;
    float t;
    bool screenShotTaken;

    private void Awake()
    {
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
        p1 = new List<int>(3);
        p2 = new List<int>(3);
        p3 = new List<int>(3);
        p4 = new List<int>(3);
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
        ////check how many of each type of houses are in scene right now
        //Debug.Log(h1Pos.Count);

    }
    private void Start()
    {
        screenShotTaken = false;
        p1 = new List<int> { 0, 0, 0 };
        p2 = new List<int> { 0, 0, 0 };
        p3 = new List<int> { 0, 0, 0 };
        p4 = new List<int> { 0, 0, 0 };
        p1ATM = new List<int> { 0, 0, 0 };
        p2ATM = new List<int> { 0, 0, 0 };
        p3ATM = new List<int> { 0, 0, 0 };
        p4ATM = new List<int> { 0, 0, 0 };
        p1Total = new List<int> { 0, 0, 0 };
        p2Total = new List<int> { 0, 0, 0 };
        p3Total = new List<int> { 0, 0, 0 };
        p4Total = new List<int> { 0, 0, 0 };
        Bro = new List<int> { 0, 0, 0, 0 };
        t = GameTime;
        ScreenCapture.CaptureScreenshot("SomeLevel");
    }
    private void Update()
    {
        if (playerCandies)
        {
            playerCandies.text = "Player 1 Candies: " + p1[0] + ", " + p1[1] + ", " + p1[2] + " Player 2 Candies: " + p2[0] + ", " + p2[1] + ", " + p2[2] + " Player 3 Candies: " + p3[0] + ", " + p3[1] + ", " + p3[2] + " Player 4 Candies: " + p4[0] + ", " + p4[1] + ", " + p4[2];
        }
        if (atm)
        {
            atm.text = p1ATM[0] + ", " + p1ATM[1] + ", " + p1ATM[2];
        }
        if (bro)
        {
            bro.text = Bro[0] + ", " + Bro[1] + ", " + Bro[2] + ", " + Bro[3] + ", ";
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
                if (!screenShotTaken)
                {
                    string folderPath = "Assets/Screenshots/"; // the path of your project folder
                                        string screenshotName =
                                                                "Screenshot_" +
                                                                System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + // puts the current time right into the screenshot name
                                                                ".png"; // put youre favorite data format here
                                        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName), 2); // takes the sceenshot, the "2" is for the scaled resolution, you can put this to 600 but it will take really long to scale the image up
                    screenShotTaken = true;
                }
                
            }
            Timer.text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(t / 60), Mathf.FloorToInt(t % 60));
        }
            
        
    }


}
