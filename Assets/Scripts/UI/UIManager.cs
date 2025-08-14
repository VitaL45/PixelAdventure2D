using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Menu Sounds")]
    [SerializeField] private AudioClip menuSelect;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }

    #region Game Over
    //Activate game over screen
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SoundManager.instance.PlaySound(menuSelect);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        SoundManager.instance.PlaySound(menuSelect);
    }

    public void Quit()
    {
        SoundManager.instance.PlaySound(menuSelect);
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (Only be executed inside the editor)
    #endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
        SoundManager.instance.PlaySound(menuSelect);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.05f);
        SoundManager.instance.PlaySound(menuSelect);
    }
    #endregion
}
