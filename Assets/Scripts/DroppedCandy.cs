using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCandy : MonoBehaviour
{
    public int TypeOfCandy;

    Rigidbody rb;

    float horizontalForce = 200;
    float verticalForce = 300;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * horizontalForce);
        rb.AddForce(transform.up * verticalForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
