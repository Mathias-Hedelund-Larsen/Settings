using System;
using UnityEngine;

[Serializable]
public struct SimpleResolution : IEquatable<SimpleResolution>
{   
    public float _Width, _Height;

    public bool Equals(SimpleResolution other)
    {
        if(_Width == other._Width && +_Height == other._Height)
        {
            return true;
        }

        return false;
    }

    public static bool operator ==(SimpleResolution res1, SimpleResolution res2)
    {
        return res1.Equals(res2);
    }

    public static bool operator !=(SimpleResolution res1, SimpleResolution res2)
    {
        return !res1.Equals(res2);
    }

    public static explicit operator SimpleResolution(Resolution source)
    {
        var res = new SimpleResolution();
        res._Width = source.width;
        res._Height = source.height;
        return res;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return Equals((SimpleResolution)obj);
    }

    public override int GetHashCode()
    {
        return _Width.GetHashCode() ^ _Height.GetHashCode();
    }
}
