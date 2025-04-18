using Unity.VisualScripting;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public static SlimeMovement Instance; // Singleton

    [Header("Configurações do movimento")]
    public float jumpForce = 50f; // Força do pulo
    public float jumpInterval = 5f; // Intervalos de pulo
    public float moveForce = 5f; // Força com que ele se move
    [Range(0, 100)] public float jumpChance = 50f; // Probabilidado do pulo ocorrer

    private Rigidbody rb;

    private Vector3 destination = Vector3.zero;

    private void Awake()
    {
        Instance = this;

        // Obtém o Rigidbody anexado ao objeto
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Start()
    {
        // Configura para chamar o método Jump a cada 5 segundos
        InvokeRepeating(nameof(TryJump), 0f, jumpInterval);
    }

    private void TryJump()
    {
        // Gera o RNG do pulo
        if (Random.Range(0, 100) < jumpChance)
            Jump();
    }

    private void Jump()
    {
        if (rb != null)
        {
            // Se o slime tem um destino ele vai pular em direção ao mesmo, caso contrario, ele vai pular aleatoriamente
            Vector3 direction = (destination == Vector3.zero) ? new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized : destination - transform.position;

            // Calcula a força total: para cima + direção aleatória
            Vector3 jumpVector = Vector3.up * jumpForce + direction * moveForce;

            // Aplica a força ao Rigidbody
            rb.AddForce(jumpVector, ForceMode.Impulse);
        }
    }

    public void SetDestination(Vector3 xyz)
    {
        destination = xyz;
    }
}
