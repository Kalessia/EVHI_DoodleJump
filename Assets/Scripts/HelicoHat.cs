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

    public AudioSource audioSource;
    public AudioClip soundHelico;

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
            if (player == null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                audioSource.PlayOneShot(soundHelico);
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + speed * Time.deltaTime, player.transform.position.z);
                if (player.transform.position.y > startPosition + height)
                {
                    Debug.Log(player.transform.position.y);
                    Debug.Log(startPosition);
                    player.GetComponent<Jump>().enabled = true;
                    player.GetComponent<Rigidbody2D>().simulated = true;
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 50));
                    player.GetComponent<PlayerController>().haveBonus = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("PlayerSecondCollider") || collision.gameObject.CompareTag("Player"))
        {
            if (taken == false)
            {
                Debug.Log("taken HelicoHat");
                player = GetComponent<FollowPlayer>().Player;
                
                if (player.GetComponent<PlayerController>().haveBonus == false)
                {
                    GetComponent<FollowPlayer>().enabled = true;
                
                    GetComponent<Animator>().SetTrigger("HelicoHatCondition");
                    player.GetComponent<PlayerController>().haveBonus = true;
                    player.GetComponent<Rigidbody2D>().simulated = false;
                    player.GetComponent<Jump>().enabled = false;
                    startPosition = transform.position.y;
                    transform.parent.gameObject.transform.DetachChildren();
                    SetTaken();
                }
            }
        }
    }

    public void SetTaken()
    {
        taken = true;
    }
}
