using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // Название предмета
    public string itemName;
    // Иконка
    public Sprite icon;
    // Описание
    public string description;
    // Можно ли стакать
    public bool isStackable;     

    [Tooltip("Укажите тип предмета (например: здоровье, патроны, оружие и т.д.)")]
    // Тип предмета (новое поле)
    public string itemTypeTag;      
}