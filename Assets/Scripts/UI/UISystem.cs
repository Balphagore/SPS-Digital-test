using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using AYellowpaper;

namespace SPSDigital.UI
{
    public class UISystem : MonoBehaviour, IUISystem
    {
        [SerializeField]
        private Transform coinPrefab;
        [SerializeField]
        private Transform target;

        [SerializeField]
        private Transform uICanvas;
        [SerializeField]
        private TextMeshProUGUI coinsValueText;
        [SerializeField]
        private GameObject lootPanel;
        [SerializeField]
        private List<InterfaceReference<IInventorySlot, MonoBehaviour>> inventorySlots;
        [SerializeField]
        private InterfaceReference<IInventorySlot, MonoBehaviour> currentItem;
        [SerializeField]
        private InterfaceReference<IInventorySlot, MonoBehaviour> newItem;

        [SerializeField, Range(0f, 5f)]
        private float tweenDuration = 1f;

        public event IUISystem.EquipItemHandle EquipItemEvent;
        public event IUISystem.DropItemHandle DropItemEvent;

        public void SetCoinsValueText(int value)
        {
            coinsValueText.text = value.ToString();
        }

        public void CreateCoin(int newValue, bool isNewItem)
        {
            Vector2 spawnPosition = isNewItem ? 
                newItem.UnderlyingValue.gameObject.transform.position : 
                currentItem.UnderlyingValue.gameObject.transform.position;

            AnimateCoinFlight(Instantiate(coinPrefab, uICanvas), spawnPosition, target.position, newValue);
        }

        public void SetInventorySlotValue(Sprite sprite, int slotId, int slotLevel)
        {
            IInventorySlot inventorySlot = inventorySlots[slotId].Value;
            inventorySlot.ActivateSlotImage(slotLevel > 0);
            inventorySlot.SetSlotSprite(sprite);
            inventorySlot.SetSlotLevel(slotLevel);
        }

        public void ActivateLootPanel()
        {
            lootPanel.SetActive(true);
        }

        public void DeactivateLootPanel()
        {
            lootPanel.SetActive(false);
        }

        public void SetCurrentItemValue(Sprite sprite, int itemLevel)
        {
            IInventorySlot inventorySlot = currentItem.Value;
            inventorySlot.ActivateSlotImage(itemLevel > 0);
            inventorySlot.SetSlotSprite(sprite);
            inventorySlot.SetSlotLevel(itemLevel);
        }

        public void SetNewItemValue(Sprite sprite, int itemLevel)
        {
            IInventorySlot inventorySlot = newItem.Value;
            inventorySlot.ActivateSlotImage(itemLevel > 0);
            inventorySlot.SetSlotSprite(sprite);
            inventorySlot.SetSlotLevel(itemLevel);
        }

        public void OnDropButtonClick()
        {
            DropItemEvent?.Invoke();
            DeactivateLootPanel();
        }

        public void OnEquipButtonClick()
        {
            EquipItemEvent?.Invoke();
            DeactivateLootPanel();
        }

        private void AnimateCoinFlight(Transform image, Vector2 spawnPosition, Vector2 targetPosition, int newValue)
        {
            image.transform.position = spawnPosition;
            image.DOBlendableLocalRotateBy(new Vector3(0, 0, 360), tweenDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
            image.DOMoveX(targetPosition.x, tweenDuration).SetEase(Ease.OutQuad);
            image.DOMoveY(targetPosition.y, tweenDuration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                SetCoinsValueText(newValue);
                DOTween.Kill(image);
                Destroy(image.gameObject);
            });
        }
    }
}
