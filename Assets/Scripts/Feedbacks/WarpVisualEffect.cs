using System.Collections;
using MoreMountains.Feedbacks;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.VFX;

namespace Feedbacks
{
    [AddComponentMenu("")]
    [FeedbackPath("VisualEffect/VFXPlayFeedback")]
    public class WarpVisualEffect : MMF_Feedback
    {
        [MMFInspectorGroup("Target")] 
        public VisualEffect effect;

        [MMFInspectorGroup("Curves", true)] 
        public float duration;
        public AnimationCurve warpAmountCurve;
        
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
           effect.Play();
           effect.SetFloat("WarpAmount", 0);
           Owner.StartCoroutine(PlayWarpCoroutine());
        }

        protected IEnumerator PlayWarpCoroutine()
        {
            var time = 0f;
            while (time <= duration)
            {
                var evaluateValue = warpAmountCurve.Evaluate(time / duration);
                effect.SetFloat("WarpAmount", evaluateValue);
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            effect.SetFloat("WarpAmount", warpAmountCurve.Evaluate(1f));
        }

        protected override void CustomReset()
        {
            base.CustomReset();
            effect.SetFloat("WarpAmount", 0);
            effect.Stop();
        }
    }
}