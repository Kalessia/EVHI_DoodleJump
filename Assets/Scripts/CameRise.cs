using UnityEngine;

public class CameRise : MonoBehaviour           // Make the camera folow a game object on the y axis (only going up)
{

    public GameObject target;                // The target to follow
    public float offset = 0;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (transform.position.y + offset < target.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, target.transform.position.y - offset, transform.position.z);
            }
        }
    }
}
