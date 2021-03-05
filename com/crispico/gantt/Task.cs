using System;
using System.Collections.Generic;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.gantt
{
    /**
     * A task can have a list of subtasks
     * and must have at least 1 segment
     */
    public class Task
    {        
        public String name {get; set;}
        public String comment {get; set;}
        //public Task parentTask {get; set;}
        public List<Task> subTasks {get; set;}
        public List<Segment> segments {get; set;}
        public int taskNumber {get; set;}
        public int taskTerminal {get; set;}
        public String arrivalDeparture { get; set; }
        public String type { get; set; }
        // global resource type: ranged (can occupy more that 1 desk)
        // or nonRanged(occupies 1 desk)
        public String resourceType { get; set; }
        // for alphaNumerical mode
        public String alphaNumericalName { get; set; }
       
        public Task()
        {
            name = "";
            comment = "";
            //parentTask = null;
            subTasks = new List<Task>();
            segments = new List<Segment>();
            taskNumber = 0;
            taskTerminal = 0;
            arrivalDeparture = "";
            type = ""; 
            alphaNumericalName = "";
        }
        
        public Task (String pName, String pComment, Task pParentTask,
                          List<Task> pSubTasks, List<Segment> pSegments)
        {
            name = pName;
            comment = pComment;
            //parentTask = pParentTask;
            subTasks = pSubTasks;
            segments = pSegments;
        }

        public Task clone()
        {
            Task taskClone = new Task();

            taskClone.name = this.name;
            taskClone.taskNumber = this.taskNumber;
            taskClone.taskTerminal = this.taskTerminal;
            taskClone.type = this.type;
            taskClone.resourceType = this.resourceType;
            taskClone.arrivalDeparture = this.arrivalDeparture;
            taskClone.comment = this.comment;
            taskClone.alphaNumericalName = this.alphaNumericalName;

            if (this.subTasks != null && this.subTasks.Count > 0)
                foreach (Task subTaskClone in this.subTasks)
                    taskClone.subTasks.Add(subTaskClone.clone());
            if (this.segments != null && this.segments.Count > 0)
                foreach (Segment segmentClone in this.segments)
                    taskClone.segments.Add(segmentClone.clone());

            return taskClone;
        }
    }
}
