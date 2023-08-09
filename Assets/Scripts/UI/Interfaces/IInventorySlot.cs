using UnityEngine;

namespace SPSDigital.UI
{
    public interface IInventorySlot
    {
        void SetSlotLevel(int level, string type);

        void SetSlotSprite(Sprite sprite);

        void ActivateSlotImage(bool isActive);
    }
}
