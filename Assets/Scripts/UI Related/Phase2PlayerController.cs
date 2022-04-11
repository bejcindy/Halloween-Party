using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Phase2PlayerController : MonoBehaviour
{
    Text p1,p2,p3,p4;
    public Text placement;
    public Text confirm;

    PlayerInput playerInput;

    int PlayerNum;

    Vector2 v;
    bool c;
    bool b;
    bool changed;

    //public int playerIndex { get; }
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        PlayerNum = playerInput.playerIndex + 1;
        
        //Debug.Log(PlayerNum);
        //p1 = GameObject.FindGameObjectWithTag("P1").GetComponent<Text>();
        //p2 = GameObject.FindGameObjectWithTag("P2").GetComponent<Text>();
        //p3 = GameObject.FindGameObjectWithTag("P3").GetComponent<Text>();
        //p4 = GameObject.FindGameObjectWithTag("P4").GetComponent<Text>();
    }

    


    // Update is called once per frame
    void Update()
    {
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

    }
    //public void Up()
    //{
    //    placement.text = "1";
    //}

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

    
}
