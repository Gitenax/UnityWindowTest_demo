using System;
using UnityEngine;

namespace Project.Scripts.Window
{
    public abstract class Window : MonoBehaviour
    {
        protected bool _isApplicationQuit;
        private WindowObserver _observer;
        
        public event Action WindowClosed;

        protected WindowObserver Observer
        {
            get
            {
                if(_observer == false)
                    _observer = GetObserverOnScene();

                return _observer;
            }
        }

        protected virtual void Awake()
        {
            Observer.AddWindow(this);
        }
        
        protected virtual void OnDestroy()
        {
            if (_isApplicationQuit)
                return;
            
            Observer.RemoveWindow(this);
            WindowClosed?.Invoke();
        }

        private void OnApplicationQuit()
        {
            _isApplicationQuit = true;
        }

        public void Close()
        {
            Destroy(gameObject);
        }
        
        protected WindowObserver GetObserverOnScene()
        {
            if (_isApplicationQuit)
                return default;
            
            var observer = FindObjectOfType<WindowObserver>();
            if(observer == false)
                throw new ArgumentNullException($"Не найден компонент {nameof(WindowObserver)} для работы с окнами.");
            
            return observer;
        }
    }
}