using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Architecture.Scripts.Behaviour.BehaviorCollection;
using System.Collections;
using UnityEditor;

namespace Assets.Architecture.Scripts.Behaviour
{
    public abstract class Character : MonoBehaviour
    {
        public event Action IsMoveEvent;
        public event Action<GameObject> EnemyDetected;

        [SerializeField] private string _nameCharacter;

        public string NameCharacter => _nameCharacter;

        protected Dictionary<Type, IBehaviour> _behavioursMap;

        private IBehaviour _behaviourCurrent;

        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
            _behavioursMap = new Dictionary<Type, IBehaviour>();

            InltBehaviours();
            SetBahaviourByDefault();
        }

        protected abstract void InltBehaviours();

        private void SetBahaviourByDefault()
        {
            var behaviourByDefault = GetBehaviour<BehaviourIdle>();
            SetBehaviour(behaviourByDefault);
        }

        public T GetBehaviour<T>() where T : IBehaviour
        {
            var type = typeof(T);

            return (T) _behavioursMap[type];
        }

        private void SetBehaviour(IBehaviour newBehaviour)
        {
            if (_behaviourCurrent != null)
            {
                _behaviourCurrent.Exit();
            }

            _behaviourCurrent = newBehaviour;
            _behaviourCurrent.Enter();
        }

        protected virtual void Update()
        {
            if (_behaviourCurrent != null)
            {
                _behaviourCurrent.Update();
            }
        }

        public void SwitchBehavior<T>() where T : IBehaviour
        {
            var behaviour = GetBehaviour<T>();
            SetBehaviour(behaviour);
        }

        public void TakeEnemy(GameObject obj)
        {
            EnemyDetected?.Invoke(obj);
        }

        public void IsMove()
        {
            IsMoveEvent?.Invoke();
        }
    }
}
