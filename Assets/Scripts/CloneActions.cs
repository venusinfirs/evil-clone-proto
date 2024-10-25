using System.Collections.Generic;

namespace DefaultNamespace
{
    public class CloneActions
    {
        public Queue<ActionInfo> Actions { get; private set; }
        public EvilClone CloneInstance { get; private set; }

        public void SetAction(ActionInfo actionInfo)
        {
            if (Actions == null)
            {
                Actions = new Queue<ActionInfo>();
            }
            
            Actions.Enqueue(actionInfo);
        }

        public void SetCloneInstance(EvilClone clone)
        {
            CloneInstance = clone;
        }
    }
}