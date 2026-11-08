using UnityEngine;

namespace Game
{
    public class LookRequetComponent:MonoBehaviour
    {
        public interface IAction
        {
            void Invoke(Vector3 direction);
        }
        
        public interface ICondition
        {
            bool Evaluate();
        }
    }
}