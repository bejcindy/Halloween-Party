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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        runSpeed = speed * 2;
        switch (player)
        {
            case PlayerNumber.Player1:
                playerN = 1;
                break;
            case PlayerNumber.Player2:
                playerN = 2;
                break;
            case PlayerNumber.Player3:
                playerN = 3;
                break;
            case PlayerNumber.Player4:
                playerN = 4;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x, z, s;
        if(Input.GetKey(KeyCode.Joystick1Button1))
        {
            s = runSpeed;
        }
        else
        {
            s = speed;
        }
        x = Input.GetAxis("Horizontal") * s;
        z = Input.GetAxis("Vertical") * s;
        rb.velocity = new Vector3(x, 0, z);
        Vector3 direction = new Vector3(x, 0, z).normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
        
    }
}
