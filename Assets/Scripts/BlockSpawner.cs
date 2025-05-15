using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] player1Blocks; // Prefabs do jogador 1
    public GameObject[] player2Blocks; // Prefabs do jogador 2

    public float spawnInterval = 2f;   // Intervalo entre os spawns
    public float spawnHeight = 10f;     // Altura do spawn

    public KeyCode player1Left = KeyCode.A;
    public KeyCode player1Right = KeyCode.D;
    public KeyCode player1Rotate = KeyCode.S;

    public KeyCode player2Left = KeyCode.LeftArrow;
    public KeyCode player2Right = KeyCode.RightArrow;
    public KeyCode player2Rotate = KeyCode.DownArrow;

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
        GameObject player1BlockPrefab = player1Blocks[Random.Range(0, player1Blocks.Length)];
        GameObject player1BlockInstance = Instantiate(
            player1BlockPrefab,
            new Vector3(x1, spawnHeight, 0f),
            Quaternion.identity
        );

        // Configura as teclas de controle para o jogador 1
        ControllableBlock controller1 = player1BlockInstance.GetComponent<ControllableBlock>();
        if (controller1 != null)
        {
            controller1.leftKey = player1Left;
            controller1.rightKey = player1Right;
            controller1.rotateKey = player1Rotate;
        }

        // Garante que a gravidade e o comportamento inicial do Rigidbody2D estejam corretos
        Rigidbody2D rb1 = player1BlockInstance.GetComponent<Rigidbody2D>();
        if (rb1 != null)
        {
            rb1.gravityScale = 1f; // Aplica a gravidade
        }

        // Spawn jogador 2 (lado direito)
        float x2 = Random.Range(1f, 6f);
        GameObject player2BlockPrefab = player2Blocks[Random.Range(0, player2Blocks.Length)];
        GameObject player2BlockInstance = Instantiate(
            player2BlockPrefab,
            new Vector3(x2, spawnHeight, 0f),
            Quaternion.identity
        );

        // Configura as teclas de controle para o jogador 2
        ControllableBlock controller2 = player2BlockInstance.GetComponent<ControllableBlock>();
        if (controller2 != null)
        {
            controller2.leftKey = player2Left;
            controller2.rightKey = player2Right;
            controller2.rotateKey = player2Rotate;
        }

        // Garante que a gravidade e o comportamento inicial do Rigidbody2D estejam corretos
        Rigidbody2D rb2 = player2BlockInstance.GetComponent<Rigidbody2D>();
        if (rb2 != null)
        {
            rb2.gravityScale = 1f; // Aplica a gravidade
        }
    }
}