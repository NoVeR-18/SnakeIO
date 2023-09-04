using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private GameObject LoseMenu;
    [SerializeField]
    private GameObject WinMenu;
    [SerializeField]
    private GameObject ButtleHud;
    private void Start()
    {
        instance = this;
    }

    public void ShowWinMenu()
    {
        if (WinMenu != null) { WinMenu.SetActive(true); ButtleHud.SetActive(false); }
    }
    public void ShowLoseMenu()
    {
        if (LoseMenu != null) { LoseMenu.SetActive(true); ButtleHud.SetActive(false); }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
