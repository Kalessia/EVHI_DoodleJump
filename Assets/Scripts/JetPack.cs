using UnityEngine;

public class JetPack : MonoBehaviour              // Perform the jetpack behavior     (make the player fly up)
{
    public float height = 30;
    public float speed = 10;
    private float startPosition;
    private bool taken;
    private GameObject player;

    public AudioSource audioSource;
    public AudioClip soundJetPack;

    public Animator animator;


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
                audioSource.PlayOneShot(soundJetPack);
                player.GetComponent<SpriteRenderer>().flipX = false;
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + speed * Time.deltaTime, player.transform.position.z);
                if (player.transform.position.y > startPosition + height)
                {
                    Debug.Log(player.transform.position.y);
                    Debug.Log(startPosition);
                    player.GetComponent<Jump>().enabled = true;
                    player.GetComponent<Rigidbody2D>().simulated = true;
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200));
                    player.GetComponent<PlayerController>().haveBonus = false;
                    player.GetComponent<BoxCollider2D>().enabled = true;
                    GameObject.FindGameObjectWithTag("PlayerSecondCollider").GetComponent<BoxCollider2D>().enabled = true;
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
                player = GetComponent<FollowPlayer>().Player;

                if (player.GetComponent<PlayerController>().haveBonus == false)
                {
                    GetComponent<FollowPlayer>().enabled = true;
                    GetComponent<Animator>().SetTrigger("JetPackCondition");
                    player.GetComponent<PlayerController>().haveBonus = true;
                    player.GetComponent<Rigidbody2D>().simulated = false;
                    player.GetComponent<Jump>().enabled = false;
                    player.GetComponent<BoxCollider2D>().enabled = false;
                    GameObject.FindGameObjectWithTag("PlayerSecondCollider").GetComponent<BoxCollider2D>().enabled = false;
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