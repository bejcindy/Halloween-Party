using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCandy : MonoBehaviour
{
    public int TypeOfCandy;

    Rigidbody rb;

    float horizontalForce;
    float verticalForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        horizontalForce = Random.Range(400, 600);
        verticalForce = Random.Range(300, 500);
        rb.AddForce(transform.forward * horizontalForce);
        rb.AddForce(transform.up * verticalForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
