using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class PlateScript : MonoBehaviour
{
    public static PlateScript Instance;
    private ItemData currMeal;
    private SpriteRenderer mealSprite;

    public void Awake()
    {
        Instance = this;
        mealSprite = GetComponentInChildren<SpriteRenderer>();
        ShowMeal(false);
    }

    public void SetMeal(ItemData item)
    {
        currMeal = item;
        mealSprite.sprite = item.imagem;
        ShowMeal(true);
    }

    public void ShowMeal(bool flag)
    {
        mealSprite.enabled = flag;
    }

    public bool IsEmpty()
    {
        bool returnal = (currMeal == null) ? true : false;
        return returnal;
    }
    
    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player") && !IsEmpty())
        {
            Debug.Log("Slime entrou no prato");
            SlimeBehavior.Instance.Bonappetit();
        }
    }
}
