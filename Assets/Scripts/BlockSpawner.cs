using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] player1Blocks; // Prefabs do jogador 1
    public GameObject[] player2Blocks; // Prefabs do jogador 2

    public float spawnInterval = 2f;   // Intervalo entre os spawns
    public float spawnHeight = 10f;    // Altura do spawn

    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnBlocksSimultaneously();
            spawnTimer = 0f;
        }
    }

    void SpawnBlocksSimultaneously()
    {
        // Spawn jogador 1 (lado esquerdo)
        float x1 = Random.Range(-6f, -1f);
        GameObject player1Block = Instantiate(
            player1Blocks[Random.Range(0, player1Blocks.Length)],
            new Vector3(x1, spawnHeight, 0f),
            Quaternion.identity
        );
        // Garantir que a gravidade e o comportamento inicial do Rigidbody2D estejam corretos
        Rigidbody2D rb1 = player1Block.GetComponent<Rigidbody2D>();
        if (rb1 != null)
        {
            rb1.gravityScale = 1f;  // Aplica a gravidade
        }

        // Spawn jogador 2 (lado direito)
        float x2 = Random.Range(1f, 6f);
        GameObject player2Block = Instantiate(
            player2Blocks[Random.Range(0, player2Blocks.Length)],
            new Vector3(x2, spawnHeight, 0f),
            Quaternion.identity
        );
        // Garantir que a gravidade e o comportamento inicial do Rigidbody2D estejam corretos
        Rigidbody2D rb2 = player2Block.GetComponent<Rigidbody2D>();
        if (rb2 != null)
        {
            rb2.gravityScale = 1f;  // Aplica a gravidade
        }
    }
}
