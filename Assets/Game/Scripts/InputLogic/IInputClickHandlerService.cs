using System;
using UnityEngine;

namespace Game.Scripts.GameLogic.InputLogic
{
    public interface IInputClickHandlerService
    {
        public Vector2 CurrentMousePosition { get; }
        event Action<Vector2> ClickPressed;
        event Action ClickCanceled;
    }
}