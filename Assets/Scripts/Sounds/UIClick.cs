using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Zenject;

namespace Sounds
{
    [Serializable]
    public class Event
    {
        [SerializeField] private EventTrigger.Entry entry;
        [SerializeField] private UISoundType soundType;

        public EventTrigger.Entry Entry => entry;
        public UISoundType SoundType => soundType;
    }
    
    [RequireComponent(typeof(EventTrigger))]
    public class UIClick : MonoBehaviour
    {
        [SerializeField] private Event[] events;
        [SerializeField] private EventTrigger eventTrigger;

        private SoundManager _soundManager;
        
        [Inject]
        private void Construct(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        private void Awake()
        {
            eventTrigger ??= GetComponent<EventTrigger>();

            foreach (var triggerEvents in events)
            {
                var entry = triggerEvents.Entry;
                entry.callback.AddListener(eventData => PlayClick(triggerEvents.SoundType));
                eventTrigger.triggers.Add(entry);
            }
        }

        private void PlayClick(UISoundType soundType)
        {
            _soundManager.PlaySound<UISound>(new UISoundData{Type = soundType});
        }
    }
}