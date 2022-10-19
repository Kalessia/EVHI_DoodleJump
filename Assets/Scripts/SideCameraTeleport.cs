using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCameraTeleport : MonoBehaviour
{

    public Camera cam;
    private float cam_width;

    // Start is called before the first frame update
    void Start()
    {
        cam_width = cam.orthographicSize * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.position.x > cam.transform.position.x + cam_width)
        {
            transform.position = new Vector3 (cam.transform.position.x - cam_width + 0.01f, transform.position.y, transform.position.z);
        }
       if(transform.position.x < cam.transform.position.x - cam_width)
        {
            Debug.Log("sortie gauche");
            transform.position = new Vector3(cam.transform.position.x + cam_width - 0.01f, transform.position.y, transform.position.z);
        }
    }
}
