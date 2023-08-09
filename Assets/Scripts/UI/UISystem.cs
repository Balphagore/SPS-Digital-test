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
        private TextMeshProUGUI currentItemStatText;
        [SerializeField]
        private InterfaceReference<IInventorySlot, MonoBehaviour> newItem;
        [SerializeField]
        private TextMeshProUGUI newItemStatText;
        [SerializeField]
        private GameObject greenArrow;
        [SerializeField]
        private GameObject redArrow;
        [SerializeField]
        private List<TextMeshProUGUI> playerStats;

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

        public void SetInventorySlotValue(Sprite sprite, int slotId, int slotLevel, string type)
        {
            IInventorySlot inventorySlot = inventorySlots[slotId].Value;
            inventorySlot.ActivateSlotImage(slotLevel > 0);
            inventorySlot.SetSlotSprite(sprite);
            inventorySlot.SetSlotLevel(slotLevel, type);
        }

        public void ActivateLootPanel()
        {
            lootPanel.SetActive(true);
        }

        public void DeactivateLootPanel()
        {
            lootPanel.SetActive(false);
        }

        public void SetLootItemValue(Sprite sprite, int itemLevel, string statName, bool isNewItem, bool isBetter, string type)
        {
            IInventorySlot inventorySlot = isNewItem ? newItem.Value : currentItem.Value;
            inventorySlot.ActivateSlotImage(itemLevel > 0);
            inventorySlot.SetSlotSprite(sprite);
            inventorySlot.SetSlotLevel(itemLevel, type);
            if (isNewItem)
            {
                newItemStatText.text = statName + ": " + itemLevel;
                greenArrow.SetActive(isBetter);
                redArrow.SetActive(!isBetter);
            }
            else
            {
                currentItemStatText.text = statName + ": " + itemLevel;
            }
        }

        public void OnDropButtonClick()
        {
            DropItemEvent?.Invoke();
        }

        public void OnEquipButtonClick()
        {
            EquipItemEvent?.Invoke();
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

        public void SetStatValue(int statId, string statValue)
        {
            playerStats[statId].text = statValue;
        }
    }
}
