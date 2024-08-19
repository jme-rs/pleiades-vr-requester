using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    Queue<byte[]> Q;

    // Start is called before the first frame update
    void Start()
    {
        //‰Šú‰»
        Q = new Queue<byte[]>();
    }

    // Update is called once per frame
    public void enqueue(byte[] img)
    {
        Q.Enqueue(img);
    }

    public byte[] dequeue()
    {
        return Q.Dequeue();
    }

    public bool check()
    {
        return Q.Count > 0;
    }
}
