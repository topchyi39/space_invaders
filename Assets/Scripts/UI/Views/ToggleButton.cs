using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Button button;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite toggledSprite;

        private bool _isDefault;
        private Action _defaultAction;
        private Action _toggledAction;

        public bool Value => _isDefault;

        private void Start()
        {
            button.onClick.AddListener(OnClick);
            UpdateSprite();
        }

        private void OnClick()
        {
            var onClickAction = _isDefault ? _defaultAction : _toggledAction;
            _isDefault = !_isDefault;
            UpdateSprite();
            onClickAction?.Invoke();
        }
        
        public void SetValue(bool value, bool withAction)
        {
            if(withAction)
            {
                var onClickAction = value ? _defaultAction : _toggledAction;
                onClickAction?.Invoke();
            }
            
            _isDefault = value;
            UpdateSprite();
        }

        public void AddListener(Action action)
        {
            _defaultAction = action;
            _toggledAction = action;
        }

        private void UpdateSprite()
        {
            icon.sprite = _isDefault ? defaultSprite : toggledSprite;
        }
    }
}