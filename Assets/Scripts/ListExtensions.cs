using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class ListExtensions
    {
        public static T RandomElement<T>(this List<T> list)
        {
            var i = UnityEngine.Random.Range(0, list.Count);
            return list[i];
        }
    }
}