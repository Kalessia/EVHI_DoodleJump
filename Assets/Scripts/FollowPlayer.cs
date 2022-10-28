using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;

    public float offsetY = 0;
    public float offsetX = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            transform.position = new Vector3 (Player.transform.position.x + offsetX, Player.transform.position.y + offsetY, Player.transform.position.z);
        }
    }
}
