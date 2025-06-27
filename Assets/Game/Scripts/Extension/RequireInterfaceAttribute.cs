using UnityEngine;

namespace Game.Scripts.Extension
{
    public class RequireInterfaceAttribute : PropertyAttribute
    {
        public System.Type RequiredInterface { get; private set; }

        public RequireInterfaceAttribute(System.Type interfaceType)
        {
            RequiredInterface = interfaceType;
        }
    }
}