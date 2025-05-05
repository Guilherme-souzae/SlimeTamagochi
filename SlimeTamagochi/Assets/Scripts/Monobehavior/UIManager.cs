using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dependências PH")]
    public GameObject phMinigamePanel;
    public Button phMinigameActivateButton;
    public PhSliderBehavior phSliderBehavior;

    [Header("Dependências Umidade")]
    public GameObject humidityMinigamePanel;
    public Button humidityActivateButton;
    public HumidityMinigameBehavior slimeSlimeBehavior;

    [Header("Dependências Inventário")]
    public Button eatingActivateButton;

    private void Start()
    {
        eatingActivateButton.onClick.AddListener(GoEat);

        phMinigameActivateButton.onClick.AddListener(ShowPhMinigame);
        if (phMinigamePanel != null) phMinigamePanel.SetActive(false);

        humidityActivateButton.onClick.AddListener(ShowHumidityMinigame);
        if (humidityMinigamePanel != null) humidityMinigamePanel.SetActive(false);
    }

    private void Update()
    {
        if (phMinigamePanel != null && phMinigamePanel.activeInHierarchy && Input.GetMouseButtonDown(0)) HidePhMinigame();
    }

    // Ph Minigame
    private void ShowPhMinigame()
    {
        HideButtons();
        phMinigamePanel.SetActive(true);
    }

    private void HidePhMinigame()
    {
        ShowButtons();
        phMinigamePanel.SetActive(false);
    }

    // Humidity Minigame
    public void ShowHumidityMinigame()
    {
        HideButtons();
        humidityMinigamePanel.SetActive(true);
        Invoke("HideHumidityMinigame", slimeSlimeBehavior.minigameDuration);
    }

    private void HideHumidityMinigame()
    {
        ShowButtons();
        humidityMinigamePanel.SetActive(false);
    }

    // Utils
    private void HideButtons()
    {
        phMinigameActivateButton.gameObject.SetActive(false);
        humidityActivateButton.gameObject.SetActive(false);
        eatingActivateButton.gameObject.SetActive(false);
    }

    private void ShowButtons()
    {
        phMinigameActivateButton.gameObject.SetActive(true);
        humidityActivateButton.gameObject.SetActive(true);
        eatingActivateButton.gameObject.SetActive(true);
    }

    private void GoEat()
    {
        SlimeMovement.Instance.EatingRoutine();
    }
}