using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dependências")]
    public GameObject phMinigamePanel;
    public Button phMinigameActivateButton;
    public PhSliderBehavior phSliderBehavior;

    private void Start()
    {
        phMinigameActivateButton.onClick.AddListener(ShowPhMinigame);
        if (phMinigamePanel != null) phMinigamePanel.SetActive(false);
    }

    private void Update()
    {
        if (phMinigamePanel != null && phMinigamePanel.activeInHierarchy && Input.GetMouseButtonDown(0)) HidePhMinigame();
    }

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
}
