using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class ManagerGame: MonoBehaviour
{
    // Variable used for the game to work
    public GameObject player;
    public Camera cam;
    private float cam_width;
    public float offset = 0;

    // Variables used when the player lose  
    public float speedEnd = 1;
    public bool loose;
    private Vector3 targetEnd;
    public GameObject gameOverScreen;
    public GameObject endButtonMenu;
    public GameObject endButtonPlay;

    public GameObject greenplatPrefab;
    public GameObject blueplatPrefab;
    public float maxSpeedBluePlatform = 1;
    public float minSpeedBluePlatform = 5;
    public GameObject brownplatPrefab;
    public GameObject springPrefab;
    public GameObject enemy1Prefab;
    public GameObject blackHolePrefab;

    private float difficulty;
    private GameObject lastPlatform;
    private float maxBoundaries = 5;
    private float minBoundaries = 8;

    private int score;
    public TextMeshProUGUI textScore;
    public int scoreFactor;
    private int bestScore;
    public GameObject textBestScoreObj;
    public TextMeshProUGUI textBestScore;
    public GameObject textScoreEndObj;
    public TextMeshProUGUI textScoreEnd;


    // Start is called before the first frame update
    void Start()
    {
        if (cam != null)
        {
            //cam.pixelRect = new Rect(0, 0, 640, 1024);    
            cam.transform.position = new Vector3(0, 0, 0);
            cam_width = cam.orthographicSize * cam.aspect;
            maxBoundaries = cam.orthographicSize * 2 + 1.5f;
            minBoundaries = cam.orthographicSize * 2 - 1.5f;
        }
        loose = false;
        if (endButtonMenu != null)
        {
            endButtonMenu.SetActive(false);
        }
        if (endButtonPlay != null)
        {
            endButtonPlay.SetActive(false);
        }
        difficulty = 0;
        if (player != null)
        {
            player.transform.position = new Vector3(0, 0, 1);
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, 1000));
        }
        

        if (greenplatPrefab != null)
        {
            lastPlatform = Instantiate(greenplatPrefab, new Vector3(0, -10, 2), Quaternion.identity);
        }

        score = 0;
        if(textBestScoreObj != null)
        {
            textBestScoreObj.SetActive(false);
        }
        if (textScoreEndObj != null)
        {
            textScoreEndObj.SetActive(false);
        }
        LoadScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            FallLose(); // Detect when the player goes out of the camera from the bottom and trigger the lose if so
            updateScore();
            updateDifficulty();
        }

        LevelCleaner(); // Destory the elements that goes out of the camera from the bottom

        if (loose)
        {
            endGame(); // Make the camera goes down and display the game oaver screen
        }
        else
        {
            if (player  != null)
            {
                PlatformCreator(); // Create new platforms randomly
            }
        }
    }



    void PlatformCreator()
    {
        if((lastPlatform.transform.position.y < player.transform.position.y + minBoundaries))
        {
            int nbNew = 1;
            float newVar = Random.Range(difficulty, 1.0f);
            if(newVar < 0.7f)
            {
                nbNew += 1;
            }
            if (newVar < 0.3f)
            {
                nbNew += 1;
            }
            for (int i = 0; i < nbNew; i++) {

                GameObject platformColor = greenplatPrefab;
                float speedBlue = -1.0f;
                if((blueplatPrefab != null) && (Random.Range(difficulty, 1.0f) > 0.93f))
                {
                    platformColor = blueplatPrefab;
                    speedBlue = Random.Range(minSpeedBluePlatform, maxSpeedBluePlatform - (maxSpeedBluePlatform - minSpeedBluePlatform -1) * (1 - difficulty));
                }
                lastPlatform = Instantiate(platformColor, new Vector3(Random.Range(-1.0f, 1.0f) * (cam_width - 0.6f), player.transform.position.y + Random.Range(minBoundaries + 3 * difficulty, maxBoundaries), 10), Quaternion.identity);
                if(speedBlue > -1.0f)
                {
                    PlatformMove pm = lastPlatform.GetComponent<PlatformMove>();
                    pm.speed = speedBlue;
                    pm.cam = cam;
                }
                if (springPrefab != null) {
                    if (Random.Range(0.0f, 1.0f) <= 0.1f)
                    {
                        GameObject newSpring = Instantiate(springPrefab, lastPlatform.transform, true);
                        newSpring.transform.position = lastPlatform.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0.4f, 0);
                    }
                }
                
                if (Random.Range(difficulty * 0.5f, 1.0f) > 0.95f)
                {
                    float seuilFakePlat = 0.8f;
                    float seuilEnemy = 0.5f;
                    float seuilBlackHole = 0.1f;
                    float tirage = Random.Range(0.0f, seuilFakePlat + seuilEnemy + seuilBlackHole);
                    if (tirage < seuilBlackHole)
                    {
                        if (lastPlatform.transform.position.x > 0)
                        {
                            Instantiate(blackHolePrefab, new Vector3(Random.Range(-1.0f, 0.0f) * (cam_width - 0.6f), player.transform.position.y + Random.Range(minBoundaries + 3 * difficulty, maxBoundaries), 5), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(blackHolePrefab, new Vector3(Random.Range(0.0f, 1.0f) * (cam_width - 0.6f), player.transform.position.y + Random.Range(minBoundaries + 3 * difficulty, maxBoundaries), 5), Quaternion.identity);
                        }
                    }
                    else if(tirage < seuilEnemy + seuilBlackHole) 
                    {
                        Instantiate(enemy1Prefab, new Vector3(Random.Range(-1.0f, 1.0f) * (cam_width - 0.6f), player.transform.position.y + Random.Range(minBoundaries + 3 * difficulty, maxBoundaries), 9), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(brownplatPrefab, new Vector3(Random.Range(-1.0f, 1.0f) * (cam_width - 0.6f), player.transform.position.y + Random.Range(minBoundaries + 3 * difficulty, maxBoundaries), 10), Quaternion.identity);
                    }
                }
            }
        }
    }



    void updateDifficulty()
    {
        if(player.transform.position.y < 900)
        {
            difficulty = player.transform.position.y * 0.001f;
        }
    }

    void updateScore()
    {
        if (player.transform.position.y * scoreFactor > score)
        {
            score = (int)(player.transform.position.y * scoreFactor);
            textScore.text = score.ToString();
        }
    }

    public void LoadScore()
    {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("bestScore", bestScore);
    }

    void FallLose()
    {
        if (cam.transform.position.y - cam.orthographicSize - offset > player.transform.position.y)
        {
            SetLose();
        }
    }



    public void SetLose()
    {
        if (loose == false)
        {
            targetEnd = new Vector3(cam.transform.position.x, cam.transform.position.y - cam.orthographicSize * 2, cam.transform.position.z);
            Instantiate(gameOverScreen, new Vector3(targetEnd.x, targetEnd.y, targetEnd.z + 40), Quaternion.identity);
        }
        loose = true;
    }



    void endGame()
    {
        if (cam.transform.position.y - targetEnd.y > 0.01)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, targetEnd, speedEnd * Time.deltaTime);
        }
        else
        {
            if (score > bestScore)
            {
                bestScore = score;
                SaveScore();
            }
            endButtonMenu.SetActive(true);
            endButtonPlay.SetActive(true);
            textBestScoreObj.SetActive(true);
            textBestScore.text = bestScore.ToString();
            textScoreEndObj.SetActive(true);
            textScoreEnd.text = score.ToString();


            if (player != null)
            {
                if (cam.transform.position.y - cam.orthographicSize - offset > player.transform.position.y)
                {
                    Destroy(player);
                }
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

        platforms = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject i in platforms)
        {
            if (i.transform.position.y < cam.transform.position.y - cam.orthographicSize - offset)
            {
                Destroy(i);
            }
        }

        platforms = GameObject.FindGameObjectsWithTag("FakePlatform");
        foreach (GameObject i in platforms)
        {
            if (i.transform.position.y < cam.transform.position.y - cam.orthographicSize - offset)
            {
                Destroy(i);
            }
        }

        platforms = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject i in platforms)
        {
            if (i.transform.position.y > cam.transform.position.y + cam.orthographicSize + offset)
            {
                Destroy(i);
            }
        }
    }



    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("EntryScene");
        SceneManager.UnloadSceneAsync("PlayScene");
    }
}
