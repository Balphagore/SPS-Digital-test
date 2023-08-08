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
            }
            uISystem.SetCurrentItemValue(null, 0);
            uISystem.SetNewItemValue(null, 0);
            delayedCoins = new();
        }

        public void AddCoins(int value, Vector2 spawnPosition)
        {
            coinsValue += value;
            uISystem.CreateCoin(coinsValue, false);
        }

        public void ActivateLoot()
        {
            if(!isLootActivated)
            {
                isLootActivated = true;
                uISystem.ActivateLootPanel();
                int itemIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(EItemType)).Length);
                currentItemIndex = itemIndex;

                uISystem.SetCurrentItemValue(inventorySlots[itemIndex].Sprite, inventorySlots[itemIndex].Level);

                int itemLevel = UnityEngine.Random.Range(1, 16);
                newItemLevel = itemLevel;

                uISystem.SetNewItemValue(inventorySlots[itemIndex].Sprite, itemLevel);
            }
        }

        private void OnDropItemEvent()
        {
            isLootActivated = false;
            for (int i = 0; i < newItemLevel; i++)
            {
                coinsValue++;
                delayedCoins.Add((coinsValue,true));
            }
            StartCoroutine(DelayedCoinCreation());
        }

        private void OnEquipItemEvent()
        {
            isLootActivated = false;
            for (int i = 0; i < inventorySlots[currentItemIndex].Level; i++)
            {
                coinsValue++;
                delayedCoins.Add((coinsValue, false));
            }
            StartCoroutine(DelayedCoinCreation());

            inventorySlots[currentItemIndex].Level = newItemLevel;
            uISystem.SetInventorySlotValue(inventorySlots[currentItemIndex].Sprite, currentItemIndex, newItemLevel);
        }

        private IEnumerator DelayedCoinCreation()
        {
            while(delayedCoins.Count > 0)
            {
                uISystem.CreateCoin(delayedCoins[0].value, delayedCoins[0].isNewItem);
                delayedCoins.RemoveAt(0);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
