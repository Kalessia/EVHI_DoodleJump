using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public SpriteRenderer spriteRenderer;
    public GameObject PlayerNose;
    public int factor = 150;
    public Animator animator;
    public GameObject BallPrefab;
    public float shootForce;
    public bool haveBonus = false;
    public AudioSource audioSource;
    public AudioClip soundFire;
    

    private void Update()
    {        
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && (haveBonus == false))
        {
            audioSource.PlayOneShot(soundFire);
            animator.SetTrigger("PlayerShot");
            PlayerNose.GetComponent<Animator>().SetTrigger("NoseShoot");
            GameObject ball = Instantiate(BallPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, 3), Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shootForce);
        }
    }
}
