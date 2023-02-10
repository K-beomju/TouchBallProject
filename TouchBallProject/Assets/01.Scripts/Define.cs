using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public static List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = UnityEngine.Random.Range(0, i);

            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }

        return list;
    }
}
