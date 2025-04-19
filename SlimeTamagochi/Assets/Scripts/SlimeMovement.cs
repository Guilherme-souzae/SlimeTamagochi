using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public static SlimeMovement Instance; // Singleton

    [Header("Depend�ncias do cen�rio")]
    public GameObject plate;

    [Header("Configura��es da rotina de alimenta��o")]
    public float eatingTime = 60f;

    [Header("Configura��es do movimento")]
    public float jumpForce = 50f; // For�a do pulo
    public float jumpInterval = 5f; // Intervalos de pulo
    public float moveForce = 5f; // For�a com que ele se move
    [Range(0, 100)] public float jumpChance = 50f; // Probabilidado do pulo ocorrer

    [Header("Estado do slime")]
    public bool goingToEat = false;
    public bool grounded = false;


    private Rigidbody rb;

    private Vector3 destination = Vector3.zero;
    
    private void Awake()
    {
        Instance = this;

        // Obt�m o Rigidbody anexado ao objeto
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Configura para chamar o m�todo Jump a cada 5 segundos
        InvokeRepeating(nameof(TryJump), 0f, jumpInterval);
    }

    private void TryJump()
    {
        // Gera o RNG do pulo
        if (Random.Range(0, 100) < jumpChance && grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (rb != null)
        {
            // Se o slime tem um destino ele vai pular em dire��o ao mesmo, caso contrario, ele vai pular aleatoriamente
            Vector3 direction = (destination == Vector3.zero) ? new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized : destination - transform.position;

            // Calcula a for�a total: para cima + dire��o aleat�ria
            Vector3 jumpVector = Vector3.up * jumpForce + direction * moveForce;

            // Aplica a for�a ao Rigidbody
            rb.AddForce(jumpVector, ForceMode.Impulse);
        }
    }

    public void EatingRoutine()
    {
        destination = plate.transform.position;
        goingToEat = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;

        if (collision.gameObject.CompareTag("Plate") && goingToEat)
        {
            Debug.Log("Slime colidiu");
            StartCoroutine(EatAndChill());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    private IEnumerator EatAndChill()
    {
        Debug.Log("Comendo");

        // Para os pulos
        CancelInvoke(nameof(TryJump));

        // Faz o slime parar completamente
        rb.velocity = Vector3.zero;

        // Espera o tempo de comer
        yield return new WaitForSeconds(eatingTime);

        // Libera os pulos novamente
        InvokeRepeating(nameof(TryJump), 0f, jumpInterval);

        // Reseta o destino para voltar ao comportamento aleat�rio
        destination = Vector3.zero;

        goingToEat = false;
    }
}
