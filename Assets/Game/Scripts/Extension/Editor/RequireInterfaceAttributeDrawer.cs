using System;
using System.Linq;
using Game.Scripts.Extension;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts
{
    [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public class RequireInterfaceAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RequireInterfaceAttribute requireInterface = (RequireInterfaceAttribute)attribute;
            Type requiredType = requireInterface.RequiredInterface;
            Object previousValid = GetValidComponent(property.objectReferenceValue, requiredType);

            Object picked = EditorGUI.ObjectField(position, label, previousValid, typeof(Object), true);
            Object result = GetValidComponent(picked, requiredType);

            property.objectReferenceValue = result != null ? result : previousValid;
        }

        private static Object GetValidComponent(Object obj, Type requiredType)
        {
            if (obj is GameObject go)
            {
                return go.GetComponents<MonoBehaviour>()
                    .FirstOrDefault(c => requiredType.IsAssignableFrom(c.GetType()));
            }

            if (obj is MonoBehaviour mb)
            {
                if (requiredType.IsAssignableFrom(mb.GetType()))
                {
                    return mb;
                }

                return mb.gameObject.GetComponents<MonoBehaviour>()
                    .FirstOrDefault(c => requiredType.IsAssignableFrom(c.GetType()));
            }

            return null;
        }
    }
}