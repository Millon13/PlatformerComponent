using UnityEngine;

namespace Game.Scripts.GameObjects.Components.Look
{
    public sealed class LookComponent : MonoBehaviour
    {
        [SerializeField] private Transform _charectorTransform;


        public void Update()
        {
            if (_charectorTransform != null)
                LookAt(_charectorTransform);
        }

        public void LookAt(Transform target)
        {
            Vector2 direction = target.position - this.transform.position;
            this.LookDir(direction.x);
        }

        public void LookDir(float direction)
        {
            float angle = direction > 0 ? 0 : 180;
            this.transform.eulerAngles = new Vector3(0, angle, 0);
        }

        public void Look(Vector3 direction)
        {
            this.LookDir(direction.x);
        }
    }
}