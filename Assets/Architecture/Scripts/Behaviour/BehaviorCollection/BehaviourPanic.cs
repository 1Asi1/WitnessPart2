using UnityEngine;

namespace Assets.Architecture.Scripts.Behaviour.BehaviorCollection
{
    public class BehaviourPanic : GeneralBehaviour
    {
        private GameObject currentGameObject;

        private Vector2 _newPosition;

        private float _speed = 2.0f;
        private float _switchTime = 0.5f;
        private float _targetX;
        private float _targetY;
        private float _timerVal;

        private System.Random _randomValue = new System.Random();

        public override void Update()
        {
            _newPosition = new Vector2(_targetX, _targetY).normalized;
            currentGameObject.transform.Translate(_newPosition * Time.deltaTime * _speed);
            SetRandomPositionPanic();
        }

        private void SetRandomPositionPanic()
        {
            _timerVal += Time.deltaTime;

            if (_timerVal > _switchTime)
            {
                _timerVal = 0;
                var randomX = _randomValue.Next(-1, 1);
                var randomY = _randomValue.Next(-1, 1);
                _targetX = randomX - currentGameObject.transform.position.x;
                _targetY = randomY - currentGameObject.transform.position.y;
            }
        }

        public BehaviourPanic(GameObject gameObject)
        {
            currentGameObject = gameObject;
        }
    }
}
