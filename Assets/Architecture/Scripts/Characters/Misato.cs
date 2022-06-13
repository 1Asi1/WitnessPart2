using System;
using UnityEngine;
using Assets.Architecture.Scripts.Behaviour;
using Assets.Architecture.Scripts.Behaviour.BehaviorCollection;

namespace Assets.Architecture.Scripts.Characters
{
    public class Misato : Character
    {
        [SerializeField] private Ramiel _ramiel;

        [SerializeField] private Sprite _spriteBehaviourNormal;
        [SerializeField] private Sprite _spriteBehaviourPanic;

        private SpriteRenderer _spriteRenderer;

        protected override void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _spriteBehaviourNormal; 
        }

        protected override void Start()
        {
            base.Start();

            var aggressiveBehavior = _ramiel.GetBehaviour<BehaviourAgressive>();

            aggressiveBehavior.Attacked += OnRamielShot;
        }

        protected override void InltBehaviours()
        {
            _behavioursMap[typeof(BehaviourPanic)] = new BehaviourPanic(gameObject);
            _behavioursMap[typeof(BehaviourIdle)] = new BehaviourIdle();
        }

        private void OnRamielShot(BehaviourAgressive behavior, GameObject source, GameObject target)
        {
            SwitchBehavior<BehaviourPanic>();
            _spriteRenderer.sprite = _spriteBehaviourPanic;
            
            behavior.Attacked -= OnRamielShot;
        }
    }
}
