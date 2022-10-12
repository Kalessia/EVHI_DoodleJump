using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jump_force;
    private Rigidbody2D rb;
    private Vector2 jump;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = new Vector2(0, 1);
    }



    private void FixedUpdate() // Pour faire des tests
    {
        if (Input.GetKeyDown("space"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(jump);
            Debug.Log("saut");
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(jump * jump_force);
        Debug.Log("rebond");
    }
}
