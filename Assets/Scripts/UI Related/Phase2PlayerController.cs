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

    //SplitScreenPlayerController phase1script;
    TestBuddiesController phase1script;
    Phase2PlayerController phase2script;

    Vector2 v;
    bool c;
    bool b;
    bool changed;

    bool change;
    Rigidbody rb;
    Collider col;

    int sceneNum;

    GameObject titleCam;
    Transform initialPos;
    string startPosName;

    bool born;
    int nextLevel;

    public GameObject dizzy, sweat, dust;

    //public int playerIndex { get; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        transform.position = new Vector3(1.3f - playerInput.playerIndex*.8f, 0, 0);
        DontDestroyOnLoad(gameObject);
        born = false;
        //phase1script = GetComponent<SplitScreenPlayerController>();
        //rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        phase1script = GetComponent<TestBuddiesController>();
        phase2script = GetComponent<Phase2PlayerController>();
        rb = transform.GetComponent<Rigidbody>();
        //phase1script.enabled = false;

        dizzy.SetActive(false);
        sweat.SetActive(false);
        dust.SetActive(false);

        //changing this to 4 because particle effects are set as childern
        if (transform.childCount == 3)
        {
            if (!DataHolder.c1Taken)
            {
                GameObject characterModel = Instantiate(Resources.Load("CowBoy1"), transform.position, Quaternion.identity) as GameObject;
                characterModel.transform.parent = transform;
                phase1script.enabled = false;
                DataHolder.c1Taken = true;
            }else if (!DataHolder.c2Taken)
            {
                GameObject characterModel = Instantiate(Resources.Load("Dino1"), transform.position, Quaternion.identity) as GameObject;
                characterModel.transform.parent = transform;
                phase1script.enabled = false;
                DataHolder.c2Taken = true;
            }
            else if (!DataHolder.c3Taken)
            {
                GameObject characterModel = Instantiate(Resources.Load("HotDog1"), transform.position, Quaternion.identity) as GameObject;
                characterModel.transform.parent = transform;
                phase1script.enabled = false;
                DataHolder.c3Taken = true;
            }
            else if (!DataHolder.c4Taken)
            {
                GameObject characterModel = Instantiate(Resources.Load("Pumpkin1"), transform.position, Quaternion.identity) as GameObject;
                characterModel.transform.parent = transform;
                phase1script.enabled = false;
                DataHolder.c4Taken = true;
            }
        }

        switch(playerInput.playerIndex)
        {
            case 0:
                startPosName = "P1Start";
                break;
            case 1:
                startPosName = "P2Start";
                break;
            case 2:
                startPosName = "P3Start";
                break;
            case 3:
                startPosName = "P4Start";
                break;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
        PlayerNum = playerInput.playerIndex + 1;
        //sceneNum = SceneManager.GetActiveScene().buildIndex;
        
    }

    // Update is called once per frame
    void Update()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        switch (sceneNum)
        {
            case 0:
                playerInput.SwitchCurrentActionMap("UI");
                if (change)
                {
                    SceneManager.LoadScene(1);
                    DataHolder.reset = true;
                    playerInput.SwitchCurrentActionMap("Player");
                    change = false;
                }
                rb.useGravity = false;
                titleCam = GameObject.FindGameObjectWithTag("MainCamera");
                transform.LookAt(titleCam.transform);
                transform.localScale = new Vector3(.5f, .5f, .5f);
                //playerInput.enabled = true;
                //if (DataHolder.c1Taken && DataHolder.c2Taken && DataHolder.c3Taken && DataHolder.c4Taken)
                if (DataHolder.c1Taken && DataHolder.c2Taken&& DataHolder.c3Taken&& DataHolder.c4Taken)
                {
                    //Debug.Log(confirm);
                    if (change)
                    {
                        DataHolder.ready = true;
                        change = false;
                    }
                }
                break;

            case 1:
                //playerInput.SwitchCurrentActionMap("Player");
                if (change)
                {
                    SceneManager.LoadScene(4);
                    //born = false;
                    playerInput.SwitchCurrentActionMap("UI");
                    DataHolder.reset = true;
                    change = false;
                }
                if (!born)
                {
                    //playerInput.enabled = false;
                    nextLevel = 2;
                    initialPos = GameObject.FindGameObjectWithTag(startPosName).transform;
                    transform.position = initialPos.position;
                    transform.rotation = initialPos.rotation;
                    phase1script.cam = null;
                    phase1script.enabled = true;
                    transform.localScale = new Vector3(1, 1, 1);
                    rb.useGravity = true;
                    //phase2script.enabled = false;
                    born = true;
                }
                break;
            case 2:
                //playerInput.SwitchCurrentActionMap("Player");
                if (change)
                {
                    SceneManager.LoadScene(4);
                    //born = false;
                    playerInput.SwitchCurrentActionMap("UI");
                    DataHolder.reset = true;
                    change = false;
                }
                if (born)
                {
                    nextLevel = 3;
                    //playerInput.enabled = false;
                    phase1script.cam = null;
                    initialPos = GameObject.FindGameObjectWithTag(startPosName).transform;
                    transform.position = initialPos.position;
                    transform.rotation = initialPos.rotation;
                    phase1script.enabled = true;
                    transform.localScale = new Vector3(1, 1, 1);
                    rb.useGravity = true;
                    born = false;
                }
                break;
            case 3:
                //playerInput.SwitchCurrentActionMap("Player");
                if (change)
                {
                    SceneManager.LoadScene(4);
                    //born = false;
                    playerInput.SwitchCurrentActionMap("UI");
                    DataHolder.reset = true;
                    change = false;
                }
                if (!born)
                {
                    nextLevel = 1;
                    //playerInput.enabled = false;
                    phase1script.cam = null;
                    initialPos = GameObject.FindGameObjectWithTag(startPosName).transform;
                    transform.position = initialPos.position;
                    transform.rotation = initialPos.rotation;
                    phase1script.enabled = true;
                    transform.localScale = new Vector3(1, 1, 1);
                    rb.useGravity = true;
                    born = true;
                }
                break;
            //phase 2 stuff, change the number according to the build indext of phase 2 scene
            case 4:
                phase1script.enabled = false;
                transform.localScale = new Vector3(.5f, .5f, .5f);
                if (change)
                {
                    SceneManager.LoadScene(nextLevel);
                    playerInput.SwitchCurrentActionMap("Player");
                    DataHolder.reset = true;
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
        Debug.Log("called");
    }

    
}
