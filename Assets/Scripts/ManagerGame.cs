using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ManagerGame: MonoBehaviour
{

    public GameObject player;
    public Camera cam;
    public float offset = 0;


    public float speedEnd = 1;
    public bool loose;
    private Vector3 targetEnd;
    public GameObject gameOverScreen;
    public GameObject endButtonMenu;
    public GameObject endButtonPlay;

    // Start is called before the first frame update
    void Start()
    {
        loose = false;
        endButtonMenu.SetActive(false);
        endButtonPlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            FallLose(); // Détecter si le joueur est sorti par le bas de la caméra
        }

        LevelCleaner(); //Retirer les éléments du niveau qui sortent par le bas de la caméra

        if (loose && (player != null))
        {
            endGame();
        }
    }

    
    void FallLose()
    {
        if (cam.transform.position.y - cam.orthographicSize - offset > player.transform.position.y)
        {
            if(loose == false)
            {
                targetEnd = new Vector3(cam.transform.position.x, cam.transform.position.y - cam.orthographicSize * 2, cam.transform.position.z);
                Instantiate(gameOverScreen, new Vector3(targetEnd.x, targetEnd.y, targetEnd.z + 40), Quaternion.identity);


            }
            loose = true;
        }
    }


    void endGame()
    {
        if (cam.transform.position.y - targetEnd.y > 0.01)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, targetEnd, speedEnd * Time.deltaTime);
        }
        else
        {
            endButtonMenu.SetActive(true);
            endButtonPlay.SetActive(true);
            if (cam.transform.position.y - cam.orthographicSize - offset > player.transform.position.y)
            {
                DestroyImmediate(player);
            }
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

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
