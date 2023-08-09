using System;
using System.Collections;
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
        [SerializeField]
        private int currentItemIndex = -1;
        [SerializeField]
        private int newItemLevel = 0;
        [SerializeField]
        private List<(int value, bool isNewItem)> delayedCoins;
        [SerializeField]
        private int consecutiveCount = 0;

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
                uISystem.SetInventorySlotValue(null, i, inventorySlots[i].Level);
                uISystem.SetStatValue(i, inventorySlots[i].StatName + ": " + inventorySlots[i].Level);
            }
            uISystem.SetLootItemValue(null, 0, null, false, false);
            uISystem.SetLootItemValue(null, 0, null, true, false);
            delayedCoins = new();
        }

        public void AddCoins(int value, Vector2 spawnPosition)
        {
            coinsValue += value;
            uISystem.CreateCoin(coinsValue, false);
        }

        public void ActivateLoot()
        {
            if (!isLootActivated)
            {
                isLootActivated = true;
                uISystem.ActivateLootPanel();

                int itemIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EItemType)).Length);

                if (itemIndex == currentItemIndex)
                {
                    consecutiveCount++;
                }
                else
                {
                    consecutiveCount = 1;
                }

                if (consecutiveCount >= 4)
                {
                    while (itemIndex == currentItemIndex)
                    {
                        itemIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EItemType)).Length);
                    }
                    consecutiveCount = 1;
                }

                currentItemIndex = itemIndex;
                InventorySlotDataModel inventorySlotData = inventorySlots[itemIndex];

                uISystem.SetLootItemValue(inventorySlotData.Sprite, inventorySlotData.Level, inventorySlotData.StatName, false, false);

                int itemLevel = UnityEngine.Random.Range(1, 16);
                newItemLevel = itemLevel;

                uISystem.SetLootItemValue(inventorySlotData.Sprite, itemLevel, inventorySlotData.StatName, true, itemLevel > inventorySlotData.Level);
            }
        }

        private void OnDropItemEvent()
        {
            for (int i = 0; i < newItemLevel; i++)
            {
                coinsValue++;
                delayedCoins.Add((coinsValue,true));
            }
            StartCoroutine(DelayedCoinCreation());
        }

        private void OnEquipItemEvent()
        {
            for (int i = 0; i < inventorySlots[currentItemIndex].Level; i++)
            {
                coinsValue++;
                delayedCoins.Add((coinsValue, false));
            }
            StartCoroutine(DelayedCoinCreation());

            inventorySlots[currentItemIndex].Level = newItemLevel;
            uISystem.SetInventorySlotValue(inventorySlots[currentItemIndex].Sprite, currentItemIndex, newItemLevel);
            uISystem.SetStatValue(currentItemIndex, inventorySlots[currentItemIndex].StatName + ": " + inventorySlots[currentItemIndex].Level);
        }

        private IEnumerator DelayedCoinCreation()
        {
            while(delayedCoins.Count > 0)
            {
                uISystem.CreateCoin(delayedCoins[0].value, delayedCoins[0].isNewItem);
                delayedCoins.RemoveAt(0);
                yield return new WaitForSeconds(0.1f);
            }
            isLootActivated = false;
            uISystem.DeactivateLootPanel();
        }
    }
}
