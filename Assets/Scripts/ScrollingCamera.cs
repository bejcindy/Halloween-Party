using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCamera : MonoBehaviour
{
    public Transform start, end;

    public float scrollSpeed;

    Vector3 startNoY, endNoY;

    bool hitEnd;

    // Start is called before the first frame update
    void Start()
    {
        startNoY = new Vector3(start.position.x, transform.position.y, start.position.z);
        endNoY = new Vector3(end.position.x, transform.position.y, end.position.z);
        transform.position = startNoY;
        hitEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, endNoY, scrollSpeed * Time.deltaTime);
        if (!hitEnd)
        {
            transform.position += Vector3.right * scrollSpeed * Time.deltaTime;
            if (transform.position.x > endNoY.x || transform.position.z > endNoY.z)
            {
                hitEnd = true;
            }

        }
    }
}
