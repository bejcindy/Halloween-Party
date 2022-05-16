using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//using Cinemachine;



public class TestBuddiesController : MonoBehaviour
{
    //public float speed;
    public GameObject cam;

    public bool isRunning;

    public float jumpForce;

    float candyCarried;
    public float candySlowDown=0.98f;
    public bool attacked;

    Animator anim;

    int playerN;
    float speed = 15;
    //[SerializeField]
    //[Range(15,50)]
    float runSpeed = 20;

    Rigidbody rb;

    float x, z, currentSpeed;
    bool dontMove;
    float t, freezeTime;

    bool saved;
    Vector2 movement;
    Vector2 actualMovement;
    Vector2 rightStick;
    PlayerInput playerInput;
    bool knocking, knocked;
    float knockCoolDown;
    bool camReset;
    bool jump;
    bool isGrounded;
    bool canJump;
    bool safe;
    bool doing;


    // Start is called before the first frame update
    void Start()
    {
        freezeTime = 2f;
        dontMove = false;
        doing = false;
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        isRunning = false;
        camReset = false;
        jump = false;
        attacked = false;
        safe = false;
        Debug.Log("called");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.name+" movement:" + movement);
        if (transform.GetChild(0))
        {
            anim = transform.GetChild(0).GetComponent<Animator>();
        }
        //read the candy amount
        switch (playerInput.playerIndex)
        {
            case 0:
                candyCarried = DataHolder.p1;
                break;
            case 1:
                candyCarried = DataHolder.p2;
                break;
            case 2:
                candyCarried = DataHolder.p3;
                break;
            case 3:
                candyCarried = DataHolder.p4;
                break;
        }

        if (!cam)
        {
            switch (playerInput.playerIndex)
            {
                case 0:
                    cam = GameObject.FindGameObjectWithTag("P1");
                    Debug.Log("foudn p1"+cam.name);
                    break;
                case 1:
                    cam = GameObject.FindGameObjectWithTag("P2");
                    Debug.Log("foudn p2");
                    break;
                case 2:
                    cam = GameObject.FindGameObjectWithTag("P3");
                    Debug.Log("found p3");
                    break;
                case 3:
                    cam = GameObject.FindGameObjectWithTag("P4");
                    Debug.Log("found p4");
                    break;
            }

        }
        if (cam)
        {
            cam.GetComponent<SplitScreenCamera>().player = transform;
            cam.GetComponent<SplitScreenCamera>().horizontal = rightStick.x;
            cam.GetComponent<SplitScreenCamera>().vertical = rightStick.y;
            cam.GetComponent<SplitScreenCamera>().reset = camReset;
        }
        
        if (!dontMove)
        {
            safe = true;
            
            if (movement.x == 0 && movement.y == 0)
            {
                Debug.Log("case1");
                anim.SetBool("isRun", false);
                anim.SetBool("isWalk", false);
                anim.SetBool("isIdle", true);
            }
            else if (isRunning)
            {
                Debug.Log("case2");
                currentSpeed = runSpeed;
                //currentSpeed = runSpeed * Mathf.Pow(candySlowDown, candyCarried);
                anim.SetBool("isRun", true);
                anim.SetBool("isWalk", false);
                anim.SetBool("isIdle", false);
            }
            else if (!isRunning)
            {
                Debug.Log("case3");
                currentSpeed = speed;
                //currentSpeed = speed * Mathf.Pow(candySlowDown, candyCarried);
                Debug.Log("current speed is: " + currentSpeed);
                anim.SetBool("isRun", false);
                anim.SetBool("isWalk", true);
                anim.SetBool("isIdle", false);
            }

            x = movement.x * currentSpeed;
            z = movement.y * currentSpeed;
            Vector3 direction = (cam.transform.forward * z + cam.transform.right * x).normalized;
            //Vector3 direction = (transform.forward * z + transform.right * x).normalized;
            Vector3 zeroY = new Vector3(direction.x*currentSpeed, rb.velocity.y, direction.z*currentSpeed);
            //Vector3 zeroY=new Vector3(x,0,z).normalized;
            rb.velocity = zeroY;
            //rb.velocity = transform.TransformDirection(new Vector3(x * s, 0, z * s));
            Vector3 smoothY = new Vector3(direction.x * currentSpeed, 0, direction.z * currentSpeed);
            if (x != 0 || z != 0)
            {
                transform.rotation = Quaternion.LookRotation(smoothY, Vector3.up);
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
                    anim.SetBool("isKnock", false);
                }
            }
            //Debug.Log(gameObject.name + "jump" + jump);
            //jump related code over here!!
            if (jump && isGrounded && canJump)
            {
                
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                anim.SetBool("isJump", true);
                isGrounded = false;
            }

