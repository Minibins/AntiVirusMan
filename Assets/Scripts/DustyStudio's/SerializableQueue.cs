using System;
using System.Collections.Generic;
using UnityEngine;

namespace DustyStudios
{
    [Serializable]
    public class SerializableQueue<Type>
    {
        [SerializeField] private List<Type> queue;
        private Queue<Type> actualQueue;
        private InvalidOperationException emptyException = new InvalidOperationException("The queue is empty.");

        public int Count
        {
            get { return actualQueue.Count; }
        }

        private void serialize()
        {
            if (actualQueue == null)
            {
                actualQueue = new Queue<Type>(queue);
            }
        }

        public static explicit operator Queue<Type>(SerializableQueue<Type> me)
        {
            if (me.actualQueue == null) me.actualQueue = new Queue<Type>(me.queue);
            return me.actualQueue;
        }

        public void Enqueue(Type item)
        {
            serialize();
            actualQueue.Enqueue(item);
        }

        public Type Dequeue()
        {
            serialize();
            if (actualQueue.Count > 0)
            {
                Type item = actualQueue.Dequeue();
                return item;
            }
            else
            {
                throw emptyException;
            }
        }

        public Type Peek()
        {
            serialize();
            if (actualQueue.Count > 0)
            {
                return actualQueue.Peek();
            }
            else
            {
                throw emptyException;
            }
        }
    }
}