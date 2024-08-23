using MML3PneumaticLVS.CustomDataType;
using System.Collections.Concurrent;

namespace MML3PneumaticLVS
{
    
    public class PipeDataCollections
    {
        //public static int PipeCounter = 10;
        public static ConcurrentDictionary<string, Pipe> PipeCollections = new ConcurrentDictionary<string, Pipe>();

        public PipeDataCollections()
        {
            // Initialize any other resources or settings if necessary
        }

        // Method to add a Pipe to the collection
        public bool AddPipe(string key, Pipe pipe)
        {
            return PipeCollections.TryAdd(key, pipe);
        }

        // Method to remove a Pipe from the collection
        public bool RemovePipe(string key)
        {
            return PipeCollections.TryRemove(key, out _);
        }

        // Method to get a Pipe from the collection
        public Pipe GetPipe(string key)
        {
            PipeCollections.TryGetValue(key, out var pipe);
            return pipe;
        }

        // Method to update a Pipe in the collection
        public bool UpdatePipe(string key, Pipe pipe)
        {
            return PipeCollections.TryUpdate(key, pipe, GetPipe(key));
        }

        // Method to clear the collection
        public void ClearPipes()
        {
            PipeCollections.Clear();
        }

        // Method to check if a key exists
        public bool ContainsKey(string key)
        {
            return PipeCollections.ContainsKey(key);
        }

        // Method to get the number of pipes in the collection
        public int CountPipes()
        {
            return PipeCollections.Count;
        }
    }
}
