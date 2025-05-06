using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HumidityMinigameBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Configurações do minigame")]
    public float clickHumidityRate = 0.1f;
    public float minigameDuration = 5f;
    public float returnalStep = 0.2f;
    public float maxSpeedCap = 100f;
    [Range(0,100)] public int maxReturnal = 10;

    [Header("Configurações da física")]
    public float dragSmoothness = 10f;
    public float friction = 0.5f;
    public float bounciness = 1f;

    private RectTransform rectTransform;
    private RectTransform canvasRect;
    private bool isDragging;
    private Vector2 speed;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!isDragging && speed.magnitude > 0.01f)
        {
            rectTransform.anchoredPosition += speed * Time.deltaTime * 60f;
            BounceOffEdges();
            speed = Vector2.Lerp(speed, Vector2.zero, friction * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        rectTransform.anchoredPosition = Vector2.zero;
        speed = Vector2.zero;
        InvokeRepeating("CheckMovement", 0f, returnalStep);
    }

    private void OnDisable()
    {
        CancelInvoke("CheckMovement");
    }

    // Fisica

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        speed = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        speed = eventData.delta / dragSmoothness;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void BounceOffEdges()
    {
        Vector2 pos = rectTransform.anchoredPosition;
        Vector2 size = rectTransform.rect.size;
        Vector2 canvasSize = canvasRect.rect.size;

        // Limites
        float left = -canvasSize.x / 2f + size.x / 2f;
        float right = canvasSize.x / 2f - size.x / 2f;
        float bottom = -canvasSize.y / 2f + size.y / 2f;
        float top = canvasSize.y / 2f - size.y / 2f;

        // Horizontal
        if (pos.x < left)
        {
            pos.x = left;
            speed.x = -speed.x * bounciness;
        }
        else if (pos.x > right)
        {
            pos.x = right;
            speed.x = -speed.x * bounciness;
        }

        // Vertical
        if (pos.y < bottom)
        {
            pos.y = bottom;
            speed.y = -speed.y * bounciness;
        }
        else if (pos.y > top)
        {
            pos.y = top;
            speed.y = -speed.y * bounciness;
        }

        rectTransform.anchoredPosition = pos;
    }

    private void CheckMovement()
    {
        float absSpeed = speed.magnitude;
        float normalized = Mathf.Clamp01(absSpeed / maxSpeedCap);
        int returnal = Mathf.RoundToInt(normalized * maxReturnal);

        Debug.Log("Movimento detectado: " + returnal);
    }

}
