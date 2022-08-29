using System;
using System.Collections.Generic;

public class Observable<T>
{
    private T value;
    private Dictionary<Identifier, Action<T>> callbacks = new();

    public Observable(T value)
    {
        this.value = value;
    }

    public T Get()
    {
        return this.value;
    }

    public void Set(T value)
    {
        // Only notify if values are different
        if (EqualityComparer<T>.Default.Equals(this.value, value)) return;
        this.value = value;
        this.Notify();
    }

    public void Notify()
    {
        foreach (Action<T> callback in new List<Action<T>>(this.callbacks.Values))
        {
            callback(this.value);
        }
    }

    public Identifier Subscribe(Action<T> callback)
    {
        Identifier identifier = Identifier.GetIdentifier();
        this.callbacks.Add(identifier, callback);

        // Initial callback
        callback(this.value);

        return identifier;
    }

    public void Unsubscribe(Identifier identifier)
    {
        this.callbacks.Remove(identifier);
        identifier.Destroy();
    }
}