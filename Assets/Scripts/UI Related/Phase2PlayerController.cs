using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Phase2PlayerController : MonoBehaviour
{
    Text p1,p2,p3,p4;
    public Text placement;
    public Text confirm;

    PlayerInput playerInput;

    int PlayerNum;

    SplitScreenPlayerController phase1script;

    Vector2 v;
    bool c;
    bool b;
    bool changed;

    bool change;
    Rigidbody rb;
    Collider col;

    int sceneNum;

    GameObject titleCam;

    //public int playerIndex { get; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        transform.position = new Vector3(1.3f - playerInput.playerIndex*.8f, 0, 0);
        DontDestroyOnLoad(gameObject);
        phase1script = GetComponent<SplitScreenPlayerController>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        phase1script.enabled = false;
        if (transform.childCount == 0)
        {
            if (!DataHolder.c1Taken)
            {
                GameObject characterModel = Instantiate(Resources.Load("cowboy"), transform.position, Quaternion.identity) as GameObject;
                characterModel.transform.parent = transform;
                DataHolder.c1Taken = true;
            }else if (!DataHolder.c2Taken)
            {
                GameObject characterModel = Instantiate(Resources.Load("dino"), transform.position, Quaternion.identity) as GameObject;
                characterModel.transform.parent = transform;
                DataHolder.c2Taken = true;
            }
            else if (!DataHolder.c3Taken)
            {
                GameObject characterModel = Instantiate(Resources.Load("hotdof"), transform.position, Quaternion.identity) as GameObject;
                characterModel.transform.parent = transform;
                DataHolder.c3Taken = true;
            }
            else if (!DataHolder.c4Taken)
            {
                GameObject characterModel = Instantiate(Resources.Load("pump"), transform.position, Quaternion.identity) as GameObject;
                characterModel.transform.parent = transform;
                DataHolder.c4Taken = true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
        PlayerNum = playerInput.playerIndex + 1;
        //sceneNum = SceneManager.GetActiveScene().buildIndex;
        
    }

    //private void OnEnable()
    //{
    //    transform.position = new Vector3(2 - playerInput.playerIndex, 0, 0);
    //}


    // Update is called once per frame
    void Update()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        switch (sceneNum)
        {
            case 0:
                //playerInput.SwitchCurrentActionMap("UI");
                if (change)
                {
                    SceneManager.LoadScene(1);
                    change = false;
                }
                rb.useGravity = false;
                titleCam = GameObject.FindGameObjectWithTag("MainCamera");
                transform.LookAt(titleCam.transform);
                break;
            case 1:
                //playerInput.SwitchCurrentActionMap("Player");
                if (change)
                {
                    SceneManager.LoadScene(2);
                    
                    change = false;
                }
                phase1script.enabled = true;
                transform.localScale = new Vector3(3, 3, 3);
                rb.useGravity = true;
                break;
            case 2:
                //playerInput.SwitchCurrentActionMap("Player");
                //Debug.Log(change);
                phase1script.enabled = false;
                transform.localScale = new Vector3(1, 1, 1);
                if (change)
                {
                    //playerInput.actions["SwitchActionMap"].performed+=SwitchActionMap;
                    SceneManager.LoadScene(0);
                    change = false;
                }
                rb.useGravity = false;
                transform.GetChild(0).gameObject.SetActive(false);
                if (Phase2Manager.timeUp)
                {
                    if (placement)
                    {
                        if (v == Vector2.up)
                        {
                            placement.text = "1";
                        }
                        if (v == Vector2.left)
                        {
                            placement.text = "2";
                        }
                        if (v == Vector2.down)
                        {
                            placement.text = "3";
                        }
                        if (v == Vector2.right)
                        {
                            placement.text = "4";
                        }
                    }
                    if (confirm)
                    {
                        if (c)
                        {
                            if (!changed)
                            {
                                if (confirm.gameObject.activeSelf)
                                {
                                    confirm.gameObject.SetActive(false);
                                    changed = true;
                                }
                                else
                                {
                                    confirm.gameObject.SetActive(true);
                                    changed = true;
                                }
                            }
                        }
                        else
                        {
                            changed = false;
                        }

                    }
                }
                break;
        }
    }
    //public void Up()
    //{
    //    placement.text = "1";
    //}
    //playerInput.currentActionMap = playerInput.actions.FindActionMap("Player");



    public void InputPlacement(InputAction.CallbackContext context)
    {
        v = context.ReadValue<Vector2>();
        //Debug.Log(gameObject.name + " controls " + v);
        
    }
    public void OnConfirm(InputAction.CallbackContext context)
    {
        //c = context.ReadValue<bool>();
        c = context.action.triggered;
        //Debug.Log(c);
    }
    public void ChangeScene(InputAction.CallbackContext context)
    {
        change = context.action.triggered;
    }

    
}
