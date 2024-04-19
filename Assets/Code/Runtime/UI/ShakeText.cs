using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI
{
    public class ShakeText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textToShake;
        [SerializeField] private float oneShakeDuration = 1f;
        [SerializeField] private float strengthShake = 3f;

        private Tweener _tweener;

        private void Awake()
        {
            _tweener = textToShake.transform
                .DORotate(new Vector3(0, 0, strengthShake), oneShakeDuration)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy() =>
            _tweener.Kill();
    }
}