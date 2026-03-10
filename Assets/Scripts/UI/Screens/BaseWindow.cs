using Enums;
using Interfaces;
using UnityEngine;

namespace UI.Screens
{
    public abstract class BaseWindow : MonoBehaviour, IWindowTyped
    {
        [SerializeField] private UIWindowType _type;

        public UIWindowType Type => _type;

        public abstract void Init();

        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
    }
}