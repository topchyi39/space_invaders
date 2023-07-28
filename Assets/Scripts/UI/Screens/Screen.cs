using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public interface IViewModel
    {
        event Action OnDataChanged;
    }

    public abstract class BaseScreen : MonoBehaviour
    {        
        [SerializeField] private bool hideOnAwake = true;

        protected UIManager _uiManager;
        
        [Inject]
        private void BaseConstruct(UIManager uiManager)
        {
            _uiManager = uiManager;
            _uiManager.AddScreen(this);
            if (hideOnAwake) Hide();
        }
        
        public abstract Type ViewModelType { get; }
        public abstract void Show();
        public abstract void Hide();
        public abstract void Bind<T>(T viewModel) where T : class, IViewModel;
    }
    
    
    public abstract class Screen<TViewModel> : BaseScreen where TViewModel : class, IViewModel
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        
        [Space]

        protected TViewModel ViewModel;
        private bool _validated;
        public override Type ViewModelType => typeof(TViewModel);

        protected void OnValidate()
        {
            if (_validated) return;
            
            canvas ??= GetComponent<Canvas>();
            canvasGroup ??= GetComponent<CanvasGroup>();
            graphicRaycaster ??= GetComponent<GraphicRaycaster>();

            _validated = true;
        }

        public override void Show()
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
            
            canvas.enabled = true;
            graphicRaycaster.enabled = true;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            ShowCallback();
        }

        public override void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvas.enabled = false;
            graphicRaycaster.enabled = false;
            HideCallback();
        }
 
        public override void Bind<T>(T viewModel)
        {
            if (typeof(T) != ViewModelType) throw new InvalidOleVariantTypeException();

            ViewModel = viewModel as TViewModel;
            ViewModel.OnDataChanged += OnDataChanged;
            BindCallback(ViewModel);
        }

        protected virtual void ShowCallback() { }
        protected virtual void HideCallback() { }
        protected virtual void OnDataChanged() {}
        protected virtual void BindCallback(TViewModel viewModel) { }
    }
}