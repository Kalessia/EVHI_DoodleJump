using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1;
    private Vector2 mouvement;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public GameObject PlayerNose;
    private float cptTime;
    public int factor = 150;
    public Animator animator;
    public GameObject BallPrefab;
    public float shootForce;
    //public float velocity_max;  



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerNose.SetActive(false);
        cptTime = 0;
    }

    private void Update()
    {
        if (cptTime <= 0)
        {
            PlayerNose.SetActive(false);
        } else
        {
            cptTime = cptTime - Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0,0);
            spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("PlayerShot");
            PlayerNose.SetActive(true);
            cptTime = factor * Time.deltaTime;
            GameObject ball = Instantiate(BallPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, 3), Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shootForce); // AddForce(new Vector2(0, shootForce));
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
