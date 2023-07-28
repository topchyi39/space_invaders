using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MenuComponents
{
    public class ToGameState : ParallaxState
    {
        public override void Enter()
        {
            base.Enter();
            var playerFeedbacks = Target.GetComponentInChildren<MMF_Player>();
            Target.localPosition = Vector3.zero;
            Target.localScale = Vector3.one;
            var sequence = DOTween.Sequence();
            Target.DOLocalRotate(new Vector3(-70, 0, 0), 0.5f).SetEase(Ease.InOutBack);
            SkyboxSettings.SetAngleMultiplier(0f);
            sequence.AppendInterval(0.2f);
            sequence.AppendCallback(playerFeedbacks.PlayFeedbacks);
            sequence.AppendInterval(2f);
            sequence.AppendCallback(() => StateMachine.GameStartAction?.Invoke());
        }
    }
}