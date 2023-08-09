using UnityEngine;

namespace SPSDigital.UI
{
    public interface IUISystem
    {
        delegate void EquipItemHandle();
        event EquipItemHandle EquipItemEvent;

        delegate void DropItemHandle();
        event DropItemHandle DropItemEvent;

        void SetCoinsValueText(int value);

        void CreateCoin(int newValue, bool isNewItem);

        void SetInventorySlotValue(Sprite sprite, int slotId, int slotLevel, string type);

        void SetLootItemValue(Sprite sprite, int itemLevel, string statName, bool isNewItem, bool isBetter, string type);

        void ActivateLootPanel();

        void DeactivateLootPanel();

        void SetStatValue(int statId, string statValue);
    }
}
