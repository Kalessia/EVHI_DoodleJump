using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAspiration : MonoBehaviour
{
    private GameObject player;
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
        collision.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector3 newDest = this.transform.position; // Final destination
        collision.transform.position = Vector3.Lerp(collision.transform.position, newDest, Time.deltaTime * smooth);
        Vector3 newScale = new Vector3(5,5,5); // Final dimensions
        collision.transform.localScale = Vector3.Lerp(collision.transform.localScale, newScale, Time.deltaTime * smooth);
        Destroy(collision.gameObject, 0.5f);
        Debug.Log("BlackHole");
    }
}
