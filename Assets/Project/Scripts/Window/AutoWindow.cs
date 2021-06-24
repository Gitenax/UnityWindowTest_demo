using System;
using UnityEngine;

namespace Project.Scripts.Window
{
    public class AutoWindow : Window
    {
        [SerializeField] private Canvas _canvas;

        protected override void OnDestroy()
        {
            if (_isApplicationQuit)
                return;
            
            Observer.AllWindowsClosed -= ObserverOnAllWindowsClosed;
            base.OnDestroy();
        }

        public void ShowWindow()
        {
            _canvas = FindObjectOfType<Canvas>();
            if(_canvas == false)
                throw new ArgumentNullException($"На сцене найден компонент {nameof(Canvas)} для отрисовки окна");
            
            CreateWindow();
        }
        
        private void CreateWindow()
        {
            if (Observer.ActiveWindows.Count > 0)
                Observer.AllWindowsClosed += ObserverOnAllWindowsClosed;
            else
                InstantiateWindow();
        }
        
        private void ObserverOnAllWindowsClosed()
        {
            InstantiateWindow();
            Observer.AllWindowsClosed -= ObserverOnAllWindowsClosed;
        }

        private void InstantiateWindow()
        {
            Instantiate(this, _canvas.transform.position, Quaternion.identity, _canvas.transform);
        }
    }
}