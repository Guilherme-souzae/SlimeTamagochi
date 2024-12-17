using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float jumpForce = 50f; // For�a do pulo
    public float jumpInterval = 5f; // Intervalos de pulo
    public float moveForce = 5f;
    [Range(0, 100)] public float jumpChance = 50f; // Probabilidado do pulo ocorrer

    private Rigidbody rb;

    void Start()
    {
        // Obt�m o Rigidbody anexado ao objeto
        rb = GetComponent<Rigidbody>();

        // Configura para chamar o m�todo Jump a cada 5 segundos
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
            // Gera uma dire��o aleat�ria no plano XZ
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

            // Calcula a for�a total: para cima + dire��o aleat�ria
            Vector3 jumpVector = Vector3.up * jumpForce + randomDirection * moveForce;

            // Aplica a for�a ao Rigidbody
            rb.AddForce(jumpVector, ForceMode.Impulse);
        }
    }
}
