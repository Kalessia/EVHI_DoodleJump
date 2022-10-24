using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{

    public Camera cam;
    public float speed = 1;
    public float offset = 0;
    private Vector3 targetLeft;
    private Vector3 targetRight;
    private bool leftTarget = true;

    // Start is called before the first frame update
    void Start()
    {
        setUpCam();
    }

    // Update is called once per frame
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
            transform.position = Vector3.MoveTowards(transform.position, targetLeft, speed * Time.deltaTime);
        }
        else
        {
            if (targetRight.x - transform.position.x < 0.01)
            {
                leftTarget = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetRight, speed * Time.deltaTime);
        }
    }

    void setUpCam()
    {
        float cam_width = cam.orthographicSize * cam.aspect;
        targetLeft = new Vector3(cam.transform.position.x - cam_width + offset, transform.position.y, transform.position.z);
        targetRight = new Vector3(cam.transform.position.x + cam_width - offset, transform.position.y, transform.position.z);
    }
}
