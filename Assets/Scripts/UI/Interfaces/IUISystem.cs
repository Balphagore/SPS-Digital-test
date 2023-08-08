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

        void SetInventorySlotValue(int slotId, int slotLevel);

        void SetCurrentItemValue(Sprite sprite, int itemLevel);

        void SetNewItemValue(Sprite sprite, int itemLevel);

        void ActivateLootPanel();

        void DeactivateLootPanel();
    }
}
