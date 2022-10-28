using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAspiration : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundBlackHole;
    public GameObject audioManager;

    private float smooth = 2.0f;    // BlackHole scale resize smooth effect


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // BlackHole
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("PlayerSecondCollider") || collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(soundBlackHole);
            audioManager.GetComponent<AudioSource>().enabled = false;

            GameObject player = collision.GetComponent<FollowPlayer>().Player;
            player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector3 newDest = this.transform.position; // Final destination
            player.transform.position = Vector3.Lerp(player.transform.position, newDest, Time.deltaTime * smooth);
            Vector3 newScale = new Vector3(5, 5, 5); // Final dimensions
            player.transform.localScale = Vector3.Lerp(player.transform.localScale, newScale, Time.deltaTime * smooth);
            Destroy(player.gameObject, 0.5f);
            Destroy(collision.gameObject);
            //Debug.Log("BlackHole");
            GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
            ManagerGame mg = gm.GetComponent<ManagerGame>();
            mg.SetLose();
        }
    } 
}
