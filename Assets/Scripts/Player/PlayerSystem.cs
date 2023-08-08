using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using SPSDigital.UI;

namespace SPSDigital.Player
{
    public class PlayerSystem : MonoBehaviour, IPlayerSystem
    {
        [SerializeField]
        private int coinsValue = 0;
        [SerializeField]
        List<InventorySlotDataModel> inventorySlots;
        [SerializeField]
        private bool isLootActivated;

        [Inject]
        private IUISystem uISystem;

        private void OnEnable()
        {
            uISystem.DropItemEvent += OnDropItemEvent;
            uISystem.EquipItemEvent += OnEquipItemEvent;
        }

        private void OnDisable()
        {
            uISystem.DropItemEvent -= OnDropItemEvent;
            uISystem.EquipItemEvent -= OnEquipItemEvent;
        }

        private void Awake()
        {
            uISystem.SetCoinsValueText(coinsValue);
            uISystem.DeactivateLootPanel();
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                uISystem.SetInventorySlotValue(i, inventorySlots[i].Level);
            }
            uISystem.SetCurrentItemValue(null, 0);
            uISystem.SetNewItemValue(null, 0);
        }

        public void AddCoins(int value, Vector2 spawnPosition)
        {
            coinsValue += value;
            uISystem.CreateFlyingCoin(coinsValue, spawnPosition);
        }

        public void ActivateLoot()
        {
            if(!isLootActivated)
            {
                isLootActivated = true;
                uISystem.ActivateLootPanel();
                int itemIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EItemType)).Length);

                uISystem.SetCurrentItemValue(inventorySlots[itemIndex].Sprite, inventorySlots[itemIndex].Level);

                int itemLevel = UnityEngine.Random.Range(1, 16);

                uISystem.SetNewItemValue(inventorySlots[itemIndex].Sprite, itemLevel);
            }
        }

        private void OnDropItemEvent()
        {
            isLootActivated = false;
        }

        private void OnEquipItemEvent()
        {
            isLootActivated = false;
        }
    }
}
