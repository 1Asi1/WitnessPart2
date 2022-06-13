using Assets.Architecture.Scripts.Behaviour;
using Assets.Architecture.Scripts.Behaviour.BehaviorCollection;
using UnityEngine;

namespace Assets.Architecture.Scripts.Characters
{
    public class Ramiel : Character
    {
        [SerializeField] private AudioClip _clipIdle;
        [SerializeField] private AudioClip _clipShooting;

        [SerializeField] private GameObject _laserPrefab;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private BehaviorAggressiveConfig _config;

        protected override void InltBehaviours()
        {
            _behavioursMap[typeof(BehaviourAgressive)] = new BehaviourAgressive(_audioSource, _laserPrefab, _clipShooting, this);
            _behavioursMap[typeof(BehaviourIdle)] = new BehaviourIdle();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                SwitchBehavior<BehaviourAgressive>();
                TakeEnemy(collision.gameObject);
            }
        }
    }
}
