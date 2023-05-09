using Features.Lootboxes.Data;
using Features.Lootboxes.Views;
using UnityEditor;

namespace Features.Lootboxes
{
    [CustomEditor(typeof(BaseLootboxView))]
    public class TextLootboxCustomDrawer : Editor
    {
        private SerializedObject _serializedObject;
        private SerializedProperty _currentProperty;

        private void OnEnable()
        {
            _serializedObject = new SerializedObject(target);
            _currentProperty = _serializedObject.GetIterator();
            _currentProperty.Next(true);
        }

        public override void OnInspectorGUI()
        {
            _serializedObject.Update();

            var lootTypeProp = _serializedObject.FindProperty("_lootType");
            var timeToTriggerProp = _serializedObject.FindProperty("_timeToTrigger");

            EditorGUILayout.PropertyField(lootTypeProp);

            if ((LootType)lootTypeProp.enumValueIndex == LootType.HoldF)
            {
                EditorGUILayout.PropertyField(timeToTriggerProp);
            }

            var property = _serializedObject.GetIterator();
            var next = property.NextVisible(true);
            
            while (next)
            {
                if (property.name != "_lootType" && property.name != "_timeToTrigger")
                {
                    EditorGUILayout.PropertyField(property, true);
                }
                next = property.NextVisible(false);
            }

            _serializedObject.ApplyModifiedProperties();
        }
    }
}