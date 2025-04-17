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
    public SlimeSlimeBehavior slimeSlimeBehavior;

    [Header("Configurações")]
    public float HumidityMinigameDuration = 5f;

    private void Start()
    {
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
    public void ShowPhMinigame()
    {
        phMinigamePanel.SetActive(true);
        phMinigameActivateButton.gameObject.SetActive(false);
        phSliderBehavior.Reset();
    }

    public void HidePhMinigame()
    {
        Debug.Log(phSliderBehavior.getPh());
        phSliderBehavior.Stop();
        phMinigamePanel.SetActive(false);
        phMinigameActivateButton.gameObject.SetActive(true);
    }

    // Humidity Minigame
    public void ShowHumidityMinigame()
    {
        slimeSlimeBehavior.Reset();
        humidityMinigamePanel.SetActive(true);
        humidityActivateButton.gameObject.SetActive(false);
        Invoke("HideHumidityMinigame", HumidityMinigameDuration);
    }

    private void HideHumidityMinigame()
    {
        humidityMinigamePanel.SetActive(false);
        humidityActivateButton.gameObject.SetActive(true);
    }
}
