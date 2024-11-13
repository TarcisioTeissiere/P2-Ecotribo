using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText; // Arraste o TextMeshProUGUI aqui pelo Inspector

    private void Start()
    {
        UpdateScoreText(); // Atualiza o texto inicialmente
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        if (score < 0)
        {
            SceneManager.LoadScene("Menu"); // Carrega a cena de derrota
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Pontuação: " + score; // Atualiza o texto na tela
    }

    public int GetScore()
    {
        return score;
    }
}
