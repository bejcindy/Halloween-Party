using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Phase1UIManager : MonoBehaviour
{
    public Text p1txt, p2txt, p3txt, p4txt;
    public Slider p1s, p2s, p3s, p4s;
    public Text Timer;
    public Image p1Stemina, p2Stemina, p3Stemina, p4Stemina;
    float t = 120;
    float otherT = 2;

    GameObject p1cam, p2cam, p3cam, p4cam;

    Transform p1, p2, p3, p4;

    bool p1ste, p2ste, p3ste, p4ste;

    // Start is called before the first frame update
    void Start()
    {
        p1cam = GameObject.FindGameObjectWithTag("P1");
        p2cam = GameObject.FindGameObjectWithTag("P2");
        p3cam = GameObject.FindGameObjectWithTag("P3");
        p4cam = GameObject.FindGameObjectWithTag("P4");

        //p1 = p1cam.GetComponent<SplitScreenCamera>().player;
        //p2 = p2cam.GetComponent<SplitScreenCamera>().player;
        //p3 = p3cam.GetComponent<SplitScreenCamera>().player;
        //p4 = p4cam.GetComponent<SplitScreenCamera>().player;
        p1ste = false;
        p2ste = false;
        p3ste = false;
        p4ste = false;
    }

    private void LateUpdate()
    {
        if (!p1&& p1cam.GetComponent<SplitScreenCamera>().player!=null)
        {
            p1 = p1cam.GetComponent<SplitScreenCamera>().player;
        }
        if (!p2 && p2cam.GetComponent<SplitScreenCamera>().player != null)
        {
            p2 = p2cam.GetComponent<SplitScreenCamera>().player;
        }
        if (!p3 && p3cam.GetComponent<SplitScreenCamera>().player != null)
        {
            p3 = p3cam.GetComponent<SplitScreenCamera>().player;
        }
        if (!p4 && p4cam.GetComponent<SplitScreenCamera>().player != null)
        {
            p4 = p4cam.GetComponent<SplitScreenCamera>().player;
        }

        if (p1)
        {
            //Debug.Log("foudn p1");
            //change fill to stemina
            float fillValue = Mathf.Clamp(DataHolder.p1Stem, 0, 1);
            p1Stemina.fillAmount = fillValue;
            if (fillValue >= 1)
            {
                p1Stemina.gameObject.SetActive(false);
            }
            else
            {
                p1Stemina.gameObject.SetActive(true);
            }
            Vector3 stemPos = p1cam.GetComponent<Camera>().WorldToScreenPoint(p1.position);
            if (!p1ste)
            {
                p1Stemina.transform.position = stemPos;
                p1ste = true;
            }
            else
            {
                p1Stemina.transform.position = Vector2.Lerp(p1Stemina.transform.position, stemPos, 5f * Time.deltaTime);
            }
        }
        if (p2)
        {
            //Debug.Log("foudn p1");
            //change fill to stemina
            float fillValue = Mathf.Clamp(DataHolder.p2Stem, 0, 1);
            p2Stemina.fillAmount = fillValue;
            if (fillValue >= 1)
            {
                p2Stemina.gameObject.SetActive(false);
            }
            else
            {
                p2Stemina.gameObject.SetActive(true);
            }
            Vector3 stemPos = p2cam.GetComponent<Camera>().WorldToScreenPoint(p2.position);
            if (!p2ste)
            {
                p2Stemina.transform.position = stemPos;
                p2ste = true;
            }
            else
            {
                p2Stemina.transform.position = Vector2.Lerp(p2Stemina.transform.position, stemPos, 5f * Time.deltaTime);
            }
        }
        if (p3)
        {
            //Debug.Log("foudn p1");
            //change fill to stemina
            float fillValue = Mathf.Clamp(DataHolder.p3Stem, 0, 1);
            p3Stemina.fillAmount = fillValue;
            if (fillValue >= 1)
            {
                p3Stemina.gameObject.SetActive(false);
            }
            else
            {
                p3Stemina.gameObject.SetActive(true);
            }
            Vector3 stemPos = p3cam.GetComponent<Camera>().WorldToScreenPoint(p3.position);
            if (!p3ste)
            {
                p3Stemina.transform.position = stemPos;
                p3ste = true;
            }
            else
            {
                p3Stemina.transform.position = Vector2.Lerp(p3Stemina.transform.position, stemPos, 5f * Time.deltaTime);
            }
        }
        if (p4)
        {
            //Debug.Log("foudn p1");
            //change fill to stemina
            float fillValue = Mathf.Clamp(DataHolder.p4Stem, 0, 1);
            p4Stemina.fillAmount = fillValue;
            if (fillValue >= 1)
            {
                p4Stemina.gameObject.SetActive(false);
            }
            else
            {
                p4Stemina.gameObject.SetActive(true);
            }
            Vector3 stemPos = p4cam.GetComponent<Camera>().WorldToScreenPoint(p4.position);
            if (!p4ste)
            {
                p4Stemina.transform.position = stemPos;
                p4ste = true;
            }
            else
            {
                p4Stemina.transform.position = Vector2.Lerp(p4Stemina.transform.position, stemPos, 5f * Time.deltaTime);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (!p1)
        //{
        //    p1 = p1cam.GetComponent<SplitScreenCamera>().player;
        //}
        
        //if (p1)
        //{
        //    Debug.Log("foudn p1");
        //    //change fill to stemina
        //    float fillValue = Mathf.Clamp(DataHolder.p1Stem, 0, 1);
        //    p1Stemina.fillAmount = fillValue;
        //    if (fillValue >= 1)
        //    {
        //        p1Stemina.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        p1Stemina.gameObject.SetActive(true);
        //    }
        //    Vector3 stemPos = p1cam.GetComponent<Camera>().WorldToScreenPoint(p1.position);
        //    if (!p1ste)
        //    {
        //        p1Stemina.transform.position = stemPos;
        //        p1ste = true;
        //    }
        //    else
        //    {
        //        p1Stemina.transform.position = Vector2.Lerp(p1Stemina.transform.position, stemPos, 5f * Time.deltaTime);
        //    }
        //}


        if (p1txt)
        {
            p1txt.text = "Candy Carrying: " + DataHolder.p1 + "\n" + "Candy Stored: " + DataHolder.p1ATM;
            p1s.value = DataHolder.p1ATM;
        }
        if (p2txt)
        {
            p2txt.text = "Candy Carrying: " + DataHolder.p2 + "\n" + "Candy Stored: " + DataHolder.p2ATM;
            p2s.value = DataHolder.p2ATM;
        }
        if (p3txt)
        {
            p3txt.text = "Candy Carrying: " + DataHolder.p3 + "\n" + "Candy Stored: " + DataHolder.p3ATM;
            p3s.value = DataHolder.p3ATM;
        }
        if (p4txt)
        {
            p4txt.text = "Candy Carrying: " + DataHolder.p4 + "\n" + "Candy Stored: " + DataHolder.p4ATM;
            p4s.value = DataHolder.p4ATM;
        }
        if (Timer)
        {
            if (t > 0)
            {
                t -= Time.deltaTime;
            }
            else
            {
                t = 0;
                otherT -= Time.deltaTime;
                if (otherT <= 0)
                {
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    SceneManager.LoadScene(4);
                }
            }
            Timer.text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(t / 60), Mathf.FloorToInt(t % 60));
        }

    }
}
