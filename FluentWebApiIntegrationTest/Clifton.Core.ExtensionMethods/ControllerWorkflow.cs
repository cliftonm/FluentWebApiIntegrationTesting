using System;
using System.Collections.Generic;
using System.Text;

namespace Clifton.Core.ExtensionMethods
{
    public class WorkflowCondition
    {
        public Func<bool> IfTrue { get; set; }
        public Func<object> Then { get; set; }
    }

    public class ControllerWorkflow
    {
        public List<WorkflowCondition> WorkflowItems { get; set; } = new List<WorkflowCondition>();

        public ControllerWorkflow Add(WorkflowCondition item)
        {
            WorkflowItems.Add(item);

            return this;
        }

        public ControllerWorkflow Add(Func<bool> ifTrue, Func<object> then)
        {
            WorkflowItems.Add(new WorkflowCondition() { IfTrue = ifTrue, Then = then });

            return this;
        }

        public object Execute()
        {
            object ret = null;

            foreach (var item in WorkflowItems)
            {
                if (item.IfTrue())
                {
                    ret = item.Then();
                    break;
                }
            }

            return ret;
        }
    }
}
