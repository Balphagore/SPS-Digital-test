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

        [Inject]
        private IUISystem uISystem;

        private void Awake()
        {
            uISystem.SetCoinsValueText(coinsValue);
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
    }
}
