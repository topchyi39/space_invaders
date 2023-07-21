using System;
using UnityEngine;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class TypeAttribute : PropertyAttribute
    {
        public Type BaseType;
        public int index;
        
        public TypeAttribute(Type baseType)
        {
            BaseType = baseType;
        }
    }
}