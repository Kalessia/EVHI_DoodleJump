using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour                 // Script for autamatic behaviors on the player
{
    public float jump_force;
    public float spring_force;
    private Rigidbody2D rb;
    private Vector2 jump;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip soundJumpSimple;
    public AudioClip soundJumpSpring;
    public AudioClip soundJumpMonster;
    public AudioClip soundJumpFakePlatform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = new Vector2(0, 1);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if ((rb.velocity.y < 0) && (!(collision.gameObject.CompareTag("BlackHole")))        // Only when th player is going down after a jump
            && (collision.gameObject.CompareTag("PlayerSecondCollider") == false))
        {
            // Fake plateform
            if (collision.gameObject.CompareTag("FakePlatform"))
            {
                audioSource.PlayOneShot(soundJumpFakePlatform);
                collision.gameObject.GetComponent<Animator>().SetTrigger("PlatformBreak");
                collision.gameObject.GetComponent<FakePlatformBreak>().SetBroken();  
                return;
            }
            // Spring
            if (collision.gameObject.CompareTag("Spring"))
            {
                audioSource.PlayOneShot(soundJumpSpring);
                rb.velocity = Vector2.zero;
                rb.AddForce(jump * jump_force * spring_force);
                if (animator != null)
                {
                    animator.SetTrigger("JumpCondition");
                    collision.gameObject.GetComponent<Animator>().SetTrigger("SpringBounce");
                }
                
                return;
            }
            // Monster
            if (collision.gameObject.CompareTag("Monster"))
            {
                audioSource.PlayOneShot(soundJumpMonster);
                Destroy(collision.gameObject);
            }

            // Normal jump behavior
            audioSource.PlayOneShot(soundJumpSimple);
            rb.velocity = Vector2.zero;
            rb.AddForce(jump * jump_force);
            if (animator != null)
            {
                animator.SetTrigger("JumpCondition");
            }

        }
    }
}
