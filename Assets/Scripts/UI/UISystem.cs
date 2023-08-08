using DG.Tweening;
using UnityEngine;

namespace SPSDigital.UI
{
    public class UISystem : MonoBehaviour, IUISystem
    {
        [SerializeField]
        private Transform mainCanvas;
        [SerializeField]
        private Transform coinPrefab;
        [SerializeField]
        private Transform target;

        private void AnimateImageFlight(Transform image, Vector2 starSpawnPosition, Vector2 targetPosition)
        {
            image.transform.position = starSpawnPosition;
            image.DOBlendableLocalRotateBy(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
            image.DOMoveX(targetPosition.x, 2).SetEase(Ease.OutQuad);
            image.DOMoveY(targetPosition.y, 2).SetEase(Ease.InQuad).OnComplete(() =>
            {
                DOTween.Kill(image);
                Destroy(image.gameObject);
            });
        }

        public void CreateFlyingImage()
        {
            AnimateImageFlight(Instantiate(coinPrefab, mainCanvas), Vector2.zero, target.position);
        }
    }
}
