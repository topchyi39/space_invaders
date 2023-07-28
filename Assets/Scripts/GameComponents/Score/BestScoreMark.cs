using DG.Tweening;
using UnityEngine;

namespace GameComponents.Score
{
    public class BestScoreMark : MonoBehaviour
    {
        [SerializeField] private GameObject markObject;

        public void SetMark(bool state)
        {
            markObject.SetActive(state);

            if (state)
            {
                var sequence = DOTween.Sequence();
                sequence.Append(transform.DOScale(Vector3.one * 1.2f, 0.5f).SetEase(Ease.InBack));
                sequence.AppendInterval(0.2f);
                sequence.SetLoops(10, LoopType.Yoyo);
            }
        }
        
    }
}