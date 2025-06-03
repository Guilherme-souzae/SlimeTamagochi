using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Dependências - PH")]
    public TextMeshProUGUI UIph;
    public GameObject phMinigamePanel;
    public Button phMinigameActivateButton;
    public PhSliderBehavior phSliderBehavior;

    [Header("Dependências - Umidade")]
    public TextMeshProUGUI UIhumidity;
    public GameObject humidityMinigamePanel;
    public Button humidityActivateButton;
    public HumidityMinigameBehavior humidityMinigameBehavior;

    [Header("Dependências - Fome")]
    public TextMeshProUGUI UIhunger;
    public ItemShop restaurant;
    public Button eatingActivateButton;

    [Header("Dependências - Energia")] 
    public TextMeshProUGUI UIenergy;
    public Button energyActivateButton;
    
    private void Awake() => Instance = this;
    
    private void Start()
    {
        // Ligações dos botões com os métodos
        eatingActivateButton?.onClick.AddListener(GoEat);
        eatingActivateButton?.onClick.AddListener(AwakeTheSlime);
        
        energyActivateButton?.onClick.AddListener(GoSleep);
        energyActivateButton?.onClick.AddListener(AwakeTheSlime);
        
        phMinigameActivateButton?.onClick.AddListener(ShowPhMinigame);
        phMinigameActivateButton?.onClick.AddListener(AwakeTheSlime);
        if (phMinigamePanel != null) phMinigamePanel.SetActive(false);

        humidityActivateButton?.onClick.AddListener(ShowHumidityMinigame);
        humidityActivateButton?.onClick.AddListener(AwakeTheSlime);
        if (humidityMinigamePanel != null) humidityMinigamePanel.SetActive(false);
    }

    private void Update()
    {
        if (phMinigamePanel != null && phMinigamePanel.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            phSliderBehavior?.updateSlimeTroughtMinigame();
            HidePhMinigame();
        }
    }

    // -------------------------------
    // PH Minigame
    private void ShowPhMinigame()
    {
        HideButtons();
        if (phMinigamePanel != null) phMinigamePanel.SetActive(true);
    }

    private void HidePhMinigame()
    {
        if (phMinigamePanel != null) phMinigamePanel.SetActive(false);
        ShowButtons();
    }

    // -------------------------------
    // Humidity Minigame
    public void ShowHumidityMinigame()
    {
        if (humidityMinigamePanel == null || humidityMinigameBehavior == null) return;

        HideButtons();
        humidityMinigamePanel.SetActive(true);
        Invoke(nameof(HideHumidityMinigame), humidityMinigameBehavior.minigameDuration);
    }

    private void HideHumidityMinigame()
    {
        if (humidityMinigamePanel != null) humidityMinigamePanel.SetActive(false);
        ShowButtons();
    }

    // -------------------------------
    // Utilidades da interface
    private void HideButtons()
    {
        UIph?.gameObject.SetActive(false);
        UIhumidity?.gameObject.SetActive(false);
        UIhunger?.gameObject.SetActive(false);
        UIenergy?.gameObject.SetActive(false);
        phMinigameActivateButton?.gameObject.SetActive(false);
        humidityActivateButton?.gameObject.SetActive(false);
        eatingActivateButton?.gameObject.SetActive(false);
        energyActivateButton?.gameObject.SetActive(false);
    }

    private void ShowButtons()
    {
        UIph?.gameObject.SetActive(true);
        UIhumidity?.gameObject.SetActive(true);
        UIhunger?.gameObject.SetActive(true);
        UIenergy?.gameObject.SetActive(true);
        phMinigameActivateButton?.gameObject.SetActive(true);
        humidityActivateButton?.gameObject.SetActive(true);
        eatingActivateButton?.gameObject.SetActive(true);
        energyActivateButton?.gameObject.SetActive(true);
    }

    // -------------------------------
    // Ações principais
    private void GoEat()
    {
        if (PlateScript.Instance == null || SlimeBehavior.Instance == null || restaurant == null) return;

        if (PlateScript.Instance.IsEmpty())
        {
            ItemData meal = restaurant.CallTheWaiter();
            PlateScript.Instance.SetMeal(meal);
            SlimeBehavior.Instance.SetState(BehaviorState.GOING_TO_EAT);
        }
    }

    private void GoSleep()
    {
        if (SlimeBehavior.Instance != null)
        {
            SlimeBehavior.Instance.SetState(BehaviorState.GOING_TO_SLEEP);
        }
    }

    private void AwakeTheSlime()
    {
        if (SlimeValues.Instance != null)
        {
            SlimeValues.Instance.SetState(ValueState.IDLE);
        }
    }

    public void Die()
    {
        HideButtons();
    }
}