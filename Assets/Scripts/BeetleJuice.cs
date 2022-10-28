using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleJuice : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundMonster;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("PlayerSecondCollider"))
        {
            audioSource.PlayOneShot(soundMonster);

            GameObject player = collision.GetComponent<FollowPlayer>().Player;
            player.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(collision.gameObject);
            
            GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
            ManagerGame mg = gm.GetComponent<ManagerGame>();
            mg.SetLose();
        }     
    }
}
