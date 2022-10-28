using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicoHat : MonoBehaviour
{
    public float height = 300;
    public float speed = 10;
    private float startPosition;
    private bool taken;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.y;
        taken = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("PlayerSecondCollider"))
        {
            if (taken)
            {
                Debug.Log("taken HelicoHat");
                GameObject player = collision.GetComponent<FollowPlayer>().Player;
                //player.GetComponent<BoxCollider2D>().enabled = false;
                player.transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);

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
