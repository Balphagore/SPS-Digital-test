using UnityEngine;

namespace SPSDigital.UI
{
    public interface IUISystem
    {
        void SetCoinsValueText(int value);

        void CreateFlyingCoin(int newValue, Vector2 spawnPosition);
    }
}
