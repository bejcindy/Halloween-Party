using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerNumber {Player1,Player2,Player3,Player4};
    [SerializeField]
    PlayerNumber player;
    //public float speed;

    public bool isRunning;

    int playerN;
    float speed, runSpeed;
    Rigidbody rb;
    string horizontal, vertical;
    float x, z, s;
    bool dontMove;
    float t, freezeTime;

    // Start is called before the first frame update
    void Start()
    {
        freezeTime = 2;
        dontMove = false;
        rb = GetComponent<Rigidbody>();
        runSpeed = speed * 2;
        speed = DataHolder.speed;
        runSpeed = DataHolder.runSpeed;
        isRunning = false;
        switch (player)
        {
            case PlayerNumber.Player1:
                playerN = 1;
                horizontal = "Horizontal1";
                vertical = "Vertical1";
                break;
            case PlayerNumber.Player2:
                playerN = 2;
                horizontal = "Horizontal2";
                vertical = "Vertical2";
                break;
            case PlayerNumber.Player3:
                playerN = 3;
                horizontal = "Horizontal3";
                vertical = "Vertical3";
                break;
            case PlayerNumber.Player4:
                playerN = 4;
                horizontal = "Horizontal4";
                vertical = "Vertical4";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dontMove)
        {
            //change to add force so that it's a burst
            switch (playerN)
        {
            case 1:
                    if (Input.GetKey(KeyCode.Joystick1Button1))
                {
                    s = runSpeed;
                    isRunning = true;
                }
                else
                {
                    s = speed;
                }
                    break;
            case 2:
                if (Input.GetKey(KeyCode.Joystick2Button1))
                {
                    s = runSpeed;
                    isRunning = true;
                }
                else
                {
                    s = speed;
                }
                break;
            case 3:
                if (Input.GetKey(KeyCode.Joystick3Button1))
                {
                    s = runSpeed;
                    isRunning = true;
                }
                else
                {
                    s = speed;
                }
                break;
            case 4:
                if (Input.GetKey(KeyCode.Joystick4Button1))
                {
                    s = runSpeed;
                    isRunning = true;
                }
                else
                {
                    s = speed;
                }
                break;
        }

            x = Input.GetAxis(horizontal) * s;
            z = Input.GetAxis(vertical) * s;
            
            rb.velocity = new Vector3(x, 0, z);
        }
        else
        {
            t += Time.deltaTime;
            //add a flashing animation here
            if (t >= freezeTime)
            {
                dontMove = false;
                t = 0;
            }
        }
        Vector3 direction = new Vector3(x, 0, z).normalized;
        if (x != 0 || z != 0)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //knock off your friend and lose candy!
        if (collision.gameObject.CompareTag("Player") && isRunning)
        {
            //check if this & the other player isRunning
            if (collision.gameObject.GetComponent<PlayerController>().isRunning)
            {
                //decide which candy to lose
                int candyType = Random.Range(0, 3);
                switch (playerN)
                {
                    case 1:
                        DataHolder.p1[candyType] -= 1;
                        break;
                    case 2:
                        DataHolder.p2[candyType] -= 1;
                        break;
                    case 3:
                        DataHolder.p3[candyType] -= 1;
                        break;
                    case 4:
                        DataHolder.p4[candyType] -= 1;
                        break;
                }
                dontMove = true;
                //instantiate candy model prefab (maybe add a forward & upward force so that it's not gonna be too close to players)
                string candyName = "Candy" + (candyType + 1);
                float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
                Instantiate(Resources.Load(candyName), transform.position, Quaternion.Euler(0, randomEulerY, 0));
            }

        }

        if (collision.gameObject.CompareTag("Candy") && !dontMove)
        {
            int candyCollect = collision.gameObject.GetComponent<DroppedCandy>().TypeOfCandy - 1;
            switch (playerN)
            {
                case 1:
                    DataHolder.p1[candyCollect] += 1;
                    break;
                case 2:
                    DataHolder.p2[candyCollect] += 1;
                    break;
                case 3:
                    DataHolder.p3[candyCollect] += 1;
                    break;
                case 4:
                    DataHolder.p4[candyCollect] += 1;
                    break;
            }
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //knock on neighbors' doors!
        if(other.CompareTag("House1")|| other.CompareTag("House2")|| other.CompareTag("House3"))
        {
            //add the cool down time here later
            //need animation
            KnockOnDoors(other.gameObject.GetComponent<HouseManager>().revisits, other.gameObject.GetComponent<HouseManager>().possibility, other.gameObject.GetComponent<HouseManager>().rTimes);
        }

        
    }

    void KnockOnDoors(List<int> hr,List<float> pos , int[] rTimes)
    {
        int c;
        switch (playerN)
        {
            case 1:
                if (rTimes[0] < hr[0])
                {
                   if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                    {
                        //add decrease here later
                        c = Random.Range(hr[1] - 1, hr[1] + 1);
                        DataHolder.p1 = new List<int> { DataHolder.p1[0] + (int)(c * pos[0]), DataHolder.p1[1] + (int)(c * pos[1]), DataHolder.p1[2] + (int)(c * pos[2]) };
                        rTimes[0] += 1;
                        Debug.Log("1: " + DataHolder.p1[0]+ " 2: " + DataHolder.p1[1]+ " 3: " + DataHolder.p1[2]);
                    }
                }
                
                break;
            case 2:
                if (rTimes[1] < hr[0])
                {
                    if (Input.GetKeyDown(KeyCode.Joystick2Button2))
                    {
                        //add decrease here later
                        c = Random.Range(hr[1] - 1, hr[1] + 1);
                        DataHolder.p2 = new List<int> { DataHolder.p2[0] + (int)(c * pos[0]), DataHolder.p2[1] + (int)(c * pos[1]), DataHolder.p2[2] + (int)(c * pos[2]) };
                        //Debug.Log("1: " + DataHolder.p1[0]+ " 2: " + DataHolder.p1[1]+ " 3: " + DataHolder.p1[2]);
                        rTimes[1] += 1;
                    }
                }
                    
                break;
            case 3:
                if (rTimes[2] < hr[0])
                {
                    if (Input.GetKeyDown(KeyCode.Joystick3Button2))
                    {
                        //add decrease here later
                        c = Random.Range(hr[1] - 1, hr[1] + 1);
                        DataHolder.p3 = new List<int> { DataHolder.p3[0] + (int)(c * pos[0]), DataHolder.p3[1] + (int)(c * pos[1]), DataHolder.p3[2] + (int)(c * pos[2]) };
                        //Debug.Log("1: " + DataHolder.p1[0]+ " 2: " + DataHolder.p1[1]+ " 3: " + DataHolder.p1[2]);
                        rTimes[2] += 1;
                    }
                }
              
                break;
            case 4:
                if (rTimes[3] < hr[0])
                {
                   if (Input.GetKeyDown(KeyCode.Joystick4Button2))
                    {
                        //add decrease here later
                        c = Random.Range(hr[1] - 1, hr[1] + 1);
                        DataHolder.p4 = new List<int> { DataHolder.p4[0] + (int)(c * pos[0]), DataHolder.p4[1] + (int)(c * pos[1]), DataHolder.p4[2] + (int)(c * pos[2]) };
                        //Debug.Log("1: " + DataHolder.p1[0]+ " 2: " + DataHolder.p1[1]+ " 3: " + DataHolder.p1[2]);
                        rTimes[3] += 1;
                    }
                }
             
                break;
        }
    }

}
