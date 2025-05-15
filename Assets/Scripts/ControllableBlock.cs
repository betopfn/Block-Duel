using UnityEngine;

public class ControllableBlock : MonoBehaviour
{
    public float moveSpeed = 5f;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode rotateKey;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
    }

    void Update()
    {
        // Verifica se o script está habilitado antes de permitir o controle
        if (enabled)
        {
            float moveX = 0f;

            if (Input.GetKey(leftKey))
                moveX = -1f;
            else if (Input.GetKey(rightKey))
                moveX = 1f;

            // Aplica força horizontal proporcional
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

            if (Input.GetKeyDown(rotateKey))
                transform.Rotate(0, 0, 90f);
        }
        else
        {
            // Se o script não estiver habilitado, garante que a velocidade horizontal seja zero
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta se tocou no chão ou em outro bloco (com tag Block ou Ground)
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Ground"))
        {
            // Desativa este script para impedir mais controle
            enabled = false;

            // Opcional: Congelar a rotação se você não quiser que tombem mais
            // rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}