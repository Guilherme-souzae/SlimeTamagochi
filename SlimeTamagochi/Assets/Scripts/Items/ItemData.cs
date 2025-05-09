using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Meal/Item")]
public class ItemData : ScriptableObject
{
    public int ph, humidity, hunger, energy;
    public Sprite imagem;
}
