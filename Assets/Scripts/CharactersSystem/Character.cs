using UnityEngine;
using UnityEngine.EventSystems;
using Spine.Unity;

namespace SPSDigital.Characters
{
    public class Character : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private SkeletonAnimation skeletonAnimation;

        public void OnPointerDown(PointerEventData eventData)
        {
            Spine.AnimationState animationState = skeletonAnimation.AnimationState;

            animationState.SetAnimation(0, "jump", true);
        }
    }
}
