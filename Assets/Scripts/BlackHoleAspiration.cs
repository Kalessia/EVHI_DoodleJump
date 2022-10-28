using UnityEngine;

public class BlackHoleAspiration : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundBlackHole;
    public GameObject audioManager;
    public GameObject player;


    // BlackHole
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerSecondCollider") || collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(soundBlackHole);
            audioManager.GetComponent<AudioSource>().enabled = false;

            GameObject player = collision.GetComponent<FollowPlayer>().Player;
            Destroy(player.gameObject, 0.5f);
            Destroy(collision.gameObject);
            //Debug.Log("BlackHole");
            GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
            ManagerGame mg = gm.GetComponent<ManagerGame>();
            mg.SetLose();
        }
    } 
}
