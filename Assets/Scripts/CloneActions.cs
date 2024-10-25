using System.Collections.Generic;

namespace DefaultNamespace
{
    public class CloneActions
    {
        public Queue<ActionInfo> ActionCycles { get; private set; }
        public EvilClone CloneInstance { get; private set; }

        public void SetAction(ActionInfo actionInfo)
        {
            if (ActionCycles == null)
            {
                ActionCycles = new Queue<ActionInfo>();
            }
            
            ActionCycles.Enqueue(actionInfo);
        }

        public void SetCloneInstance(EvilClone clone)
        {
            CloneInstance = clone;
        }
    }
}