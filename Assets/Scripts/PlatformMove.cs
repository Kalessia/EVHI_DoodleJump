using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour                // Make a platform move horryzontally 
{
    public Camera cam;
    public float speed = 1;
    public float offset = 0;
    private Vector3 targetLeft;
    private Vector3 targetRight;
    private bool leftTarget = true;

    void Start()
    {
        setUpCam();
    }

    void Update()
    {
        if(cam == null)
        {
            setUpCam();
        }
        
        if (leftTarget)
        {
            if(transform.position.x - targetLeft.x < 0.01)
            {
                leftTarget = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetLeft, speed * Time.deltaTime);                            // Go to the rigth
        }
        else
        {
            if (targetRight.x - transform.position.x < 0.01)
            {
                leftTarget = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetRight, speed * Time.deltaTime);                           // Go to the left
        }
    }

    void setUpCam()                                  // Register the boundaries of the screen and define from where to where the platform will move
    {
        float cam_width = cam.orthographicSize * cam.aspect;
        targetLeft = new Vector3(cam.transform.position.x - cam_width + offset, transform.position.y, transform.position.z);
        targetRight = new Vector3(cam.transform.position.x + cam_width - offset, transform.position.y, transform.position.z);
    }
}
