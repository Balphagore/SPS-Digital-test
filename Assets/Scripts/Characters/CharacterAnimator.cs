using UnityEngine;

namespace SPSDigital.Characters
{
    public class CharacterAnimator : MonoBehaviour, ICharacterAnimator
    {
        [SerializeField]
        private Animator animator;

        public void SetAttackTrigger()
        {
            animator.SetTrigger("AttackTrigger");
        }
    }
}
