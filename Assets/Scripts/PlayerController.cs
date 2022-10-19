using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1;
    private Vector2 mouvement;
    private Rigidbody2D rb;
    //public float velocity_max;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0,0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {/*
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if(rb.velocity.x < 0)
            {
                Vector2 temp = new Vector2(0, rb.velocity.y);
                rb.velocity = temp;
            }
            mouvement = Vector2.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(rb.velocity.x < 0)
            {
                Vector2 temp = new Vector2(0, rb.velocity.y);
                rb.velocity = temp;
            }
            mouvement = Vector2.left;
        }
        rb.AddForce(mouvement * speed);*/
    }


}
