using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Zenject;

namespace UI.Popup
{
    public class PopupService : MonoBehaviour
    {
        private readonly Transform _popupRoot;
        private readonly FloatingPopup _popupPrefab;
        private readonly List<FloatingPopupConfig> _configs;

        private readonly Dictionary<FloatingPopupType, FloatingPopupConfig> _configLookup;
        private readonly Dictionary<System.Type, Queue<IPopup>> _pool = new();
        
        [Inject]
        public PopupService(Transform uiRoot, FloatingPopup popupPrefab, List<FloatingPopupConfig> configs)
        {
            _popupRoot = uiRoot;
            _popupPrefab = popupPrefab;
            _configs = configs;

            _configLookup = new Dictionary<FloatingPopupType, FloatingPopupConfig>();
            
            foreach (var cfg in configs)
            {
                _configLookup[cfg.type] = cfg;
            }
        }
        
        /*public void Show<TData>(IPopup prefab, TData data) where TData : PopupData
        {
            var type = prefab.GetType();

            if (!_pool.TryGetValue(type, out var queue))
            {
                queue = new Queue<IPopup>();
                _pool[type] = queue;
            }

            IPopup popup = queue.Count > 0 ? queue.Dequeue() : Instantiate(prefab as MonoBehaviour, _popupRoot) as IPopup;
            popup.Show(data, p => queue.Enqueue(p));
        }*/
        private Dictionary<Type, BasePopup<PopupData>> _prefabLookup = new();
        private Dictionary<Type, Queue<BasePopup<PopupData>>> _pools = new();
        
        
        public void Show<TData>(TData data) where TData : PopupData
        {
            Debug.Log("PopupService.Show");
            
            Type dataType = typeof(TData);

            if (!_prefabLookup.TryGetValue(dataType, out var prefab))
            {
                Debug.LogWarning($"Popup для {dataType} не найден!");
                return;
            }

            var pool = _pools[prefab.GetType()];
            BasePopup<PopupData> instance = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab, _popupRoot);

            // Кастим к BasePopup<TData>
            (instance as BasePopup<TData>).Show(data, p => pool.Enqueue(instance));
            
        }
    }
}