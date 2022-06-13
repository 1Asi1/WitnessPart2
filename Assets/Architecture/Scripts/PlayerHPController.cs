using Assets.Architecture.Scripts.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Architecture.Scripts
{
    public class PlayerHPController : MonoBehaviour
    {
       [SerializeField] private PlayerController _playerScript;
       [SerializeField] private Slider _sliderHP;

        private void LateUpdate()
        {
            _sliderHP.value = _playerScript.GetPlayerHP();
            if (_sliderHP.value == 0)
            {
                _sliderHP.gameObject.SetActive(false);
            }
        }
    }
}
