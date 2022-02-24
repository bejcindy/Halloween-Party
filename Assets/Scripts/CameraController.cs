using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject[] players;

    public Vector3 offset;
    public Vector3 speed;
    public float minZoom, maxZoom;

    public float zoomLimiter;
    public float zoomSpeed;
    float smoothTime = .5f;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (players.Length == 0)
        {
            return;
        }
        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPos = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref speed, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDist() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime*zoomSpeed);
    }

    float GetGreatestDist()
    {
        Bounds b = new Bounds(players[0].transform.position, Vector3.zero);
        for(int i = 1; i < players.Length; i++)
        {
            b.Encapsulate(players[i].transform.position);
        }
        if (b.size.x > b.size.z)
        {
            return b.size.x;
        }
        else
        {
            return b.size.z;
        }
    }

    Vector3 GetCenterPoint()
    {
        if (players.Length == 1)
        {
            return players[0].transform.position;
        }
        else
        {
            Bounds b = new Bounds(players[0].transform.position, Vector3.zero);
            for(int i = 1; i < players.Length; i++)
            {
                b.Encapsulate(players[i].transform.position);
            }
            return b.center;
        }
    }
}
