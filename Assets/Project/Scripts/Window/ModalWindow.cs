using System;
using UnityEngine;

namespace Project.Scripts.Window
{
    public class ModalWindow : Window
    {
        [SerializeField] private SubModalWindow[] _subWindowPrefabs;
        private SubModalWindow _currentSubWindow;
        
        public SubModalWindow[] SubWindows => _subWindowPrefabs;

        protected override void OnDestroy()
        {
            if(_currentSubWindow == true)
                _currentSubWindow.WindowClosed -= CurrentSubWindowOnWindowClosed;
            
            base.OnDestroy();
        }
        
        public void CreateSubWindow(int subWindowIndex)
        {
            if (subWindowIndex < 0 || subWindowIndex >= _subWindowPrefabs.Length)
                throw new IndexOutOfRangeException("Указанный индекс вне границ массива");
            
            if(_currentSubWindow == true)
            {
                Destroy(_currentSubWindow.gameObject);
                _currentSubWindow = default;
            }

            _currentSubWindow = InstantiateWindow(_subWindowPrefabs[subWindowIndex]);
            _currentSubWindow.WindowClosed += CurrentSubWindowOnWindowClosed;
            
            gameObject.SetActive(false);
        }

        private SubModalWindow InstantiateWindow(SubModalWindow prefab)
        {
            return Instantiate( prefab, transform.position, Quaternion.identity, transform.parent);
        }
        
        private void CurrentSubWindowOnWindowClosed()
        {
            gameObject.SetActive(true);
            _currentSubWindow.WindowClosed -= CurrentSubWindowOnWindowClosed;
            _currentSubWindow = default;
        }
    }
}
