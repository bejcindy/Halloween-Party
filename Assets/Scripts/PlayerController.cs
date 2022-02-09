using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerNumber {Player1,Player2,Player3,Player4};
    [SerializeField]
    PlayerNumber player;

    public float speed;

    int playerN;
    float runSpeed;
    Rigidbody rb;
    string horizontal, vertical;
    float x, z, s;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        runSpeed = speed * 2;
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
        //float x, z, s;
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
        rb.velocity = new Vector3(x, 0, z);
        Vector3 direction = new Vector3(x, 0, z).normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
        
    }
}
