using Attributes;
using MoreMountains.Feedbacks;
using UnityEngine;
using VisualEffects;

namespace Feedbacks
{
    [AddComponentMenu("")]
    [FeedbackHelp("You can add a description for your feedback here.")]
    [FeedbackPath("VisualEffect/VFXPlayFeedback")]
    public class VisualEffectFeedback : MMF_Feedback
    {
        [MMFInspectorGroup("Target")] 
        public Transform target;

        [MMFInspectorGroup("VFX Instancer", true)] 
        [Type(typeof(BasePoolEffect))] public string vfxType;
        public VisualEffectInstancer vfxInstancer;

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            vfxInstancer.SpawnAndPlayEffect<DestroyingEffect>(target.position, target.rotation);
            vfxInstancer.SpawnAndPlayEffect(vfxType, target.position, target.rotation);
        }
    }
}