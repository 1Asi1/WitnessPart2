using UnityEngine;

namespace Assets.Architecture.Scripts.Characters
{
    public class PlayerController : MonoBehaviour, IDamageble
    {
        private float _horisontal;
        private float _vertical;
        private float _playerSpeed = 3f;
        private float _playerHP = 100;

        private void FixedUpdate()
        {
            transform.Translate(MovePlayer() * Time.fixedDeltaTime * _playerSpeed);
        }

        private void Update()
        {
            if (_playerHP == 0)
            {
                Destroy(gameObject);
            }
        }

        private Vector2 MovePlayer()
        {
            _horisontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
            return new Vector2(_horisontal, _vertical);
        }

        public void TakeDamage(float dmgValue)
        {
            _playerHP -= dmgValue;
        }

        public float GetPlayerHP()
        {
            return _playerHP;
        }
    }
}
