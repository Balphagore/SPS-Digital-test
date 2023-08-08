using UnityEngine;
using UnityEngine.EventSystems;
using AYellowpaper;
using Zenject;

namespace SPSDigital.Characters
{
    public class Character : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private InterfaceReference<ICharacterAnimator,MonoBehaviour> characterAnimator;

        [Inject]
        private ICharactersSystem charactersSystem;

        public void OnPointerDown(PointerEventData eventData)
        {
            characterAnimator.Value.SetAttackTrigger();
            charactersSystem.PlayerCharacterAction(transform.position);
        }
    }
}
