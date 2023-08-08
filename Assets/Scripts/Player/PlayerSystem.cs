using UnityEngine;
using Zenject;

using SPSDigital.UI;

namespace SPSDigital.Player
{
    public class PlayerSystem : MonoBehaviour, IPlayerSystem
    {
        [SerializeField]
        private int coinsValue = 0;

        [Inject]
        private IUISystem uISystem;

        private void Awake()
        {
            uISystem.SetCoinsValueText(coinsValue);
        }

        public void AddCoins(int value, Vector2 spawnPosition)
        {
            coinsValue += value;
            uISystem.CreateFlyingCoin(coinsValue, spawnPosition);
        }
    }
}
