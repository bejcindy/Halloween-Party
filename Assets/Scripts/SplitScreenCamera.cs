using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenCamera : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;



    public float horizontal;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
        //transform.eulerAngles += new Vector3(0, Input.GetAxis(RHorizontal) * Time.deltaTime * DataHolder.camTurnSpeed, 0);

        offset = Quaternion.AngleAxis(horizontal * 5, Vector3.up) * offset;
        transform.LookAt(player.position);
    }
}
