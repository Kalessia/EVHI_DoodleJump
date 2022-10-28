using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PlayScene");
        SceneManager.UnloadSceneAsync("EntryScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
