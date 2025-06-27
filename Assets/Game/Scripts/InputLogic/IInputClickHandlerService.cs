using System;
using UnityEngine;

namespace Game.Scripts.GameLogic.InputLogic
{
    public interface IInputClickHandlerService
    {
        event Action<Vector2> ClickPressed;
        event Action ClickCanceled;
    }
}