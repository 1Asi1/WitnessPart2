using System;
using UnityEngine;

namespace Assets.Architecture.Scripts.Behaviour.BehaviorCollection
{
    public class BehaviourAgressive : GeneralBehaviour
    {
        private GameObject _currentGameObject;
        private GameObject _targetGameObject;

        private BehaviourCharacter _currentScript;

        private float _speed = 0.5f * Time.deltaTime;
        private float _minDistance = 2f;
        private float _maxDistance = 7f;

        private bool _isCharge = false;

        public override void Enter()
        {
            _currentScript.EnemyDetectedEvent += SetEnemy;
            _currentScript.ShotEvent += Shot;
        }

        public override void Update()
        {
            Pursue();
            LookToPlayer();
        }

        private void Pursue()
        {
            if (_targetGameObject != null)
            {
                if (GetDistance() >= _maxDistance)
                {
                    _currentGameObject.transform.position = Vector2.MoveTowards(
                                        _currentGameObject.transform.position,
                                        _targetGameObject.transform.position,
                                        _speed);
                    _currentScript.IsMove();
                }

                else if (GetDistance() <= _minDistance)
                {
                    _currentGameObject.transform.position = Vector2.MoveTowards(
                                        _currentGameObject.transform.position,
                                        _targetGameObject.transform.position,
                                        _speed * -1);
                    _currentScript.IsMove();
                }

                else
                {
                    if (!_isCharge)
                    {
                        _currentScript.IsStop();
                        _isCharge = true;
                    }
                }
            }
        }

        private float GetDistance()
        {
            return Vector2.Distance(_currentGameObject.transform.position, _targetGameObject.transform.position);
        }

        private void LookToPlayer()
        {
            if (_targetGameObject != null)
            {
                Vector2 diraction = _targetGameObject.transform.position - _currentGameObject.transform.position;
                var targetAngle = Mathf.Atan2(diraction.y, diraction.x) * Mathf.Rad2Deg;
                _currentGameObject.transform.rotation = Quaternion.Euler(0, 0, targetAngle);
            }
        }

        public BehaviourAgressive(GameObject gameObject, BehaviourCharacter currentScript)
        {
            _currentGameObject = gameObject;
            _currentScript = currentScript;
        }

        private void SetEnemy(GameObject playerGameObject)
        {
            _targetGameObject = playerGameObject;
        }

        private void Shot()
        {
            _isCharge = false;
        }
    }
}
