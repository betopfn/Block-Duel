using UnityEngine;

public class ControllableBlock : MonoBehaviour
{
    public float moveSpeed = 5f;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode rotateKey;

    private bool isControllable = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
    }

    void Update()
    {
        if (isControllable)
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta se tocou no chão ou em outro bloco (com tag Block ou Ground)
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Ground"))
        {
            isControllable = false;

            // Aqui NÃO congelamos posição ou rotação — queremos que ele possa tombar!
        }
    }
}
