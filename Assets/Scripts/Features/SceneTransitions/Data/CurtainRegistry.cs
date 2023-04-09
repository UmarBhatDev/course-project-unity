using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Features.SceneTransitions.Views;
using FSM.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Features.SceneTransitions.Data
{
    [CreateAssetMenu(fileName = "CurtainRegistry", menuName = "Registries/CurtainRegistry")]
    public class CurtainRegistry : ScriptableObject
    {
        [SerializeField] private List<CurtainViewBase> _curtainViews;

        public List<CurtainViewBase> CurtainViews => _curtainViews;
        
        [CanBeNull]
        public GameObject GetCurtainByType(CurtainType type)
        {
            return (from curtainView in _curtainViews
                let attribute = (AttributeCurtainType)curtainView.GetType().GetCustomAttribute(typeof(AttributeCurtainType))
                where attribute.CurtainType == type 
                select curtainView.gameObject).FirstOrDefault();
        }
    }
}