using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SPSDigital.UI
{
    public class InventorySlot : MonoBehaviour, IInventorySlot
    {
        [SerializeField]
        private Image slotImage;
        [SerializeField]
        private TextMeshProUGUI slotText;

        public void ActivateSlotImage(bool isActive)
        {
            slotImage.gameObject.SetActive(isActive);
        }

        public void SetSlotSprite(Sprite sprite)
        {
            slotImage.sprite = sprite;
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
