using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class TextCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_Text textClone;
        [SerializeField] private Button testButton;
        [SerializeField] private Button decreaseTestButton;
        [SerializeField] private float counterDuration;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private RectTransform _textCloneRect;
        private int _currentValue;
        private bool _sequenceEnded;
        private Sequence _sequence;
        private Queue<int> _values = new();

        private void Awake()
        {
            _textCloneRect = textClone.rectTransform;
            _textCloneRect.anchoredPosition += Vector2.down * _textCloneRect.sizeDelta.y;
            canvasGroup.DOFade(0, 0);
            testButton.onClick.AddListener(IncreaseValue5);
            decreaseTestButton.onClick.AddListener(DecreaseValue5);
        }

        public void InitializeValue(int value)
        {
            _currentValue = value;
            text.text = _currentValue.ToString();
        }

        public void UpdateValue(int value)
        {
            _values.Enqueue(value);
         
            if (value > _currentValue)
            {
                IncreaseValue(value);
            }
            else
            {
                DecreaseValue(value);
            }
        }

        private void IncreaseValue5()
        {
            IncreaseValue(_currentValue + 5);
        }
        
        private void DecreaseValue5()
        {
            DecreaseValue(_currentValue - 5);
        }

        private void IncreaseValue(int value)
        {
            _textCloneRect.anchoredPosition =
                text.rectTransform.anchoredPosition + Vector2.down * (_textCloneRect.sizeDelta.y * 2);
            textClone.text = $"+{value - _currentValue}";
            var sequence = CreateSequence(text.rectTransform.anchoredPosition);
            sequence.AppendCallback(() => StartCoroutine(ChangeValue(_currentValue, value, counterDuration)));
        }

        private void DecreaseValue(int value)
        {
            _textCloneRect.anchoredPosition =
                text.rectTransform.anchoredPosition;
            textClone.text = $"{value - _currentValue}";
            StartCoroutine(ChangeValue(_currentValue, value, counterDuration));
            CreateSequence(text.rectTransform.anchoredPosition + Vector2.down * (_textCloneRect.sizeDelta.y * 2));
        }

        private Sequence CreateSequence(Vector2 endPosition)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => canvasGroup.DOFade(1f, 0.2f));
            sequence.Append(_textCloneRect.DOAnchorPos(text.rectTransform.anchoredPosition +
                                                       Vector2.down * _textCloneRect.sizeDelta.y, 0.2f));
            sequence.AppendInterval(0.5f);
            sequence.AppendCallback(() => _textCloneRect.DOAnchorPos(endPosition, 0.2f));
            sequence.Append(canvasGroup.DOFade(0, 0.2f));
            return sequence;
        }

        private IEnumerator ChangeValue(int from, int to, float duration)
        {
            var time = 0f;
            
            while (time <= duration)
            {
                var value = (int)Mathf.Lerp(from, to, time / duration);
                text.text = value.ToString();
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
            }
            
            text.text = to.ToString();
            _currentValue = to;
        }
    }
}