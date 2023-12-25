using System;
using System.Collections.Generic;

public class PriorityQueue<T> where T : IComparable<T>
{
    public List<T> heap = new List<T>();

    public int Count => heap.Count;

    public T Contains(T t)
    {
        int idx = heap.IndexOf(t);
        if (idx < 0) return default(T);
        return heap[idx];
    }

    public void Push(T data)
    {
        heap.Add(data);
        int now = heap.Count - 1;

        while (now > 0)
        {
            int next = (now - 1) / 2;
            if (heap[now].CompareTo(heap[next]) < 0)
            {
                break;
            }

            T temp = heap[now];
            heap[now] = heap[next];
            heap[next] = temp;

            now = next;
        }
    }

    public T Pop()
    {
        T ret = heap[0];

        int lastIndex = heap.Count - 1;
        heap[0] = heap[lastIndex];
        heap.RemoveAt(lastIndex);
        lastIndex--;

        int now = 0;

        while (true)
        {
            int left = 2 * now + 1;
            int right = 2 * now + 1;

            int next = now;
            if (left <= lastIndex && heap[next].CompareTo(heap[left]) < 0)
            {
                next = left;
            }
            if (right <= lastIndex && heap[next].CompareTo(heap[right]) < 0)
            {
                next = right;
            }

            if (next == now)
            {
                break;
            }

            T temp = heap[now];
            heap[now] = heap[next];
            heap[next] = temp;

            now = next;
        }

        return ret;
    }

    public void Recalculation(T node)
    {
        int now = heap.IndexOf(node);

        while (now > 0)
        {
            int next = (now - 1) / 2;
            if (heap[now].CompareTo(heap[next]) < 0)
            {
                break;
            }

            T temp = heap[now];
            heap[now] = heap[next];
            heap[next] = temp;

            now = next;
        }
    }

    public T Peak()
    {
        return heap.Count == 0 ? default(T) : heap[0];
    }

    public void Clear()
    {
        heap.Clear();
    }
}
