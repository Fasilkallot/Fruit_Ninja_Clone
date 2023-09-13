
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI finalScore;

    public void StartGame()
    {
        startMenu.SetActive(false);
        GameManager.Instance.GameStart();
    }
    public void Ouit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        gameOverScreen.SetActive(false);
        GameManager.Instance.GameStart();
    }
    public void Menu()
    {
        gameOverScreen.SetActive(false);
        startMenu.SetActive(true);
    }
    public void GameOver()
    {
        GameManager.Instance.GameOver();
        gameOverScreen.SetActive(true) ;
        finalScore.text = $"You Got {GameManager.Instance.point}";
    }
    private void OnEnable()
    {
        Bomb.gameOver += GameOver;
    }
    private void OnDisable()
    {
        Bomb.gameOver -= GameOver;
    }
}
