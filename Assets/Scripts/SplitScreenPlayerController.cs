using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SplitScreenPlayerController : MonoBehaviour
{
    

    //public float speed;
    public GameObject cam;

    bool isRunning;

    int playerN;
    float speed = 10;
    float runSpeed = 15;
    Rigidbody rb;
    
    float x, z, currentSpeed;
    bool dontMove;
    float t, freezeTime;

    bool saved1, saved2, saved3, saved4;
    Vector2 movement;
    Vector2 rightStick;
    PlayerInput playerInput;
    bool knocking, knocked;
    float knockCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        freezeTime = 2;
        dontMove = false;
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        cam = GameObject.FindGameObjectWithTag("P1");
        isRunning = false;
        cam.GetComponent<SplitScreenCamera>().player = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dontMove)
        {
            if (isRunning)
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = speed;
            }

            x = movement.x * currentSpeed;
            z = movement.y * currentSpeed;

            cam.GetComponent<SplitScreenCamera>().horizontal = rightStick.x;
            Vector3 direction = (cam.transform.forward * z + cam.transform.right * x).normalized;
            Vector3 zeroY = new Vector3(direction.x, 0, direction.z);
            rb.velocity = zeroY*currentSpeed;
            //rb.velocity = transform.TransformDirection(new Vector3(x * s, 0, z * s));
            if (x != 0 || z != 0)
            {
                transform.rotation = Quaternion.LookRotation(zeroY, Vector3.up);
            }
            //Debug.Log(isRunning);
            Debug.Log(knocking);
            if (knocked)
            {
                knockCoolDown += Time.deltaTime;
                if (knockCoolDown >= 1f)
                {
                    knocked = false;
                    knockCoolDown = 0;
                }
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
        //if (saved1)
        //{
        //    for (int i = 0; i < DataHolder.p1.Count; i++)
        //    {
        //        DataHolder.p1[i] = 0;

        //    }
        //    if (DataHolder.p1[DataHolder.p1.Count - 1] == 0)
        //    {
        //        saved1 = false;
        //    }
        //}
        //if (saved2)
        //{
        //    for (int i = 0; i < DataHolder.p2.Count; i++)
        //    {
        //        DataHolder.p2[i] = 0;

        //    }
        //    if (DataHolder.p2[DataHolder.p2.Count - 1] == 0)
        //    {
        //        saved2 = false;
        //    }
        //}
        //if (saved3)
        //{
        //    for (int i = 0; i < DataHolder.p3.Count; i++)
        //    {
        //        DataHolder.p3[i] = 0;

        //    }
        //    if (DataHolder.p3[DataHolder.p3.Count - 1] == 0)
        //    {
        //        saved3 = false;
        //    }
        //}
        //if (saved4)
        //{
        //    for (int i = 0; i < DataHolder.p4.Count; i++)
        //    {
        //        DataHolder.p4[i] = 0;

        //    }
        //    if (DataHolder.p4[DataHolder.p4.Count - 1] == 0)
        //    {
        //        saved4 = false;
        //    }
        //}
        #endregion

    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    //knock off your friend and lose candy!
    //    if (collision.gameObject.CompareTag("Player") && isRunning)
    //    {
    //        //check if this & the other player isRunning
    //        if (collision.gameObject.GetComponent<PlayerController>().isRunning)
    //        {
    //            //decide which candy to lose
    //            int candyType = Random.Range(0, 3);
    //            switch (playerN)
    //            {
    //                case 1:
    //                    DataHolder.p1[candyType] -= 1;
    //                    break;
    //                case 2:
    //                    DataHolder.p2[candyType] -= 1;
    //                    break;
    //                case 3:
    //                    DataHolder.p3[candyType] -= 1;
    //                    break;
    //                case 4:
    //                    DataHolder.p4[candyType] -= 1;
    //                    break;
    //            }
    //            dontMove = true;
    //            //instantiate candy model prefab (maybe add a forward & upward force so that it's not gonna be too close to players)
    //            string candyName = "Candy" + (candyType + 1);
    //            float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
    //            Instantiate(Resources.Load(candyName), transform.position, Quaternion.Euler(0, randomEulerY, 0));
    //        }

    //    }

    //    if (collision.gameObject.CompareTag("Candy") && !dontMove)
    //    {
    //        int candyCollect = collision.gameObject.GetComponent<DroppedCandy>().TypeOfCandy - 1;
    //        switch (playerN)
    //        {
    //            case 1:
    //                DataHolder.p1[candyCollect] += 1;
    //                break;
    //            case 2:
    //                DataHolder.p2[candyCollect] += 1;
    //                break;
    //            case 3:
    //                DataHolder.p3[candyCollect] += 1;
    //                break;
    //            case 4:
    //                DataHolder.p4[candyCollect] += 1;
    //                break;
    //        }
    //        Destroy(collision.gameObject);
    //    }

    //}

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("House1") || other.CompareTag("House2") || other.CompareTag("House3"))
        {
            //add the cool down time here later
            //need animation
            Debug.Log(other.gameObject.GetComponent<HouseManager>().candyAmount);

            KnockOnDoors(other.gameObject.GetComponent<HouseManager>().candyAmount, other.gameObject.GetComponent<HouseManager>().rTimes, other.gameObject.GetComponent<HouseManager>().CanCandy);
        }
        //if (other.CompareTag("ATM"))
        //{
        //    switch (playerN)
        //    {
        //        case 1:
        //            if (!saved1 && Input.GetKeyDown(KeyCode.Joystick1Button2))
        //            {
        //                for (int i = 0; i < DataHolder.p1.Count; i++)
        //                {
        //                    DataHolder.p1ATM[i] += DataHolder.p1[i];
        //                    //Debug.Log(DataHolder.p1ATM[i]);
        //                    saved1 = true;
        //                }

        //            }

        //            break;
        //        case 2:
        //            if (Input.GetKeyDown(KeyCode.Joystick2Button2))
        //            {
        //                for (int i = 0; i < DataHolder.p2.Count; i++)
        //                {
        //                    DataHolder.p2ATM[i] = DataHolder.p2[i];
        //                    DataHolder.p2[i] = 0;
        //                }
        //            }
        //            break;
        //        case 3:
        //            if (Input.GetKeyDown(KeyCode.Joystick3Button2))
        //            {
        //                for (int i = 0; i < DataHolder.p3.Count; i++)
        //                {
        //                    DataHolder.p3ATM[i] = DataHolder.p3[i];
        //                    DataHolder.p3[i] = 0;
        //                }
        //            }
        //            break;
        //        case 4:
        //            if (Input.GetKeyDown(KeyCode.Joystick4Button2))
        //            {
        //                for (int i = 0; i < DataHolder.p4.Count; i++)
        //                {
        //                    DataHolder.p4ATM[i] = DataHolder.p4[i];
        //                    DataHolder.p4[i] = 0;
        //                }
        //            }
        //            break;
        //    }
        //}

    }

    void KnockOnDoors(int candyAmount, int[] rTimes, bool can)
    {
        int c;
        int b;
        Debug.Log(rTimes[playerInput.playerIndex] );
        if (can)
        {
            if (rTimes[playerInput.playerIndex] < 3)
            {
                if (knocking && !knocked) 
                {
                    //add decrease here later
                    //c = candyAmount;
                    DataHolder.p1 +=candyAmount;
                    //if (b <= 15)
                    //{
                    //    DataHolder.Bro[0]++;
                    //}
                    rTimes[0] += 1;
                    knocked = true;
                    //totalR+=1;
                    Debug.Log("called knocking");
                    //Debug.Log("rTimes" + rTimes[0]);
                }
                
            }
        }
    }

    

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        rightStick = context.ReadValue<Vector2>();
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        isRunning = context.action.triggered;
    }
    public void OnKnock(InputAction.CallbackContext context)
    {
        knocking = context.action.triggered;
    }
}
