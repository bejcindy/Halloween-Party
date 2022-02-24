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

    int playerN;
    float speed, runSpeed;
    Rigidbody rb;
    string horizontal, vertical;
    float x, z, s;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        runSpeed = speed * 2;
        speed = DataHolder.speed;
        runSpeed = DataHolder.runSpeed;
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
        //change to add force so that it's a burst
        switch (playerN)
        {
            case 1:
                if (Input.GetKey(KeyCode.Joystick1Button1))
                {
                    s = runSpeed;
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
            transform.rotation = Quaternion.LookRotation(zeroY, Vector3.forward);
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("House1") || other.CompareTag("House2") || other.CompareTag("House3"))
        {
            //add the cool down time here later
            //need animation
            //Debug.Log(other.gameObject);
            KnockOnDoors(other.gameObject.GetComponent<HouseManager>().revisits, other.gameObject.GetComponent<HouseManager>().possibility, other.gameObject.GetComponent<HouseManager>().rTimes);
        }
    }

    void KnockOnDoors(List<int> hr, List<float> pos, int[] rTimes)
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
                        Debug.Log("1: " + DataHolder.p1[0] + " 2: " + DataHolder.p1[1] + " 3: " + DataHolder.p1[2]);
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
