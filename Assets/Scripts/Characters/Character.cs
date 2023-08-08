using UnityEngine;
using UnityEngine.EventSystems;
using AYellowpaper;

namespace SPSDigital.Characters
{
    public class Character : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private InterfaceReference<ICharacterAnimator,MonoBehaviour> characterAnimator;

        public void OnPointerDown(PointerEventData eventData)
        {
            characterAnimator.Value.SetAttackTrigger();
        }
    }
}
