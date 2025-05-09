using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
