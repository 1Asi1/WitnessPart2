using UnityEngine;
using Assets.Architecture.Scripts.Behaviour;
using Assets.Architecture.Scripts.Behaviour.BehaviorCollection;

namespace Assets.Architecture.Scripts.Characters
{
    public class Misato : BehaviourCharacter
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
            _ramiel.ShotEvent += OnRamielShot;
        }

        private void OnRamielShot()
        {
            SwitchBehavior<BehaviourPanic>();
            _spriteRenderer.sprite = _spriteBehaviourPanic;
            _ramiel.ShotEvent -= OnRamielShot;
        }
    }
}
