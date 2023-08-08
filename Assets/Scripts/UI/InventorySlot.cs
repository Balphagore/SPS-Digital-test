using UnityEngine;
using TMPro;

namespace SPSDigital.UI
{
    public class InventorySlot : MonoBehaviour, IInventorySlot
    {
        [SerializeField]
        private GameObject slotImage;
        [SerializeField]
        private TextMeshProUGUI slotText;

        public void ActivateSlotImage(bool isActive)
        {
            slotImage.SetActive(isActive);
        }

        public void SetSlotLevel(int level)
        {
            if (level > 0)
            {
                slotText.enabled = true;
                slotText.text = "Level " + level;
            }
            else
            {
                slotText.enabled = false;
            }
        }
    }
}
