using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame: MonoBehaviour
{

    public GameObject player;
    public Camera cam;
    public float offset = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            FallLose(); // Détecter si le joueur est sorti par le bas de la caméra
        }

        LevelCleaner(); //Retirer les éléments du niveau qui sortent par le bas de la caméra
    }

    
    void FallLose()
    {
        if (cam.transform.position.y - cam.orthographicSize - offset > player.transform.position.y)
        {
            Debug.Log("Perdu");
        }
    }


    void LevelCleaner()
    {
        var platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject i in platforms)
        {
            if (i.transform.position.y < cam.transform.position.y - cam.orthographicSize - offset)
            {
                Destroy(i);
            }
        }

        platforms = GameObject.FindGameObjectsWithTag("Spring");
        foreach (GameObject i in platforms)
        {
            if (i.transform.position.y < cam.transform.position.y - cam.orthographicSize - offset)
            {
                Destroy(i);
            }
        }

        platforms = GameObject.FindGameObjectsWithTag("BlackHole");
        foreach (GameObject i in platforms)
        {
            if (i.transform.position.y < cam.transform.position.y - cam.orthographicSize - offset)
            {
                Destroy(i);
            }
        }
    }
}
