using Assets.Architecture.Scripts.Behaviour;
using Assets.Architecture.Scripts.Behaviour.BehaviorCollection;
using UnityEngine;

namespace Assets.Architecture.Scripts.Characters
{
    public class Ramiel : BehaviourCharacter
    {
        [SerializeField] private AudioClip _clipIdle;
        [SerializeField] private AudioClip _clipShooting;

        [SerializeField] private GameObject _laserPrefab;

        private AudioSource _audioSrc;

        private float _laserUpdateTime;
        private float _offsetTime = 1.50f;

        private bool _laserIsDone;
        private bool _laserIsCreate;
        private bool _isCharges;


        protected override void Awake()
        {
            _audioSrc = GetComponent<AudioSource>();
            IsMoveEvent += IdleSoudPlay;
            IsStopEvent += ShootingSoudPlay;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                SwitchBehavior<BehaviourAgressive>();
                TakeEnemy(collision.gameObject);
            }
        }

        protected override void Update()
        {
            base.Update();
            CreateLaser();
        }

        public void IdleSoudPlay()
        {
            if (_isCharges)
            {
                _isCharges = false;
                _audioSrc.Stop();
                _audioSrc.clip = _clipIdle;
                _audioSrc.loop = true;
                _audioSrc.Play();
            }
        }

        public void ShootingSoudPlay()
        {
            _laserIsDone = true;
            _audioSrc.Stop();
            _audioSrc.clip = _clipShooting;
            _audioSrc.loop = false;
            _audioSrc.Play();
        }

        private void CreateLaser()
        {
            if (_laserIsDone)
            {
                _laserUpdateTime += Time.deltaTime;

                if (_laserUpdateTime > _audioSrc.clip.length - _offsetTime)
                {
                    if (!_laserIsCreate)
                    {
                        _laserIsCreate=true;
                        Instantiate(_laserPrefab, transform.position, transform.rotation);
                    }
                }

                if (_laserUpdateTime > _audioSrc.clip.length)
                {
                    _laserUpdateTime = 0;
                    _laserIsCreate = false;
                    _laserIsDone = false;
                    _isCharges = true;
                    Shoot();
                }
            }
        }
    }
}
