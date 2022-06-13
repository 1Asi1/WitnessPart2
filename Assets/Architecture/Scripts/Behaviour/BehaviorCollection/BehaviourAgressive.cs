using UnityEngine;

namespace Assets.Architecture.Scripts.Behaviour.BehaviorCollection
{
    public delegate void AttackHandler(BehaviourAgressive behavior, GameObject source, GameObject target);
    public class BehaviourAgressive : GeneralBehaviour
    {
        public event AttackHandler Attacked; 
        
        private GameObject _targetGameObject;

        private AudioClip _clipShooting;
        private GameObject _laserPrefab;
        private AudioSource _audioSource;
        private Character _character;

        private float _speed = 0.5f * Time.deltaTime;
        private float _minDistance = 2f;
        private float _maxDistance = 7f;
        private float _offsetTime = 1.50f;
        
        private float _laserUpdateTime;
        private bool _laserNotWorking;
        private bool _wasLaserCreated;
        private bool _isLaserCharged = false;

        public BehaviourAgressive(AudioSource audioSource, GameObject laserPrefab, AudioClip clipShooting, Character character)
        {
            _audioSource = audioSource;
            _laserPrefab = laserPrefab;
            _clipShooting = clipShooting;
            _character = character;
        }

        public override void Enter()
        {
            _character.EnemyDetected += OnEnemyDetected;
        }

        public override void Update()
        {
            CreateLaser();
            Pursue();
            LookToPlayer();
        }

        public override void Exit()
        {
            base.Exit();

            _character.EnemyDetected -= OnEnemyDetected;
        }

        private void Pursue()
        {
            if (_targetGameObject != null)
            {
                if (GetDistance() >= _maxDistance)
                {
                    _character.transform.position = Vector2.MoveTowards(
                        _character.transform.position, 
                        _targetGameObject.transform.position,
                        _speed);
                    _character.IsMove();
                }
                else if (GetDistance() <= _minDistance)
                {
                    _character.transform.position = Vector2.MoveTowards(
                        _character.transform.position, 
                        _targetGameObject.transform.position, 
                        _speed * -1);
                    _character.IsMove();
                }
                else
                {
                    if (!_isLaserCharged)
                    {
                        StopShooting();
                        _isLaserCharged = true;
                    }
                }
            }
        }

        private float GetDistance()
        {
            return Vector2.Distance(_character.transform.position, _targetGameObject.transform.position);
        }

        private void LookToPlayer()
        {
            if (_targetGameObject != null)
            {
                Vector2 diraction = _targetGameObject.transform.position - _character.transform.position;
                var targetAngle = Mathf.Atan2(diraction.y, diraction.x) * Mathf.Rad2Deg;
                _character.transform.rotation = Quaternion.Euler(0, 0, targetAngle);
            }
        }

        private void SetEnemy(GameObject playerGameObject)
        {
            _targetGameObject = playerGameObject;
        }

        private void StopShooting()
        {
            _laserNotWorking = true;
        }

        private void Shot()
        {
            _isLaserCharged = false;
            PlayShootingSound();
            
            Attacked?.Invoke(this, _character.transform.gameObject, _targetGameObject);
        }
        
        private void CreateLaser()
        {
            if (_laserNotWorking)
            {
                _laserUpdateTime += Time.deltaTime;

                if (_laserUpdateTime > _audioSource.clip.length - _offsetTime)
                {
                    if (!_wasLaserCreated)
                    {
                        _wasLaserCreated=true;
                        Object.Instantiate(_laserPrefab, _character.transform.position, _character.transform.rotation);
                    }
                }

                if (_laserUpdateTime > _audioSource.clip.length)
                {
                    _laserUpdateTime = 0;
                    _wasLaserCreated = false;
                    _laserNotWorking = false;
                    Shot();
                }
            }
        }
        
        private void PlayShootingSound()
        {
            _audioSource.Stop();
            _audioSource.clip = _clipShooting;
            _audioSource.loop = false;
            _audioSource.Play();
        }
        
        private void OnEnemyDetected(GameObject enemyGameObject)
        {
            SetEnemy(enemyGameObject);
        }
    }
}
