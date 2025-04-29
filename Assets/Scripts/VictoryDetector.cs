using UnityEngine;

public class VictoryDetector : MonoBehaviour
{
    private bool victoryDeclared = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!victoryDeclared && (other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            victoryDeclared = true;
            string winner = other.tag == "Player1" ? "Jogador 1" : "Jogador 2";
            Debug.Log($"{winner} venceu!");
            Time.timeScale = 0; // pausa o jogo
        }
    }
}
