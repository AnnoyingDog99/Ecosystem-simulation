using System;
using System.Collections.Generic;


using ObservableIdentifier = System.UInt32;
public class Observable<T>
{
    private T value;
    private Dictionary<ObservableIdentifier, Action<T>> callbacks = new();

    public Observable(T value)
    {
        this.value = value;
    }

    private ObservableIdentifier _GetAvailableIdentifier()
    {
        ObservableIdentifier identifier = 0;
        while (this.callbacks.ContainsKey(identifier))
        {
            identifier++;
        }
        return identifier;
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
        foreach (Action<T> callback in this.callbacks.Values)
        {
            callback(this.value);
        }
    }

    public ObservableIdentifier Subscribe(Action<T> callback)
    {
        ObservableIdentifier identifier = this._GetAvailableIdentifier();
        this.callbacks.Add(identifier, callback);

        // Initial callback
        callback(this.value);

        return identifier;
    }

    public void Unsubscribe(ObservableIdentifier identifier)
    {
        this.callbacks.Remove(identifier);
    }
}