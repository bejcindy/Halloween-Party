using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenCamera : MonoBehaviour
{
    public float t=5f;
    public Transform player;

    public Vector3 offset;

    Vector3 originalOffestY;
    Vector3 originalOffset;
    public float horizontal;
    public float vertical;
    public bool reset;
    public float camSpeed;
    public float rotSpeed;
    public float regularRSpeed;
    public bool jumping;

    public Vector3 previousPos;

    bool stop;
    bool toohigh;

    // Start is called before the first frame update
    void Start()
    {
        
        originalOffestY= new Vector3(0, offset.y, 0);
        originalOffset = offset;
        if (player)
        {
            transform.position = player.position + offset;
            offset = transform.position - player.transform.position;
        }
        //transform.position = player.position + offset;
        stop = false;
        toohigh = false;
        //offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player)
        {
            return;
        }
        if (transform.position.y >= 1 && transform.position.y <= 4)
        {
            Vector3 destination = player.position + offset;
            transform.position = new Vector3(destination.x, Mathf.Lerp(transform.position.y, destination.y, Time.deltaTime * camSpeed), destination.z);
            stop = false;
            toohigh = false;
        }
        else if (transform.position.y < 1)
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
            offset = originalOffset;
            //offset = transform.position - player.transform.position;

            //difference between camera & player on x & z axis
            //offset
            //player's direction - difference

            //Debug.Log("reseted to "+(transform.position-player.transform.position));
        }
        else
        {
            offset = Quaternion.AngleAxis(horizontal * 5, Vector3.up) * offset;
            //if (!stop && !toohigh)
            //{
            //    offset += new Vector3(0, vertical * camSpeed, 0);
            //}
            //else if (stop)
            //{
            //    if (vertical > 0)
            //    {
            //        offset += new Vector3(0, vertical * camSpeed, 0);
            //    }
            //}
            //else if (toohigh)
            //{
            //    if (vertical < 0)
            //    {
            //        offset += new Vector3(0, vertical * camSpeed, 0);
            //    }
            //}

        }

        //transform.position = player.position + offset;
        //Vector3 slowLook = new Vector3(Mathf.Lerp(transform.position.x, player.position.x + originalOffset.x, t), Mathf.Lerp(transform.position.y, player.position.y + originalOffset.y, t), Mathf.Lerp(transform.position.z, player.position.z + originalOffset.z, t));
        //Debug.Log(slowLook);
        //transform.rotation = Quaternion.LookRotation(slowLook);
        if (!jumping)
        {
            //transform.LookAt(player.position + originalOffestY);
            Vector3 relativePos = player.position + originalOffestY - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            if (horizontal == 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
            }
            else
            {
                transform.LookAt(player.position + originalOffestY);
            }
            //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
        }else if(jumping && horizontal != 0)
        {
            Vector3 prevY = new Vector3(player.position.x, previousPos.y, player.position.z);
            transform.LookAt(prevY + originalOffestY);
        }
        //transform.LookAt(player.position + originalOffestY);
        //Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        //Debug.Log(rotation);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * t);
    }
}
