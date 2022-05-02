using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStuff : MonoBehaviour
{
    Animator a;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
    }

    void CloseDoor()
    {
        a.SetBool("open", false);
        Debug.Log("called");
    }
}
