using UnityEngine;

public class VictoryDetector : MonoBehaviour
{
    private bool victoryDeclared = false;
    public BlockSpawner blockSpawner;

    [Header("Player 1 Victory Screen")]
    public GameObject telaVitoriaPlayer1Object; // Objeto com o SpriteRenderer da tela do Player 1
    public Animator animacaoVitoriaPlayer1;   // Animator da tela do Player 1

    [Header("Player 2 Victory Screen")]
    public GameObject telaVitoriaPlayer2Object; // Objeto com o SpriteRenderer da tela do Player 2
    public Animator animacaoVitoriaPlayer2;   // Animator da tela do Player 2

    public string triggerVitoria = "MostrarVitoria"; // Nome do trigger no Animator

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (victoryDeclared)
            return;

        if (other.CompareTag("Player1"))
        {
            victoryDeclared = true;
            Debug.Log("Jogador 1 venceu!");
            AtivarTelaVitoria(telaVitoriaPlayer1Object, animacaoVitoriaPlayer1);
            FinalizarJogo();
        }
        else if (other.CompareTag("Player2"))
        {
            victoryDeclared = true;
            Debug.Log("Jogador 2 venceu!");
            AtivarTelaVitoria(telaVitoriaPlayer2Object, animacaoVitoriaPlayer2);
            FinalizarJogo();
        }
    }

    void AtivarTelaVitoria(GameObject telaVitoriaObject, Animator animacaoVitoria)
    {
        if (telaVitoriaObject != null)
        {
            // Ativa o SpriteRenderer
            SpriteRenderer renderer = telaVitoriaObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }
            else
            {
                Debug.LogError($"O objeto '{telaVitoriaObject.name}' não possui um SpriteRenderer!");
            }
        }
        else
        {
            Debug.LogError("O objeto da tela de vitória não foi atribuído no Inspector!");
        }

        // Dispara o trigger na animação, se houver um Animator atribuído
        if (animacaoVitoria != null)
        {
            animacaoVitoria.SetTrigger(triggerVitoria);
        }
    }

    void FinalizarJogo()
    {
        if (blockSpawner != null)
        {
            blockSpawner.EndGame();
            FreezeAllBlocks();
        }

        Time.timeScale = 0f;
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