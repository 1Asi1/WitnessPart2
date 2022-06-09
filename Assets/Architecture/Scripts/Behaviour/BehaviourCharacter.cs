using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Architecture.Scripts.Behaviour.BehaviorCollection;
using System.Collections;

namespace Assets.Architecture.Scripts.Behaviour
{
    public class BehaviourCharacter : MonoBehaviour
    {
        public event Action ShotEvent;
        public event Action IsMoveEvent;
        public event Action IsStopEvent;
        public event Action<GameObject> EnemyDetectedEvent;

        [SerializeField] private string _nameCharacter;

        public string NameCharacter => _nameCharacter;

        private Dictionary<Type, IBehaviour> _behavioursMap;

        private IBehaviour _behaviourCurrent;

        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
            InltBehaviours();
            SetBahaviourByDefault();
        }

        private void InltBehaviours()
        {
            _behavioursMap = new Dictionary<Type, IBehaviour>();

            _behavioursMap[typeof(BehaviourAgressive)] = new BehaviourAgressive(gameObject, this);
            _behavioursMap[typeof(BehaviourPanic)] = new BehaviourPanic(gameObject);
            _behavioursMap[typeof(BehaviourIdle)] = new BehaviourIdle();
        }

        private void SetBahaviourByDefault()
        {
            var behaviourByDefault = GetBehaviour<BehaviourIdle>();
            SetBehaviour(behaviourByDefault);
        }

        private IBehaviour GetBehaviour<T>() where T : IBehaviour
        {
            var type = typeof(T);

            return _behavioursMap[type];
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
            EnemyDetectedEvent?.Invoke(obj);
        }

        public void Shoot()
        {
            ShotEvent?.Invoke();
        }

        public void IsMove()
        {
            IsMoveEvent?.Invoke();
        }

        public void IsStop()
        {
            IsStopEvent?.Invoke();
        }
    }
}
