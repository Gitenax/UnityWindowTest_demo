using Project.Scripts.Window;
using UnityEngine;

namespace Project.Scripts.Button
{
    public class ButtonActions : MonoBehaviour
    {
        private Transform _parent;

        private void Awake()
        {
            _parent = transform.root;
        }
        
        public void ShowModal(ModalWindow windowPrefab)
        {
            Instantiate(windowPrefab, _parent.position, Quaternion.identity, _parent);
        }
    }
}
