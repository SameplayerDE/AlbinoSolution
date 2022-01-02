using System.Collections;
using System.Collections.Generic;
using AlbinoLibrary;

namespace AlbinoLibraryMonogame.Objects
{
    public class QuestHolder : IEnumerable
    {

        private readonly List<Quest> _quests;
        
        public int Count => _quests.Count;
        public bool IsEmpty => _quests.Count == 0;
        public Quest this[int index]
        {
            get => _quests[index];
            set => _quests[index] = value;
        }
        
        public QuestHolder()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _quests = new List<Quest>();
        }
        
        public void Add(Quest quest)
        {
            _quests.Add(quest);
        }

        public void Remove(Quest quest)
        {
            _quests.Remove(quest);
        }
        
        public IEnumerator GetEnumerator()
        {
            // ReSharper disable once HeapView.BoxingAllocation
            return _quests.GetEnumerator();
        }
    }
}