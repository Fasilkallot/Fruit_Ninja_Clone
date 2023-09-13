
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        GameManager.Instance.score = this;
    }
    public void UpdateScore()
    {
        GameManager.Instance.point++;
        scoreText.text = GameManager.Instance.point.ToString();
    }
}
