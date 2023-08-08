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

        [SerializeField, Range(0f, 5f)]
        private float tweenDuration = 2f;

        public event IUISystem.EquipItemHandle EquipItemEvent;
        public event IUISystem.DropItemHandle DropItemEvent;

        public void SetCoinsValueText(int value)
        {
            coinsValueText.text = value.ToString();
        }

        public void CreateFlyingCoin(int newValue, Vector2 position)
        {
            AnimateCoinFlight(Instantiate(coinPrefab, uICanvas), position, target.position, newValue);
        }

        public void SetInventorySlotValue(int slotId, int slotLevel)
        {
            IInventorySlot inventorySlot = inventorySlots[slotId].Value;
            inventorySlot.ActivateSlotImage(slotLevel > 0);
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
