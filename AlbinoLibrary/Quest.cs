using System;
using System.Collections.Generic;

namespace AlbinoLibrary
{

    public abstract class QuestTask
    {
        public Quest Parent;
        
        public event EventHandler Completed;

        public float Progress = 0f;
        public string Description = string.Empty;
        
        public void IncreaseProgress(float amount = 1f)
        {
            if (amount <= 0)
            {
                return;
            }
            Progress += amount;
            if (Progress >= 100f)
            {
                OnCompleted(EventArgs.Empty);
            }
        }
        
        protected virtual void OnCompleted(EventArgs e)
        {
            var handler = Completed;
            handler?.Invoke(this, e);
        }

        public override string ToString()
        {
            var text = $"{Progress}";
            return text;
        }
    }
    
    public abstract class CounterTask<T> : QuestTask
    {
        public readonly int Should = 1;
        public int Have = 0;

        protected CounterTask(int should = 1)
        {
            if (should <= 0)
            {
                should = 1;
            }
            Should = should;
        }

        public void Increase(int amount = 1)
        {
            var addProgress = 100f / Should;
            addProgress *= amount;
            
            Have += amount;
            IncreaseProgress(addProgress);
        }
    }
    
    public class CollectTask<T> : CounterTask<T>
    {
        public CollectTask(int should) : base(should)
        {
        }
    }

    public class Quest
    {
        private readonly Queue<QuestTask> _questTasks;
        
        public event EventHandler TaskCompleted;
        public event EventHandler Completed;
        
        public string Title = "Untitled";
        public float Progress = 0f;

        public int TaskCount
        {
            get
            {
                return _questTasks.Count;
            }
        }

        public Quest()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _questTasks = new Queue<QuestTask>();
        }

        protected virtual void OnCompleted(EventArgs e)
        {
            var handler = Completed;
            handler?.Invoke(this, e);
        }
        
        protected virtual void OnTaskCompleted(EventArgs e)
        {
            var handler = TaskCompleted;
            handler?.Invoke(this, e);
        }

        private void IncreaseProgress(object sender, EventArgs eventArgs)
        {
            var addProgress = 100f / _questTasks.Count;
            Progress += addProgress;
            if (Progress >= 100f)
            {
                OnCompleted(EventArgs.Empty);
            }
        }
        
        public void Enqueue(QuestTask questTask)
        {
            questTask.Parent = this;
            // ReSharper disable once HeapView.DelegateAllocation
            questTask.Completed += IncreaseProgress;
            _questTasks.Enqueue(questTask);
        }
        
        public QuestTask Dequeue()
        {
            return _questTasks.Dequeue();
        }
        
        public bool TryDequeue(out QuestTask questTask)
        {
            return _questTasks.TryDequeue(out questTask);
        }
        
        public QuestTask Peek()
        { 
            return _questTasks.Peek();
        }
        
        public bool TryPeek(out QuestTask questTask)
        { 
            return _questTasks.TryPeek(out questTask);
        }

        public override string ToString()
        {
            var text = "{Tasks: " + _questTasks.Count + ", ";

            int i = 0; 
            
            foreach (var task in _questTasks)
            {
                text += i + " : " + task.ToString() + ", ";
                i++;
            }
            return text;
        }
    }
}