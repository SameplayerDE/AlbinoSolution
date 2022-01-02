using System.Collections;
using System.IO;
using AlbinoLibrary;
using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame.Objects;

namespace AlbinoLibraryMonogame.Components
{
    public class QuestLogComponent : AlbinoComponent
    {
        private readonly QuestHolder _questHolder;

        public int Count => _questHolder.Count;
        public bool IsEmpty => _questHolder.Count == 0;
        public Quest this[int index]
        {
            get => _questHolder[index];
            set => _questHolder[index] = value;
        }
        
        public IEnumerator GetEnumerator()
        {
            // ReSharper disable once HeapView.BoxingAllocation
            return _questHolder.GetEnumerator();
        }
        
        public QuestLogComponent()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _questHolder = new QuestHolder();
        }

        public void Add(Quest quest)
        {
            _questHolder.Add(quest);
        }

        public void Remove(Quest quest)
        {
            _questHolder.Remove(quest);
        }

        public override byte[] GetBytes()
        {
            return new byte[] {0x00};
        }

        public override void FromBytes(byte[] datar)
        {
            throw new System.NotImplementedException();
        }
    }
}