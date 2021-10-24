using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static List<T> RemoveBack<T>(this List<T> list)
    {
        if (list.Count > 0)
        {
            list.RemoveAt(list.Count - 1);
            return list;
        }
        return default;
    }

    public static List<T> RemoveFront<T>(this List<T> list)
    {
        if (list.Count > 0)
        {
            list.RemoveAt(0);
            return list;
        }
        return default;
    }

    public static T PopBack<T>(this List<T> list)
    {
        if (list.Count > 0)
        {
            T ret = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return ret;
        }
        return default;
    }

    public static T PopFront<T>(this List<T> list)
    {
        T ret = list[0];
        list.RemoveAt(0);
        return ret;
    }
}
