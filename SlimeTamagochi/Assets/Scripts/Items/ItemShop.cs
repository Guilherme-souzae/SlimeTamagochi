using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemShop", menuName = "Meal/Item Database")]
public class ItemShop : ScriptableObject
{
    public List<ItemData> todosOsItens;
    private Dictionary<string, ItemData> cardapio;

    private void InitializeDict()
    {
        cardapio = new Dictionary<string, ItemData>();  
        foreach (var meal in todosOsItens)
        {
            string key = meal.name;
            cardapio.Add(key, meal);
        }
    }

    public ItemData CallTheWaiter(string name = null)
    {
        if (cardapio == null)
            InitializeDict();

        if (string.IsNullOrEmpty(name) || !cardapio.TryGetValue(name, out var itemData))
        {
            if (todosOsItens != null && todosOsItens.Count > 0)
            {
                int index = Random.Range(0, todosOsItens.Count);
                return todosOsItens[index];
            }
            else
            {
                return null;
            }
        }
        
        return itemData;
    }
}