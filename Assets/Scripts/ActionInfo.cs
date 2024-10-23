using UnityEngine;

namespace DefaultNamespace
{
    public class ActionInfo
    {
        public ActionKind Kind;
        public float? Axis;  
        public float Time;

        public ActionInfo(ActionKind kind, float time, float? axis = null)
        {
            Kind = kind;
            Time = time;
            Axis = axis;
        }
    }
}