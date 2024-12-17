using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float jumpForce = 50f; // Força do pulo
    public float jumpInterval = 5f; // Intervalos de pulo
    public float moveForce = 5f;
    [Range(0, 100)] public float jumpChance = 50f; // Probabilidado do pulo ocorrer

    private Rigidbody rb;

    void Start()
    {
        // Obtém o Rigidbody anexado ao objeto
        rb = GetComponent<Rigidbody>();

        // Configura para chamar o método Jump a cada 5 segundos
        InvokeRepeating(nameof(TryJump), 0f, jumpInterval);
    }

    void TryJump()
    {
        // Gera o RNG do pulo
        if (Random.Range(0, 100) < jumpChance)
            Jump();
    }

    void Jump()
    {
        if (rb != null)
        {
            // Gera uma direção aleatória no plano XZ
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

            // Calcula a força total: para cima + direção aleatória
            Vector3 jumpVector = Vector3.up * jumpForce + randomDirection * moveForce;

            // Aplica a força ao Rigidbody
            rb.AddForce(jumpVector, ForceMode.Impulse);
        }
    }
}
