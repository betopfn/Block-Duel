// ControllableBlock.cs
using UnityEngine;

public class ControllableBlock : MonoBehaviour
{
    public float moveSpeed = 5f;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode rotateKey;

    [HideInInspector]
    public int playerNumber;

    private Rigidbody2D rb;
    private BlockSpawner spawner;
    private bool hasStopped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        rb.constraints = RigidbodyConstraints2D.None;

        // Define a tag baseada no playerNumber
        if (playerNumber == 1)
            gameObject.tag = "Player1";
        else if (playerNumber == 2)
            gameObject.tag = "Player2";

        hasStopped = false;
        enabled = true; // Garantir que o controle está ativo
    }

    void Update()
    {
        if (hasStopped || !enabled)
        {
            // Para movimento horizontal após cair
            rb.velocity = new Vector2(0f, rb.velocity.y);
            return;
        }

        float moveX = 0f;
        if (Input.GetKey(leftKey))
            moveX = -1f;
        else if (Input.GetKey(rightKey))
            moveX = 1f;

        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(rotateKey))
            transform.Rotate(0, 0, 90f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Ground")) && !hasStopped)
        {
            hasStopped = true;
            enabled = false; // Bloqueia controle

            // Notifica spawner que o bloco parou de cair
            if (spawner != null)
            {
                spawner.NotifyBlockLanded(gameObject.tag);
            }
            else
            {
                // Busca o spawner se não setado
                BlockSpawner foundSpawner = FindObjectOfType<BlockSpawner>();
                if (foundSpawner != null)
                    foundSpawner.NotifyBlockLanded(gameObject.tag);
            }
        }
    }

    public void SetSpawner(BlockSpawner spawner)
    {
        this.spawner = spawner;
    }
}
