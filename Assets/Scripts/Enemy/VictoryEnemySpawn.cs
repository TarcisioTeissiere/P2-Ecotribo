using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryEnemySpawn : MonoBehaviour
{
    public int requiredKills = 10; // Número de inimigos abatidos para vencer
    private int killCount = 0; // Contador de inimigos abatidos
    private ScoreManager scoreManager; // Referência ao script de pontuação

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>(); // Encontra o ScoreManager

        // Se você quiser que esse objeto persista entre cenas
        DontDestroyOnLoad(this.gameObject);
    }

    // Método para ser chamado sempre que um inimigo morrer
    public void EnemyDefeated()
    {
        killCount++; // Incrementa o contador de inimigos abatidos
        CheckVictoryCondition();
    }

    private void CheckVictoryCondition()
    {
        // Verifica se o número de inimigos abatidos foi atingido e a pontuação é maior que 0
        if (killCount >= requiredKills && scoreManager != null && scoreManager.GetScore() > 0)
        {
            LoadVictoryScene();
        }
    }

    private void LoadVictoryScene()
    {
        Debug.Log("Carregando tela de vitória...");
        SceneManager.LoadScene("Victory"); // Nome da cena de vitória
    }
}
