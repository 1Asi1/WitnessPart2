using UnityEngine;

namespace Assets.Architecture.Scripts
{
    public class LaserWeapon : MonoBehaviour
    {
        [SerializeField] private float _scaleTime = 0.5f;
        [SerializeField] private float _lifeTime = 1f;

        private float _damage = 25;
        private float _timeInTimeWork;
        private float _timeInLifeTime;
        private float _scaleFactorX;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var component = collision.collider.GetComponent<IDamageble>();

            if (component != null)
            {
                component.TakeDamage(_damage);
            }
        }

        private void Update()
        {
            transform.localScale += new Vector3(_scaleFactorX, 0, 0);
            transform.Translate(_scaleFactorX / 2, 0, 0);

            TimerWork();
            LifeTimeTimer();
        }

        private void TimerWork()
        {
            _timeInTimeWork += Time.deltaTime;

            if (_timeInTimeWork > _scaleTime)
            {
                _timeInTimeWork = 0;
                _scaleFactorX++;
            }
        }

        private void LifeTimeTimer()
        {
            _timeInLifeTime += Time.deltaTime;

            if (_timeInLifeTime > _lifeTime)
            {
                _timeInLifeTime = 0;
                Destroy(gameObject);
            }
        }
    }
}
