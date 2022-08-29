using UnityEngine;
using System.Collections.Generic;
public class Identifier
{
    private static List<Identifier> identifiers = new List<Identifier>();
    private int value;

    public static Identifier GetIdentifier()
    {
        Identifier newIdentifier = new Identifier();
        identifiers.Add(newIdentifier);
        return newIdentifier;
    }

    private Identifier()
    {
        this.value = this._GetAvailableIdentifier();
    }

    ~Identifier()
    {
        this.Destroy();
    }

    private int _GetAvailableIdentifier()
    {
        int l_identifier = 0;
        while (identifiers.Find((identifiable) => identifiable.Get() == l_identifier) != null)
        {
            l_identifier++;
        }
        return l_identifier;
    }

    public int Get()
    {
        return this.value;
    }

    public void Destroy()
    {
        identifiers.Remove(this);
    }
}