using UnityEngine;

namespace SPSDigital.UI
{
    public interface IInventorySlot
    {
        void SetSlotLevel(int level);

        void SetSlotSprite(Sprite sprite);

        void ActivateSlotImage(bool isActive);
    }
}
