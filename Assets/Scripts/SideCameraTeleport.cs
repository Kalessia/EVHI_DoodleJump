using UnityEngine;

public class SideCameraTeleport : MonoBehaviour
{

    public Camera cam;
    private float cam_width;

    void Start()
    {
        cam_width = cam.orthographicSize * cam.aspect;
    }

    void Update()
    {
       if(transform.position.x > cam.transform.position.x + cam_width)
        {
            transform.position = new Vector3 (cam.transform.position.x - cam_width + 0.01f, transform.position.y, transform.position.z);
        }
       if(transform.position.x < cam.transform.position.x - cam_width)
        {
            transform.position = new Vector3(cam.transform.position.x + cam_width - 0.01f, transform.position.y, transform.position.z);
        }
    }
}
