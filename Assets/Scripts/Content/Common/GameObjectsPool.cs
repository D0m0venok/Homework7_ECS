using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Content
{
    public class GameObjectsPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _activeContainer;
        private readonly Transform _disableContainer;
        private readonly HashSet<T> _active = new();
        private readonly Queue<T> _disabled = new();
        private readonly int _maxSize;

        public int Count => _active.Count;
        public bool HasFreeObject => _active.Count < _maxSize;

        public GameObjectsPool(T prefab, Transform activeContainer = null, Transform disableContainer = null, int initSize = 0, int maxSize = int.MaxValue)
        {
            if (prefab == null)
                throw new NullReferenceException();
            
            _prefab = prefab;

            _activeContainer = activeContainer;
            _disableContainer = disableContainer;
            
            _maxSize = Mathf.Clamp(maxSize, 1, int.MaxValue);
            initSize = Mathf.Clamp(initSize, 0, _maxSize);

            for (var i = 0; i < initSize; i++)
            {
                var obj = Create();
                obj.transform.SetParent(_disableContainer);
                _disabled.Enqueue(obj);
            }
        }

        public virtual bool TryGet(out T obj)
        {
            obj = default;
            if (_active.Count == _maxSize)
                return false;
            else
                obj = GetOrCreate();
            
            return true;
        }
        public virtual T Get()
        {
            T obj;

            if (_active.Count == _maxSize)
                return null;
            else
                obj = GetOrCreate();
            
            return obj;
        }
        public virtual void Put(T obj)
        {
            if(!_active.Remove(obj))
                return;
            
            _disabled.Enqueue(obj);
            obj.transform.SetParent(_disableContainer);
        }

        private T GetOrCreate()
        {
            var obj = _disabled.Count > 0 ? _disabled.Dequeue() : Create();
            
            obj.transform.SetParent(_activeContainer);
            _active.Add(obj);

            return obj;
        }
        protected virtual T Create()
        {
            return Object.Instantiate(_prefab);
        }
    }
}