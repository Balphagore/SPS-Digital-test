using UnityEngine;
using Zenject;

using SPSDigital.UI;

namespace SPSDigital.Characters
{
    public class CharactersSystem : MonoBehaviour, ICharactersSystem
    {
        [Inject]
        private IUISystem uISystem;

        public void PlayerCharacterAction()
        {
            uISystem.CreateFlyingImage();
        }
    }
}
