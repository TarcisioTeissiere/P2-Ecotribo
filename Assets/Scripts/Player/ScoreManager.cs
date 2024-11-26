using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            SceneManager.sceneLoaded += OnSceneLoaded; 
        }
        else
        {
            Destroy(gameObject); // Remove instâncias duplicadas
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        if (score < 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Pontuação: " + score;
        }
    }

    public int GetScore()
    {
        return score;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica se a cena atual é uma das que precisam mostrar pontuação
        if (scene.name == "First" || scene.name == "Second" || scene.name == "Victory")
        {
            // Tenta encontrar um objeto TextMeshProUGUI na nova cena
            scoreText = FindObjectOfType<TextMeshProUGUI>();
            UpdateScoreText();
        }
        else
        {
            scoreText = null;
            score =  0; 
        }
    }
}
