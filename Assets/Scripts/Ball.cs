using UnityEngine;

public class Ball : MonoBehaviour                    // Destroy monsters that is touched by the gameobect to which this script is attached
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
