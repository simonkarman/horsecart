using System.Linq;
using UnityEngine;

public static class Hierarchy {
    public static T Find<T>() {
        return GameObject.FindGameObjectsWithTag(typeof(T).Name)
            .Select(gameObject => gameObject.GetComponent<T>())
            .Where(t => t != null)
            .FirstOrDefault();
    }
}