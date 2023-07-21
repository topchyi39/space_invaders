using System.Linq;
using ModestTree;
using UnityEditor;
using UnityEngine;

namespace Attributes.Editor
{
    public class TypeNames
    {
        public string name;
        public string fullName;
    }
    
    [CustomPropertyDrawer(typeof(TypeAttribute))]
    public class TypeDrawer : PropertyDrawer
    {
        
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label(label);
            var typeAttribute = attribute as TypeAttribute;

            var baseType = typeAttribute.BaseType;
            var inheritorsTypes = baseType.Assembly.GetTypes()
                .Where(t => t.BaseType == baseType && t != baseType);
            
            var names = inheritorsTypes.Select(t => t.FullName).ToArray();
            
            if (!string.IsNullOrEmpty(property.stringValue))
            {
                var value = property.stringValue;
                var index = names.IndexOf(value);
                
                if (index >= 0)
                    typeAttribute.index = index;
            }

            typeAttribute.index = EditorGUILayout.Popup(typeAttribute.index, names);
            property.stringValue = names[typeAttribute.index];
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}