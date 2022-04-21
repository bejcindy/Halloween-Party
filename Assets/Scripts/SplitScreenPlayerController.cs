using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SplitScreenPlayerController : MonoBehaviour
{
    

    //public float speed;
    public GameObject cam;

    public bool isRunning;

    int playerN;
    float speed = 10;
    float runSpeed = 15;
    Rigidbody rb;
    
    float x, z, currentSpeed;
    bool dontMove;
    float t, freezeTime;

    bool saved;
    Vector2 movement;
    Vector2 rightStick;
    PlayerInput playerInput;
    bool knocking, knocked;
    float knockCoolDown;
    bool camReset;

    // Start is called before the first frame update
    void Start()
    {
        freezeTime = 2;
        dontMove = false;
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        isRunning = false;
        camReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cam)
        {
            switch (playerInput.playerIndex)
            {
                case 0:
                    cam = GameObject.FindGameObjectWithTag("P1");
                    break;
                case 1:
                    cam = GameObject.FindGameObjectWithTag("P2");
                    break;
                case 2:
                    cam = GameObject.FindGameObjectWithTag("P3");
                    break;
                case 3:
                    cam = GameObject.FindGameObjectWithTag("P4");
                    break;
            }

            cam.GetComponent<SplitScreenCamera>().player = transform;
        }
        if (cam)
        {
            cam.GetComponent<SplitScreenCamera>().horizontal = rightStick.x;
            cam.GetComponent<SplitScreenCamera>().vertical = rightStick.y;
            cam.GetComponent<SplitScreenCamera>().reset = camReset;
        }
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

            
            Vector3 direction = (cam.transform.forward * z + cam.transform.right * x).normalized;
            Vector3 zeroY = new Vector3(direction.x, 0, direction.z);
            rb.velocity = zeroY*currentSpeed;
            //rb.velocity = transform.TransformDirection(new Vector3(x * s, 0, z * s));
            if (x != 0 || z != 0)
            {
                transform.rotation = Quaternion.LookRotation(zeroY, Vector3.up);
            }
            //Debug.Log(isRunning);
            //Debug.Log(knocking);
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
        if (saved)
        {
            switch (playerInput.playerIndex)
            {
                case 0:
                    DataHolder.p1 = 0;
                    break;
                case 1:
                    DataHolder.p2 = 0;
                    break;
                case 2:
                    DataHolder.p3 = 0;
                    break;
                case 3:
                    DataHolder.p4 = 0;
                    break;
            }
            saved = false;
        }
        #endregion

    }
    private void OnCollisionEnter(Collision collision)
    {
        //knock off your friend and lose candy!
        if (collision.gameObject.CompareTag("Player") && isRunning)
        {
            //check if this & the other player isRunning
            if (collision.gameObject.GetComponent<SplitScreenPlayerController>().isRunning)
            {
                int candyType = Random.Range(0, 3);
                switch (playerInput.playerIndex)
                {
                    case 0:
                        DataHolder.p1 -= 1;
                        break;
                    case 1:
                        DataHolder.p2 -= 1;
                        break;
                    case 2:
                        DataHolder.p3 -= 1;
                        break;
                    case 3:
                        DataHolder.p4 -= 1;
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
            switch (playerInput.playerIndex)
            {
                case 0:
                    DataHolder.p1 += 1;
                    break;
                case 1:
                    DataHolder.p2 += 1;
                    break;
                case 2:
                    DataHolder.p3 += 1;
                    break;
                case 3:
                    DataHolder.p4 += 1;
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

            KnockOnDoors(other.gameObject.GetComponent<HouseManager>().candyAmount, other.gameObject.GetComponent<HouseManager>().rTimes, other.gameObject.GetComponent<HouseManager>().CanCandy);
        }

        if (other.CompareTag("ATM"))
        {
            if (knocking&&!saved)
            {
                switch (playerInput.playerIndex)
                {
                    case 0:
                        DataHolder.p1ATM += DataHolder.p1;
                        break;
                    case 1:
                        DataHolder.p2ATM += DataHolder.p2;
                        break;
                    case 2:
                        DataHolder.p3ATM += DataHolder.p3;
                        break;
                    case 3:
                        DataHolder.p4ATM += DataHolder.p4;
                        break;
                }
                saved = true;
            }
        }

    }

    void KnockOnDoors(int candyAmount, int[] rTimes, bool can)
    {
        if (can)
        {
            if (rTimes[playerInput.playerIndex] < 3)
            {
                if (knocking && !knocked) 
                {
                    switch (playerInput.playerIndex)
                    {
                        case 0:
                            DataHolder.p1 += candyAmount;
                            break;
                        case 1:
                            DataHolder.p2 += candyAmount;
                            break;
                        case 2:
                            DataHolder.p3 += candyAmount;
                            break;
                        case 3:
                            DataHolder.p4 += candyAmount;
                            break;
                    }
                    
                    rTimes[playerInput.playerIndex] += 1;
                    knocked = true;
                    Debug.Log("called knocking");
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
    public void ResetCam(InputAction.CallbackContext context)
    {
        camReset = context.action.triggered;
    }
}
