using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenCamera : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    Vector3 originalOffestY;
    Vector3 originalOffset;
    public float horizontal;
    public float vertical;
    public bool reset;
    public float camSpeed;

    bool stop;
    bool toohigh;

    // Start is called before the first frame update
    void Start()
    {
        
        originalOffestY= new Vector3(0, offset.y, 0);
        originalOffset = offset;
        transform.position = player.position + offset;
        stop = false;
        toohigh = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 1 && transform.position.y<=4)
        {
            transform.position = player.position + offset;
            stop = false;
            toohigh = false;
        }
        else if(transform.position.y<1)
        {
            stop = true;
            transform.position = player.position + offset;
        }
        else if (transform.position.y > 4)
        {
            toohigh = true;
            transform.position = player.position + offset;
        }
        
        //transform.eulerAngles += new Vector3(0, Input.GetAxis(RHorizontal) * Time.deltaTime * DataHolder.camTurnSpeed, 0);
        if (reset)
        {
            //snap to back of character
            //offset = originalOffset;
            offset = player.transform.rotation * originalOffset;
        }
        else
        {
            offset = Quaternion.AngleAxis(horizontal * 5, Vector3.up) * offset;
            if (!stop&&!toohigh)
            {
                offset += new Vector3(0, vertical * camSpeed, 0);
            }
            else if(stop)
            {
                if (vertical > 0)
                {
                    offset += new Vector3(0, vertical * camSpeed, 0);
                }
            }else if (toohigh)
            {
                if (vertical < 0)
                {
                    offset += new Vector3(0, vertical * camSpeed, 0);
                }
            }
            
        }
        transform.LookAt(player.position+originalOffestY);
    }
}
