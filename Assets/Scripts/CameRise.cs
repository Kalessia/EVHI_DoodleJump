using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameRise : MonoBehaviour
{

    public GameObject target;
    public float offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
