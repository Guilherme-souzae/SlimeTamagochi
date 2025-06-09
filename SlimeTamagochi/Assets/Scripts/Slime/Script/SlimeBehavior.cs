using System.Collections;
using UnityEngine;

public enum BehaviorState
{
    IDLE,
    GOING_TO_EAT,
    EATING,
    GOING_TO_SLEEP,
    SLEEPING
}

public class SlimeBehavior : MonoBehaviour
{
    public static SlimeBehavior Instance;

    [Header("Dependências do cenário")]
    public GameObject plate;
    public GameObject bed;

    [Header("Configurações da rotina de alimentação")]
    public float eatingTime = 60f;

    [Header("Configurações de movimento")]
    public float jumpForce = 50f;
    public float jumpInterval = 5f;
    public float moveForce = 5f;
    [Range(0, 100)] public float jumpChance = 50f;

    private BehaviorState state;
    private bool grounded;

    private Rigidbody rb;
    private Vector3 destination = Vector3.zero;

    private void Awake()
    {
        Instance = this;
        grounded = true;
        TryGetComponent(out rb);
    }

    private void Start()
    {
        DataHolder buffer = SaveSystem.LoadSlime();
        if (buffer?.isSleeping == true)
        {
            Vector3 bedPosition = bed.transform.position;
            bedPosition.y += 1.2f;
            transform.position = bedPosition;
            SetState(BehaviorState.SLEEPING);
        }
        else
        {
            SetState(BehaviorState.IDLE);
        }
    }

    public void SetState(BehaviorState newState)
    {
        if (newState != state)
        {
            state = newState;
            Decide();
        }
    }

    private void Decide()
    {
        CancelInvoke();

        switch (state)
        {
            case BehaviorState.IDLE:
                destination = Vector3.zero;
                InvokeRepeating(nameof(Jump), 0f, jumpInterval);
                break;
            case BehaviorState.GOING_TO_EAT:
                destination = plate.transform.position;
                InvokeRepeating(nameof(Jump), 0f, jumpInterval);
                break;
            case BehaviorState.EATING:
                StartCoroutine(Chill(eatingTime));
                break;
            case BehaviorState.GOING_TO_SLEEP:
                destination = bed.transform.position;
                InvokeRepeating(nameof(Jump), 0f, jumpInterval);
                break;
        }
    }

    private void Jump()
    {
        if (rb == null || !grounded || Random.Range(0, 100) >= jumpChance) return;

        Vector3 direction = (destination == Vector3.zero)
            ? new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized
            : (destination - transform.position).normalized;

        Vector3 jumpVector = Vector3.up * jumpForce + direction * moveForce;
        rb.AddForce(jumpVector, ForceMode.Impulse);
    }

    private IEnumerator Chill(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (state == BehaviorState.EATING)
        {
            PlateScript.Instance.ShowMeal(false);
            SlimeValues.Instance.Consume(PlateScript.Instance.GetMeal());
            PlateScript.Instance.SetMealEmpty();
        }
        
        SetState(BehaviorState.IDLE);
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;

        if (collision.gameObject.CompareTag("Bed") && state == BehaviorState.GOING_TO_SLEEP)
        {
            Debug.Log("Slime está dormindo");
            SetState(BehaviorState.SLEEPING);
            SlimeValues.Instance?.SetState(ValueState.SLEEPING);
        }
    }

    public void Bonappetit()
    {
        if (state == BehaviorState.GOING_TO_EAT)
        {
            SetState(BehaviorState.EATING);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    public BehaviorState GetState()
    {
        return state;
    }
}