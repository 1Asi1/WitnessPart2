using UnityEngine;

namespace Assets.Architecture.Scripts
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private GameObject _followTarget;

        private Vector2 _offset=new Vector2(0.1f, 1);

        private void LateUpdate()
        {
            if(_followTarget != null)
            {
                transform.position = new Vector2(_followTarget.transform.position.x, _followTarget.transform.position.y)+ _offset;
            }
        }
    }  
}
