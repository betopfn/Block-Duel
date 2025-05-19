// VictoryDetector.cs
using UnityEngine;

public class VictoryDetector : MonoBehaviour
{
    private bool victoryDeclared = false;
    public BlockSpawner blockSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (victoryDeclared)
            return;

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            victoryDeclared = true;

            string winner = other.CompareTag("Player1") ? "Jogador 1" : "Jogador 2";
            Debug.Log($"{winner} venceu!");

            if (blockSpawner != null)
            {
                blockSpawner.EndGame();
                FreezeAllBlocks();
            }

            Time.timeScale = 0f;
        }
    }

    void FreezeAllBlocks()
    {
        string[] playerTags = { "Player1", "Player2" };

        foreach (string tag in playerTags)
        {
            foreach (var block in GameObject.FindGameObjectsWithTag(tag))
            {
                if (block.TryGetComponent<Rigidbody2D>(out var rb))
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
        }
    }
}
