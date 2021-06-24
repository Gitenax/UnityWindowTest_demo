using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Window
{
    public sealed class WindowObserver : MonoBehaviour
    {
        [SerializeField] private List<Window> _activeWindows;

        public event Action AllWindowsClosed;
        
        public List<Window> ActiveWindows => _activeWindows;

        private void Awake()
        {
           _activeWindows = new List<Window>();
        }

        public void AddWindow(Window window)
        {
            _activeWindows.Add(window);
        }

        public void RemoveWindow(Window window)
        {
            _activeWindows.Remove(window);
            
            if(_activeWindows.Count == 0)
                AllWindowsClosed?.Invoke();
        }
    }
}