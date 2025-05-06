using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum BehaviorState
{
    IDLE,
    GOING_TO_EAT,
    EATING,
    GOING_TO_SLEEP,
    SLEEPING,
    CYST
}

public class SlimeBehavior : MonoBehaviour
{
    public static SlimeBehavior Instance; // Singleton

    [Header("Dependências do cenário")]
    public GameObject plate;
    public GameObject bed;

    [Header("Configurações da rotina de alimentação")]
    public float eatingTime = 60f;

    [Header("Configurações do movimento")]
    public float jumpForce = 50f; // Força do pulo
    public float jumpInterval = 5f; // Intervalos de pulo
    public float moveForce = 5f; // Força com que ele se move
    [Range(0, 100)] public float jumpChance = 50f; // Probabilidado do pulo ocorrer

    [Header("Estado do slime")]
    private BehaviorState state;
    private bool grounded;

    private Rigidbody rb;

    private Vector3 destination = Vector3.zero;
    
    private void Awake()
    {
        Instance = this;
        grounded = true;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        DataHolder buffer = SaveSystem.LoadSlime();
        if (buffer != null)
        {
            if (buffer.isSleeping)
            {
                Vector3 bedPosition = bed.transform.position;
                bedPosition.y += 1.2f;
                transform.position = bedPosition;
                UpdateState(BehaviorState.SLEEPING);
            }
            else
            {
                UpdateState(BehaviorState.IDLE);
            }
        }
        else
        {
            UpdateState(BehaviorState.IDLE);
        }
    }

    // State Logic
    public void UpdateState(BehaviorState newState)
    {
        if (newState != state)
        {
            state = newState;
            Decide();
        }
    }

    private void Decide()
    {
        switch (state)
        {
            case BehaviorState.IDLE:
                CancelInvoke();
                InvokeRepeating(nameof(Jump), 0f, jumpInterval);
                break;
           
            case BehaviorState.GOING_TO_EAT:
                CancelInvoke();
                destination = plate.transform.position;
                InvokeRepeating(nameof(Jump), 0f, jumpInterval);
                break;

            case BehaviorState.EATING:
                CancelInvoke();
                StartCoroutine(Chill(eatingTime));
                break;

            case BehaviorState.GOING_TO_SLEEP:
                CancelInvoke();
                destination = bed.transform.position;
                InvokeRepeating(nameof(Jump), 0f, jumpInterval);
                break;

            case BehaviorState.SLEEPING:
                CancelInvoke();
                break;
        }
    }

    // Movement Functions

    private void Jump()
    {
        if (rb != null && Random.Range(0, 100) < jumpChance && grounded)
        {
            Vector3 direction = (destination == Vector3.zero) ? new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized : destination - transform.position;
            Vector3 jumpVector = Vector3.up * jumpForce + direction * moveForce;
            rb.AddForce(jumpVector, ForceMode.Impulse);
        }
    }

    private IEnumerator Chill(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UpdateState(BehaviorState.IDLE);
    }

    // Events
    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;

        if (collision.gameObject.CompareTag("Plate") && state == BehaviorState.GOING_TO_EAT)
        {
            UpdateState(BehaviorState.EATING);
        }

        if (collision.gameObject.CompareTag("Bed") && state == BehaviorState.GOING_TO_SLEEP)
        {
            UpdateState(BehaviorState.SLEEPING);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    // Auxiliary

    public bool GetSleeping()
    {
        return (state == BehaviorState.SLEEPING) ? true : false;
    }
}
