using System.Collections.Generic;
using System.Threading;

namespace AlbinoLibraryMonogame
{
    public class SceneManager
    {
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        public static SceneManager Instance { get; } = new SceneManager();

        public Mutex ChangeMutex = new Mutex();
        public Scene Current = null;
        public Scene Prev = null;
        public Scene Next = null;

        static SceneManager()
        {
            
        }
        
        private SceneManager()
        {
            
        }

        public void ChangeScene(Scene scene)
        {
            Next = scene;
        }

        public void ExecuteChange()
        {
            lock (ChangeMutex)
            {
                if (Next == null) return;
                Prev = Current;
                Current = Next;
                Next = null;
                Current.Prepare();
            }
        }
    }
}