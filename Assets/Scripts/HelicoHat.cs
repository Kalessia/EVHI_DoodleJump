using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicoHat : MonoBehaviour
{
    public float height = 30;
    public float speed = 10;
    private float startPosition;
    private bool taken;
    private GameObject player;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.y;
        taken = false;
    }

    private void Update()
    {
        if (taken)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + speed * Time.deltaTime, player.transform.position.z);
            if (player.transform.position.y > startPosition + height)
            {
                Debug.Log(player.transform.position.y);
                Debug.Log(startPosition);
                //player.GetComponent<Rigidbody2D>().mass = 1.0f;
                player.GetComponent<Jump>().enabled = true;
                player.GetComponent<Rigidbody2D>().simulated = true;
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 50));
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("PlayerSecondCollider"))
        {
            if (taken == false)
            {
                Debug.Log("taken HelicoHat");
                //GameObject player = collision.GetComponent<FollowPlayer>().Player;
                GetComponent<FollowPlayer>().enabled = true;
                
                GetComponent<Animator>().SetTrigger("HelicoHatCondition");
                player = GetComponent<FollowPlayer>().Player;
                //player.GetComponent<Rigidbody2D>().mass = 0.0f;
                //player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                player.GetComponent<Rigidbody2D>().simulated = false;
                player.GetComponent<Jump>().enabled = false;
                startPosition = transform.position.y;
                SetTaken();
                //player.GetComponent<BoxCollider2D>().enabled = false;
                //player.transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);

                //if (player.transform.position.y > startPosition + height)
                //{
                //Destroy(this.gameObject);
                //}
            }
        }
    }

    public void SetTaken()
    {
        taken = true;
    }
}
