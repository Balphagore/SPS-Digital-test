using UnityEngine;
using Zenject;

using SPSDigital.Player;

namespace SPSDigital.Characters
{
    public class CharactersSystem : MonoBehaviour, ICharactersSystem
    {
        [Inject]
        private IPlayerSystem playerSystem;

        public void PlayerCharacterAction(Vector2 position)
        {
            playerSystem.AddCoins(1, position);
            playerSystem.ActivateLoot();
        }
    }
}
