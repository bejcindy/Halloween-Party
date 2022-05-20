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

    public GameObject dizzy, sweat, dust, knockEffect;

    int phase2Placement;

    bool L1, R1;
    string previousScene;

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

        phase2Placement = 0;

        dizzy.SetActive(false);
        sweat.SetActive(false);
        dust.SetActive(false);
        knockEffect.SetActive(false);

        //changing this to 4 because particle effects are set as childern
        if (transform.childCount == 4)
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
        //sceneNum = SceneManager.GetActiveScene().buildIndex;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Title Scene":
                if (playerInput.currentActionMap != playerInput.actions.FindActionMap("UI"))
                {
                    playerInput.SwitchCurrentActionMap("UI");
                }
                //playerInput.SwitchCurrentActionMap("UI");
                if (change)
                {
                    SceneManager.LoadScene("intro1");
                    DataHolder.reset = true;
                    //playerInput.SwitchCurrentActionMap("Player");
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

            case "intro1":
                //tutorial select scene
                if (playerInput.currentActionMap != playerInput.actions.FindActionMap("UI"))
                {
                    playerInput.SwitchCurrentActionMap("UI");
                }
                if (playerInput.playerIndex == 0)
                {
                    GameObject.FindGameObjectWithTag("Zhu").GetComponent<Intro1Manager>().L1 = L1;
                    GameObject.FindGameObjectWithTag("Zhu").GetComponent<Intro1Manager>().R1 = R1;
                }
                previousScene = "intro1";
                break;

            case "Phase 1 Intro":
                //change later
                if (playerInput.currentActionMap != playerInput.actions.FindActionMap("UI"))
                {
                    playerInput.SwitchCurrentActionMap("UI");
                }
                if(previousScene!="Phase 1 Intro")
                {
                    born = false;
                    previousScene = "Phase 1 Intro";
                }
                if (!born)
                {
                    initialPos = GameObject.FindGameObjectWithTag(startPosName).transform;
                    transform.position = initialPos.position;
                    transform.rotation = initialPos.rotation;
                    born = true;
                }
                if (playerInput.playerIndex == 0)
                {
                    if (!GameObject.FindGameObjectWithTag("Zhu").GetComponent<Phase1IntroManager>().moveL)
                    {
                        GameObject.FindGameObjectWithTag("Zhu").GetComponent<Phase1IntroManager>().moveL = L1;
                    }
                    if (!GameObject.FindGameObjectWithTag("Zhu").GetComponent<Phase1IntroManager>().move)
                    {
                        GameObject.FindGameObjectWithTag("Zhu").GetComponent<Phase1IntroManager>().move = R1;
                    }
                    
                }
                break;

            case "Phase 2 Intro":
                if (playerInput.currentActionMap != playerInput.actions.FindActionMap("UI"))
                {
                    playerInput.SwitchCurrentActionMap("UI");
                }
                break;

            case "LIL Level 1":
                //playerInput.SwitchCurrentActionMap("Player");
                if (change)
                {
                    SceneManager.LoadScene("Phase 2");
                    born = false;
                    //playerInput.SwitchCurrentActionMap("UI");
                    DataHolder.reset = true;
                    change = false;
                }
                if (!born)
                {
                    if (playerInput.currentActionMap != playerInput.actions.FindActionMap("Player"))
                    {
                        playerInput.SwitchCurrentActionMap("Player");
                    }

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
            case "LIL Level 2":
                //playerInput.SwitchCurrentActionMap("Player");
                if (change)
                {
                    SceneManager.LoadScene("Phase 2");
                    born = false;
                    //playerInput.SwitchCurrentActionMap("UI");
                    DataHolder.reset = true;
                    change = false;
                }
                if (!born)
                {
                    if (playerInput.currentActionMap != playerInput.actions.FindActionMap("Player"))
                    {
                        playerInput.SwitchCurrentActionMap("Player");
                    }
                    nextLevel = 3;
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
            case "LIL Level 3":
                //playerInput.SwitchCurrentActionMap("Player");
                if (change)
                {
                    SceneManager.LoadScene("Phase 1 Intro");
                    born = false;
                    //playerInput.SwitchCurrentActionMap("UI");
                    DataHolder.reset = true;
                    change = false;
                }
                if (!born)
                {
                    if (playerInput.currentActionMap != playerInput.actions.FindActionMap("Player"))
                    {
                        playerInput.SwitchCurrentActionMap("Player");
                    }
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
            case "Phase 2":
                phase1script.enabled = false;
                transform.localScale = new Vector3(.5f, .5f, .5f);
                if (change)
                {
                    SceneManager.LoadScene(nextLevel);
                    //playerInput.SwitchCurrentActionMap("Player");
                    born = false;
                    DataHolder.reset = true;
                    change = false;
                }

                if (playerInput.currentActionMap != playerInput.actions.FindActionMap("UI"))
                {
                    playerInput.SwitchCurrentActionMap("UI");
                }
                rb.useGravity = false;
                transform.GetChild(0).gameObject.SetActive(false);
                if (Phase2Manager.timeUp)
                {
                    if (placement && !confirm.gameObject.activeSelf)
                    {
                        if (v == Vector2.up)
                        {
                            placement.text = "1";
                            phase1script.isTopPhase2 = true;
                        }
                        if (v == Vector2.left)
                        {
                            placement.text = "2";
                            phase1script.isTopPhase2 = false;
                        }
                        if (v == Vector2.down)
                        {
                            placement.text = "3";
                            phase1script.isTopPhase2 = false;
                        }
                        if (v == Vector2.right)
                        {
                            placement.text = "4";
                            phase1script.isTopPhase2 = false;
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
    public void OnL1(InputAction.CallbackContext context)
    {
        //if (context.performed)
        //{
        //    L1 = true;
        //}
        //if (context.canceled)
        //{
        //    L1 = false;
        //}
        L1 = context.action.triggered;
        //Debug.Log("L1 is " + L1);
    }
    public void OnR1(InputAction.CallbackContext context)
    {
        //if (context.canceled)
        //{
        //    R1 = false;
        //    Debug.Log("I'm also called");
        //}
        //else if (context.started)
        //{
        //    R1 = true;
        //    Debug.Log("I'm called");
        //}
        R1 = context.action.triggered;
        //R1 = context.performed;
        //Debug.Log(gameObject.name+" R1 is " + R1);
    }

}
