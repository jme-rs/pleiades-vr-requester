using System.Collections;
// using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

public class Queue3 : MonoBehaviour
{
    // Queue3<byte[]> Q;
    // private BlockingCollection<byte[]> queue;
    private ConcurrentQueue<byte[]> queue;

    const int MAX_QUEUE_SIZE = 10;
    const int MAX_QUEUE_WAIT = 1000;

    // Start is called before the first frame update
    void Start()
    {
        //������
        // this.queue = new BlockingCollection<byte[]>(MAX_QUEUE_SIZE);
        this.queue = new ConcurrentQueue<byte[]>();
    }

    // Update is called once per frame
    public void Enqueue(byte[] img)
    {
        // return queue.TryAdd(img, MAX_QUEUE_WAIT);
        queue.Enqueue(img);
        Debug.Log("Q3 enqueued");
    }

    public byte[] Dequeue()
    {
        // return queue.TryTake(out byte[] img) ? img : null;
        // Debug.Log("Q3 dequeued");
        return queue.TryDequeue(out byte[] img) ? img : null;
    }

    public bool Check()
    {
        return queue.Count > 0;
    }
}
