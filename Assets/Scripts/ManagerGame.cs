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

    // Prefabs used to generate the level (and associated values)
    public GameObject greenplatPrefab;
    public GameObject blueplatPrefab;
    public float maxSpeedBluePlatform = 1;
    public float minSpeedBluePlatform = 5;
    public GameObject brownplatPrefab;
    public GameObject springPrefab;
    public GameObject enemy1Prefab;
    public GameObject blackHolePrefab;
    public GameObject helicoPrefab;
    public GameObject jetpackPrefab;

    private float difficulty;
    private GameObject lastPlatform;
    private float maxBoundaries = 5;
    private float minBoundaries = 8;

    // Variables and gameobjects used to count, register et display the score
    private int score;
    public TextMeshProUGUI textScore;
    public int scoreFactor;
    private int bestScore;
    public GameObject textBestScoreObj;
    public TextMeshProUGUI textBestScore;
    public GameObject textScoreEndObj;
    public TextMeshProUGUI textScoreEnd;

    // Audio
    public GameObject audioManager;
    public AudioSource audioSource;
    public AudioClip soundEndGame;

    // Start is called before the first frame update
    void Start()
    {
        if (cam != null)                                        // setup of the camera
        {  
            cam.transform.position = new Vector3(0, 0, 0);
            cam_width = cam.orthographicSize * cam.aspect;
            maxBoundaries = cam.orthographicSize * 2 + 1.5f;
            minBoundaries = cam.orthographicSize * 2 - 1.5f;
        }
        loose = false;
        if (endButtonMenu != null)                             // setup of the buton of the game over screen
        {
            endButtonMenu.SetActive(false);
        }
        if (endButtonPlay != null)
        {
            endButtonPlay.SetActive(false);
        }
        difficulty = 0;
        if (player != null)                                           // setup of the player
        {
            player.transform.position = new Vector3(0, 0, 1);
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, 1000));
        }
        

        if (greenplatPrefab != null)                                   // setup of the first platform in order to generate the level
        {
            lastPlatform = Instantiate(greenplatPrefab, new Vector3(0, -10, 2), Quaternion.identity);
        }

        score = 0;
        if(textBestScoreObj != null)                             // setup of the score display for the game over screen
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
                PlatformCreator(); // Create new platforms randomly (and obstacles)
            }
        }
    }



    void PlatformCreator()                             // Create the platforms, the obstacles, and the bonus objects
    {
        if((lastPlatform.transform.position.y < player.transform.position.y + minBoundaries))                        // Detect if it necessary to create new platform (distance betwin the last platform created and the maximum heigt the player can jump)
        {
                                                                   // PLATFORM GENERATION
            int nbNew = 1;                                                           // Number of new platforms created
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

                GameObject platformColor = greenplatPrefab;                                      // create green platform by default
                float speedBlue = -1.0f;
                if((blueplatPrefab != null) && (Random.Range(difficulty, 1.0f) > 0.93f))                // create a blue (moving) platform instead of the green (basic) one
                {
                    platformColor = blueplatPrefab;
                    speedBlue = Random.Range(minSpeedBluePlatform, maxSpeedBluePlatform - (maxSpeedBluePlatform - minSpeedBluePlatform -1) * (1 - difficulty));
                }
                lastPlatform = Instantiate(platformColor, new Vector3(Random.Range(-1.0f, 1.0f) * (cam_width - 0.6f), player.transform.position.y + Random.Range(minBoundaries + 3 * difficulty, maxBoundaries), 10), Quaternion.identity);       // Create the new platform
                if(speedBlue > -1.0f)
                {
                    PlatformMove pm = lastPlatform.GetComponent<PlatformMove>();                     // Initialisation of the caracteristics of the new blue platform (if the new platform is a blue one)
                    pm.speed = speedBlue;
                    pm.cam = cam;
                }

                                                                         // BONUS GENERATION
                bool bonusPossible = true;
                if ((springPrefab != null) && bonusPossible) {
                    if (Random.Range(0.0f, 1.0f) <= 0.1f)
                    {
                        GameObject newSpring = Instantiate(springPrefab, lastPlatform.transform, true);
                        newSpring.transform.position = lastPlatform.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0.4f, 0);
                        bonusPossible = false;
                    }
                }

                if((helicoPrefab != null) && bonusPossible)
                {
                    if (Random.Range(0.0f, 1.0f) <= 0.02f)
                    {
                        GameObject newHelico = Instantiate(helicoPrefab, lastPlatform.transform, true);
                        newHelico.GetComponent<FollowPlayer>().Player = player;
                        newHelico.transform.position = lastPlatform.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0.4f, 0);
                        bonusPossible = false;
                    }
                }
                if ((jetpackPrefab != null) && bonusPossible)
                {
                    if (Random.Range(0.0f, 1.0f) <= 0.02f)
                    {
                        GameObject newJet = Instantiate(jetpackPrefab, lastPlatform.transform, true);
                        newJet.GetComponent<FollowPlayer>().Player = player;
                        newJet.transform.position = lastPlatform.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0.2f, 0);
                        bonusPossible = false;
                    }
                }


                                                                      //  OBSTACLES GENERATION
                if (Random.Range(difficulty * 0.5f, 1.0f) > 0.90f)
                {
                    float seuilFakePlat = 0.9f;                               // probability for each type of obstacle to appear
                    float seuilEnemy = 0.4f;
                    float seuilBlackHole = 0.3f;
                    float tirage = Random.Range(0.0f, seuilFakePlat + seuilEnemy + seuilBlackHole);
                    if ((tirage < seuilBlackHole) && (player.transform.position.y > 100))
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
                    else if((tirage < seuilEnemy + seuilBlackHole) && (player.transform.position.y > 50)) 
                    {
                        if (lastPlatform.transform.position.x > 0)
                        {
                            Instantiate(enemy1Prefab, new Vector3(Random.Range(-1.0f, 0.0f) * (cam_width - 0.6f), player.transform.position.y + Random.Range(minBoundaries + 3 * difficulty, maxBoundaries), 7), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(enemy1Prefab, new Vector3(Random.Range(0.0f, 1.0f) * (cam_width - 0.6f), player.transform.position.y + Random.Range(minBoundaries + 3 * difficulty, maxBoundaries), 7), Quaternion.identity);
                        }
                    }
                    else
                    {
                        Instantiate(brownplatPrefab, new Vector3(Random.Range(-1.0f, 1.0f) * (cam_width - 0.6f), player.transform.position.y + Random.Range(minBoundaries + 3 * difficulty, maxBoundaries), 10), Quaternion.identity);
                    }
                }
            }
        }
    }



    void updateDifficulty()                         // Update the difficuly of the game
    {
        if(player.transform.position.y < 900)
        {
            difficulty = player.transform.position.y * 0.001f;
        }
    }

    void updateScore()                                                 // Upadate the score displayed on the screen
    {
        if (player.transform.position.y * scoreFactor > score)
        {
            score = (int)(player.transform.position.y * scoreFactor);
            textScore.text = score.ToString();
        }
    }

    public void LoadScore()                                          // Load the best score of the player
    {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
    }

    public void SaveScore()                                      // Save the best score of the player
    {
        PlayerPrefs.SetInt("bestScore", bestScore);
    }

    void FallLose()                                                      // Detect if the player fell (under the bottom of the screen)
    {
        if (cam.transform.position.y - cam.orthographicSize - offset > player.transform.position.y)
        {
            SetLose();
        }
    }



    public void SetLose()                                 // Trigger the loose procedure (adn create the game over screen)
    {
        if (loose == false)
        {
            targetEnd = new Vector3(cam.transform.position.x, cam.transform.position.y - cam.orthographicSize * 2, cam.transform.position.z);
            Instantiate(gameOverScreen, new Vector3(targetEnd.x, targetEnd.y, targetEnd.z + 40), Quaternion.identity);
        }
        loose = true;
    }



    void endGame()                        // The loose procedure : make the camera go down to the game overs creen and activate the elements of the game over screen
    {
        audioManager.GetComponent<AudioSource>().enabled = false;

        if (cam.transform.position.y - targetEnd.y > 0.01)                     // Make the camera go down to the game over screen
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, targetEnd, speedEnd * Time.deltaTime);
        }
        else                                                          // Display the elements of th game over screen
        {
            if (score > bestScore)                     // Save the new best score
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
                    AudioSource.PlayClipAtPoint(soundEndGame, transform.position);
                    Destroy(player);
                }
            }
        }
    }



    void LevelCleaner()                                               // Destroy all the elements that is under the bottom of the camera
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

        platforms = GameObject.FindGameObjectsWithTag("HelicoHat");
        foreach (GameObject i in platforms)
        {
            if (i.transform.position.y < cam.transform.position.y - cam.orthographicSize - offset)
            {
                Destroy(i);
            }
        }

        platforms = GameObject.FindGameObjectsWithTag("JetPack");
        foreach (GameObject i in platforms)
        {
            if (i.transform.position.y < cam.transform.position.y - cam.orthographicSize - offset)
            {
                Destroy(i);
            }
        }
    }



    public void restart()                               // Launch a new game (from the game over screen)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backToMenu()                        // Go back to menu (from the game over screen)
    {
        SceneManager.LoadScene("EntryScene");
        SceneManager.UnloadSceneAsync("PlayScene");
    }

}
