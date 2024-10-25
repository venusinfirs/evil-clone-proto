namespace DefaultNamespace
{
    public class ActionInfo
    {
        public ActionKind Kind;
        public float StartTime;
        public float EndTime;
        public float? Axis;
        public ActionInfo(ActionKind kind, float startTime, float endTime, float? axis = null)
        {
            Kind = kind;
            StartTime = startTime;
            EndTime = endTime;
            Axis = axis;
        }
    }
}