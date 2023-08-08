using UnityEngine;
using System.Collections.Generic;
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
        }

        public void AddCoins(int value, Vector2 spawnPosition)
        {
            coinsValue += value;
            uISystem.CreateFlyingCoin(coinsValue, spawnPosition);
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                uISystem.SetInventorySlotValue(i, 1);
            }
        }

        public void ActivateLoot()
        {
            if(!isLootActivated)
            {
                isLootActivated = true;
                uISystem.ActivateLootPanel();
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
