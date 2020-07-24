using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Features.Input
{
    [Input]
    public class MouseDownComponent
    {
        public Vector2 value;
    }

    [Input]
    public class MouseUpComponent
    {
        public Vector2 value;
    }
}