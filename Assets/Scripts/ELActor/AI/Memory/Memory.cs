using UnityEngine;
public class Memory<T>
{
    private T content;
    private float startTime;
    private float memorySpan;

    public Memory(T content, float memorySpan)
    {
        this.content = content;
        this.memorySpan = memorySpan;
        this.Refresh();
    }

    public void SetMemoryContent(T content)
    {
        this.content = content;
    }

    public T GetMemoryContent()
    {
        return this.content;
    }

    public void Refresh()
    {
        this.startTime = Time.time;
    }

    public bool HasElapsed()
    {
        return Time.time > this.startTime + this.memorySpan;
    }
}