            //if attacked
            //safe will be changed in animation events
            if (attacked && !safe)
            {
                Debug.Log("attacked");
                //dontMove = true;
                anim.SetBool("isAttacked", true);
                int candyType = Random.Range(0, 3);
                switch (playerInput.playerIndex)
                {
                    case 0:
                        if (DataHolder.p1 > 0)
                        {
                            DataHolder.p1 -= 1;
                            string candyName = "Candy" + (candyType + 1);
                            float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
                            Instantiate(Resources.Load(candyName), transform.position, Quaternion.Euler(0, randomEulerY, 0));
                        }

                        break;
                    case 1:
                        if (DataHolder.p2 > 0)
                        {
                            DataHolder.p2 -= 1;
                            string candyName = "Candy" + (candyType + 1);
                            float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
                            Instantiate(Resources.Load(candyName), transform.position, Quaternion.Euler(0, randomEulerY, 0));
                        }
                        break;
                    case 2:
                        if (DataHolder.p3 > 0)
                        {
                            DataHolder.p3 -= 1;
                            string candyName = "Candy" + (candyType + 1);
                            float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
                            Instantiate(Resources.Load(candyName), transform.position, Quaternion.Euler(0, randomEulerY, 0));
                        }

                        break;
                    case 3:
                        if (DataHolder.p4 > 0)
                        {
                            DataHolder.p4 -= 1;
                            string candyName = "Candy" + (candyType + 1);
                            float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
                            Instantiate(Resources.Load(candyName), transform.position, Quaternion.Euler(0, randomEulerY, 0));
                        }

                        break;
                }
                dontMove = true;
            }
        }
        else
        {
            anim.SetBool("isBumped", false);
            anim.SetBool("isAttacked", false);
            t += Time.deltaTime;
            safe = false;
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

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 30);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.y - collision.gameObject.transform.position.y > .5f)
            {
                Debug.Log("jumped");
                //jump ass attack
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                rb.AddForce(transform.forward * jumpForce * .5f, ForceMode.Impulse);
                //when copy this code to actual guys, change this to the name of that script
                collision.gameObject.GetComponent<TestBuddiesController>().attacked = true;
            }
            else
            {
                //Debug.Log("bumped");
                //knock off your friend and lose candy!
                if (isRunning)
                {
                    //check if this & the other player isRunning
                    if (collision.gameObject.GetComponent<SplitScreenPlayerController>().isRunning)
                    {
                        anim.SetBool("isBumped", true);
                        int candyType = Random.Range(0, 3);
                        switch (playerInput.playerIndex)
                        {
                            case 0:
                                if (DataHolder.p1 > 0)
                                {
                                    DataHolder.p1 -= 1;
                                    string candyName = "Candy" + (candyType + 1);
                                    float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
                                    Instantiate(Resources.Load(candyName), transform.position, Quaternion.Euler(0, randomEulerY, 0));
                                }

                                break;
                            case 1:
                                if (DataHolder.p2 > 0)
                                {
                                    DataHolder.p2 -= 1;
                                    string candyName = "Candy" + (candyType + 1);
                                    float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
                                    Instantiate(Resources.Load(candyName), transform.position, Quaternion.Euler(0, randomEulerY, 0));
                                }
                                break;
                            case 2:
                                if (DataHolder.p3 > 0)
                                {
                                    DataHolder.p3 -= 1;
                                    string candyName = "Candy" + (candyType + 1);
                                    float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
                                    Instantiate(Resources.Load(candyName), transform.position, Quaternion.Euler(0, randomEulerY, 0));
                                }

                                break;
                            case 3:
                                if (DataHolder.p4 > 0)
                                {
                                    DataHolder.p4 -= 1;
                                    string candyName = "Candy" + (candyType + 1);
                                    float randomEulerY = Random.Range(transform.eulerAngles.y - 90, transform.eulerAngles.y + 90);
                                    Instantiate(Resources.Load(candyName), transform.position, Quaternion.Euler(0, randomEulerY, 0));
                                }

                                break;
                        }
                        dontMove = true;

                    }
                }
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canJump = false;
            jump = false;
            anim.SetBool("isJump", false);
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
            KnockOnDoors(other.gameObject.GetComponent<HouseManager>().candyAmount, other.gameObject.GetComponent<HouseManager>().rTimes, other.gameObject.GetComponent<HouseManager>().CanCandy, other.gameObject);
        }

        if (other.CompareTag("ATM"))
        {
            if (knocking && !saved)
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

    void KnockOnDoors(int candyAmount, int[] rTimes, bool can, GameObject house)
    {
        //bool doing;
        if (can)
        {
            
            if (knocking)
            {
                //Debug.Log("isKnock");
                anim.SetBool("isKnock", true);
                dontMove = true;
                doing = true;
            }
            if (!dontMove)
            {
                Debug.Log("trying hard");
                //anim.SetBool("isKnock", true);
                if (!house.GetComponent<HouseManager>().stopGiving)
                {
                    if (doing && !knocked)
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
                        house.GetComponent<HouseManager>().stopGiving = true;
                        doing = false;
                        knocked = true;
                        Debug.Log("called knocking");
                    }

                }
            }

        }
    }


    void Safe()
    {
        safe = true;
    }
    void Unsafe()
    {
        safe = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        //Debug.Log(gameObject.name + " actual movement:" + movement);
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
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jump = true;
            canJump = true;
        }
        //Debug.Log(gameObject.name + " pressed " + context.action.triggered);
    }

}
