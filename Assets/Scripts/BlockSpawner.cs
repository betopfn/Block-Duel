using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] player1Blocks;
    public GameObject[] player2Blocks;

    public float spawnHeight = 12f;
    public float destroyBelowY = -10f;

    private GameObject currentPlayer1Block = null;
    private GameObject currentPlayer2Block = null;

    private bool player1Ready = true;
    private bool player2Ready = true;

    void Update()
    {
        // Checa se blocos caíram fora da tela e libera spawn
        CheckFallenBlocks();

        // Gera bloco do Player 1 apenas se o anterior já parou
        if (player1Ready)
        {
            currentPlayer1Block = SpawnBlock(player1Blocks, Random.Range(-6f, -1f), 1);
            player1Ready = false;
        }

        // Gera bloco do Player 2 apenas se o anterior já parou
        if (player2Ready)
        {
            currentPlayer2Block = SpawnBlock(player2Blocks, Random.Range(1f, 6f), 2);
            player2Ready = false;
        }
    }

    GameObject SpawnBlock(GameObject[] blockArray, float xPos, int playerNumber)
    {
        GameObject prefab = blockArray[Random.Range(0, blockArray.Length)];
        GameObject instance = Instantiate(prefab, new Vector3(xPos, spawnHeight, 0f), Quaternion.identity);

        // Setar tag e playerNumber para controle
        if (playerNumber == 1)
            instance.tag = "Player1";
        else if (playerNumber == 2)
            instance.tag = "Player2";

        // Linka o spawner no bloco
        ControllableBlock controllable = instance.GetComponent<ControllableBlock>();
        if (controllable != null)
        {
            controllable.playerNumber = playerNumber;
            controllable.SetSpawner(this);
            controllable.enabled = true; // Garantir que o controle está ativo ao nascer
        }

        Renderer renderer = instance.GetComponent<Renderer>();
        if (renderer != null)
            renderer.enabled = true;

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.gravityScale = 1f;

        return instance;
    }

    void CheckFallenBlocks()
    {
        if (currentPlayer1Block != null && currentPlayer1Block.transform.position.y < destroyBelowY)
        {
            Destroy(currentPlayer1Block);
            currentPlayer1Block = null;
            player1Ready = true;
        }

        if (currentPlayer2Block != null && currentPlayer2Block.transform.position.y < destroyBelowY)
        {
            Destroy(currentPlayer2Block);
            currentPlayer2Block = null;
            player2Ready = true;
        }
    }

    // Chamado pelo bloco quando para de cair
    public void NotifyBlockLanded(string tag)
    {
        if (tag == "Player1") player1Ready = true;
        else if (tag == "Player2") player2Ready = true;
    }

    public void EndGame()
    {
        Debug.Log("Jogo encerrado.");
        // Lógica extra para o fim do jogo (pausar, tela, etc)
    }
}
