using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlatformBreak : MonoBehaviour                 // Make the plateform fall after it get broken by the player
{
    private bool broken;
    public float speed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        broken = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (broken)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        }
    }

    public void SetBroken()                   // Called in the Jump script
    {
        broken = true;
    }
}
