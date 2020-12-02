using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private LinkedList<T> _nodes = new LinkedList<T>();
    private readonly Func<T, int> _valueFn;

    public PriorityQueue(Func<T, int> valueFn)
    {
        this._valueFn = valueFn;
    }

    public int Count => _nodes.Count;
    
    public void Enqueue(T data)
    {
        var newNode = new LinkedListNode<T>(data);
        if (_nodes.Count == 0)
        {
            _nodes.AddFirst(newNode);
        }
        else
        {
            var current = _nodes.First;
            while (current != null)
            {
                if (_valueFn(current.Value) < _valueFn(data))
                {
                    _nodes.AddBefore(current, newNode);
                    return;
                }
                current = current.Next;
            }

            _nodes.AddLast(newNode);
        }
    }

    public T Dequeue()
    {
        var best = _nodes.First;
        _nodes.Remove(best);
        return best.Value;
    }
}