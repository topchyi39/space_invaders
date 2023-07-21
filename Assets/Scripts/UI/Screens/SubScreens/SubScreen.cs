using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.SubScreens
{
    public class SubScreen : MonoBehaviour
    {
        [SerializeField] private bool hideOnAwake = true;
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        
        private bool _validated;

        protected void OnValidate()
        {
            if (_validated) return;
            
            canvas ??= GetComponent<Canvas>();
            canvasGroup ??= GetComponent<CanvasGroup>();
            graphicRaycaster ??= GetComponent<GraphicRaycaster>();

            _validated = true;
        }
        private void Awake()
        {
            if (hideOnAwake) Hide();
        }

        public void Show()
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
            
            graphicRaycaster.enabled = true;
            canvas.enabled = true;
            canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
            canvas.enabled = false;
            graphicRaycaster.enabled = false;
        }
    }
}