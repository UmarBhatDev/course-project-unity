using System;
using FSM.Data;

namespace Features.SceneTransitions.Data
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeCurtainType : Attribute
    {
        public CurtainType CurtainType { get; }

        public AttributeCurtainType(CurtainType curtainType)
        {
            CurtainType = curtainType;
        }
    }
}