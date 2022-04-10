using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Button first;

    // Start is called before the first frame update
    void Start()
    {
        //first = transform.GetChild(1).GetComponent<Button>();
        //first.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        first.Select();
    }
}
