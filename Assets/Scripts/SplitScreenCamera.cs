using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenCamera : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    enum ControllerType { xbox, ps4 };
    [SerializeField]
    ControllerType controller;

    enum PlayerNumber { Player1, Player2, Player3, Player4 };
    [SerializeField]
    PlayerNumber pNum;

    string RHorizontal, RVertical;


    // Start is called before the first frame update
    void Start()
    {
        if (controller == ControllerType.xbox)
        {
            switch (pNum)
            {
                case PlayerNumber.Player1:
                    RHorizontal = "XboxRHorizontal1";
                    RVertical = "XboxRVertical1";
                    break;
                case PlayerNumber.Player2:
                    RHorizontal = "XboxRHorizontal2";
                    RVertical = "XboxRVertical2";
                    break;
                case PlayerNumber.Player3:
                    RHorizontal = "XboxRHorizontal3";
                    RVertical = "XboxRVertical3";
                    break;
                case PlayerNumber.Player4:
                    RHorizontal = "XboxRHorizontal4";
                    RVertical = "XboxRVertical4";
                    break;
            }
        }
        if (controller == ControllerType.ps4)
        {
            switch (pNum)
            {
                case PlayerNumber.Player1:
                    RHorizontal = "PS4RHorizontal1";
                    RVertical = "PS4RVertical1";
                    break;
                case PlayerNumber.Player2:
                    RHorizontal = "PS4RHorizontal2";
                    RVertical = "PS4RVertical2";
                    break;
                case PlayerNumber.Player3:
                    RHorizontal = "PS4RHorizontal3";
                    RVertical = "PS4RVertical3";
                    break;
                case PlayerNumber.Player4:
                    RHorizontal = "PS4RHorizontal4";
                    RVertical = "PS4RVertical4";
                    break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
        //transform.eulerAngles += new Vector3(0, Input.GetAxis(RHorizontal) * Time.deltaTime * DataHolder.camTurnSpeed, 0);

        offset = Quaternion.AngleAxis(Input.GetAxis(RHorizontal) * DataHolder.camTurnSpeed, Vector3.up) * offset;
        transform.LookAt(player.position);
    }
}
