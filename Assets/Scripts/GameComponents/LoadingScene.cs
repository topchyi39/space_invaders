using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameComponents
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Transform target;
        [SerializeField] private float duration;
        

        private IEnumerator Start()
        {
            target.DOScale(1.2f, duration);
            canvasGroup.DOFade(1f, duration);
            yield return new WaitForSeconds(duration / 2f);
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }
}