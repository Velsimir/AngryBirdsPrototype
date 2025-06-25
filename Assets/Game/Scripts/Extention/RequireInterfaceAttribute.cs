using UnityEngine;

namespace Game.Scripts.Extention
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