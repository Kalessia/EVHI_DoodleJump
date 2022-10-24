using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{

    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flip();
    }

    private void Flip()
    {
        if (rb.velocity.x == 0.0f)
        {
            Debug.Log("face");
        } else if (rb.velocity.x > 0.0f)
        {
            Debug.Log(rb.velocity.x);
            spriteRenderer.flipX = false;
        } else
        {
            Debug.Log("left");
            spriteRenderer.flipX = true;
            Debug.Log(rb.velocity.x);
        }
    }

}
