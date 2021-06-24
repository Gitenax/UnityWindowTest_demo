using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Project.Scripts.Runtime
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _withinFrom = 10f;
        [SerializeField] private float _withinTo = 20f;
        [SerializeField] private float _duration;
        [SerializeField] private float _current;
        [SerializeField] private bool _disableUponSuccess;
        private bool _performed;
        
        public UnityEvent TimerExpired;

        private void Awake()
        {
            _duration = Random.Range(_withinFrom, _withinTo);
        }

        private void Update()
        {
            if(_performed && _disableUponSuccess)
            {
                gameObject.SetActive(false);
                return;
            }
            
            if (_current >= _duration)
            {
                TimerExpired.Invoke();
                _performed = true;
                _current = 0f;
            }

            _current += Time.deltaTime;
        }
    }
}