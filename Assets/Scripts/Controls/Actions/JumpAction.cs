using System;
using Systemics.Variables;
using UnityEngine;

namespace Controls.Actions
{
    [CreateAssetMenu(fileName = "New Jump Action", menuName = "Actions/Jump")]
    public class JumpAction : Action, 
        ISerializationCallbackReceiver
    {
        [SerializeField] private FloatVariable groundCheckDistance;
        
        public override bool Evaluate()
        {
            
            
            return false;
        }

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            // Clean it up
        }
    }
}