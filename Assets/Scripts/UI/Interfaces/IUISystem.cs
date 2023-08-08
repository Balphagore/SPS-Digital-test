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

        void CreateFlyingCoin(int newValue, Vector2 spawnPosition);

        void SetInventorySlotValue(int slotId, int slotLevel);

        void ActivateLootPanel();

        void DeactivateLootPanel();
    }
}
