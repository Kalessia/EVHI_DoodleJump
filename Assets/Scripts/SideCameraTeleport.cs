using UnityEngine;

public class SideCameraTeleport : MonoBehaviour              // Teleport the player to the other side of the screen (horrizontally) if he get out of it
{

    public Camera cam;
    private float cam_width;

    void Start()
    {
        cam_width = cam.orthographicSize * cam.aspect;                       // Set up the width of the screen
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
