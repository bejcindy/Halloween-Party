using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenPlayerController : MonoBehaviour
{
    enum PlayerNumber { Player1, Player2, Player3, Player4 };
    [SerializeField]
    PlayerNumber player;

    //public float speed;
    public Transform cam;

    public bool isRunning;

    int playerN;
    float speed, runSpeed;
    Rigidbody rb;
    string horizontal, vertical;
    float x, z, s;
    bool dontMove;
    float t, freezeTime;

    bool saved1, saved2, saved3, saved4;

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
                    //if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                    //{
                    //    Debug.Log("pressed");
                    //}
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
            //rb.velocity = new Vector3(x, 0, z);
        
            Vector3 direction = (cam.forward * z + cam.right * x).normalized;
            Vector3 zeroY = new Vector3(direction.x, 0, direction.z);
            rb.velocity = zeroY * s;
            //rb.velocity = transform.TransformDirection(new Vector3(x * s, 0, z * s));
            if (x != 0 || z != 0)
            {
                transform.rotation = Quaternion.LookRotation(zeroY, Vector3.up);
            }
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
        #region Save To ATM
        if (saved1)
        {
            for (int i = 0; i < DataHolder.p1.Count; i++)
            {
                DataHolder.p1[i] = 0;

            }
            if (DataHolder.p1[DataHolder.p1.Count - 1] == 0)
            {
                saved1 = false;
            }
        }
        if (saved2)
        {
            for (int i = 0; i < DataHolder.p2.Count; i++)
            {
                DataHolder.p2[i] = 0;

            }
            if (DataHolder.p2[DataHolder.p2.Count - 1] == 0)
            {
                saved2 = false;
            }
        }
        if (saved3)
        {
            for (int i = 0; i < DataHolder.p3.Count; i++)
            {
                DataHolder.p3[i] = 0;

            }
            if (DataHolder.p3[DataHolder.p3.Count - 1] == 0)
            {
                saved3 = false;
            }
        }
        if (saved4)
        {
            for (int i = 0; i < DataHolder.p4.Count; i++)
            {
                DataHolder.p4[i] = 0;

            }
            if (DataHolder.p4[DataHolder.p4.Count - 1] == 0)
            {
                saved4 = false;
            }
        }
        #endregion

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

        if (other.CompareTag("House1") || other.CompareTag("House2") || other.CompareTag("House3"))
        {
            //add the cool down time here later
            //need animation
            //Debug.Log(other.gameObject);
            KnockOnDoors(other.gameObject.GetComponent<HouseManager>().revisits, other.gameObject.GetComponent<HouseManager>().possibility, other.gameObject.GetComponent<HouseManager>().rTimes, other.gameObject.GetComponent<HouseManager>().CanCandy);
        }
        if (other.CompareTag("ATM"))
        {
            switch (playerN)
            {
                case 1:
                    if (!saved1 && Input.GetKeyDown(KeyCode.Joystick1Button2))
                    {
                        for (int i = 0; i < DataHolder.p1.Count; i++)
                        {
                            DataHolder.p1ATM[i] += DataHolder.p1[i];
                            //Debug.Log(DataHolder.p1ATM[i]);
                            saved1 = true;
                        }

                    }

                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.Joystick2Button2))
                    {
                        for (int i = 0; i < DataHolder.p2.Count; i++)
                        {
                            DataHolder.p2ATM[i] = DataHolder.p2[i];
                            DataHolder.p2[i] = 0;
                        }
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.Joystick3Button2))
                    {
                        for (int i = 0; i < DataHolder.p3.Count; i++)
                        {
                            DataHolder.p3ATM[i] = DataHolder.p3[i];
                            DataHolder.p3[i] = 0;
                        }
                    }
                    break;
                case 4:
                    if (Input.GetKeyDown(KeyCode.Joystick4Button2))
                    {
                        for (int i = 0; i < DataHolder.p4.Count; i++)
                        {
                            DataHolder.p4ATM[i] = DataHolder.p4[i];
                            DataHolder.p4[i] = 0;
                        }
                    }
                    break;
            }
        }

    }

    void KnockOnDoors(List<int> hr, List<float> pos, int[] rTimes, bool can)
    {
        int c;
        int b;
        if (can)
        {
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
                            b = Random.Range(0, 100);
                            if (b <= 15)
                            {
                                DataHolder.Bro[0]++;
                            }
                            rTimes[0] += 1;
                            //totalR+=1;
                            Debug.Log("rTimes" + rTimes[0]);
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
                            b = Random.Range(0, 100);
                            if (b <= 15)
                            {
                                DataHolder.Bro[1]++;
                            }
                            //totalR += 1;
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
                            b = Random.Range(0, 100);
                            if (b <= 15)
                            {
                                DataHolder.Bro[2]++;
                            }
                            //totalR += 1;
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
                            b = Random.Range(0, 100);
                            if (b <= 15)
                            {
                                DataHolder.Bro[3]++;
                            }
                            //totalR += 1;
                            rTimes[3] += 1;
                        }
                    }

                    break;
            }

        }
    }
}
