using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public GameObject instruction;

    float t;

    // Start is called before the first frame update
    void Start()
    {
        instruction.SetActive(false);
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 5)
        {
            instruction.SetActive(true);
        }
    }
}
