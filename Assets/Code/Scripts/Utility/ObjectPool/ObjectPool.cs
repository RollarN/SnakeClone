using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectPool containing gameobjects.
/// </summary>
public class ObjectPool
{
    private readonly int expandBy;
    private readonly GameObject prefab;
    private Transform parent;
    private readonly Stack<GameObject> objects = new Stack<GameObject>();

    /// <summary>
    /// Creates a new ObjectPool
    /// </summary>
    /// <param name="initSize">Initial size of pool.</param>
    /// <param name="prefab">Object to pool.</param>
    /// <param name="expandBy">Amount to expand pool by when its empty.</param>
    /// <param name="parent">Pooled objects parent transform.</param>
    public ObjectPool(int initSize, GameObject prefab, Transform parent = null, int expandBy = 1)
    {
        this.expandBy = expandBy < 1 ? 1 : expandBy;
        this.parent = parent;
        this.prefab = prefab;
        Expand(initSize < 1 ? 1 : initSize);
    }
    /// <summary>
    /// Expands the object pool by the set amount
    /// </summary>
    private void Expand(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject instance = Object.Instantiate(prefab, parent);
            instance.transform.parent = parent;
            instance.SetActive(false);
            InvokeOnDisable invokeOnDisable = instance.AddComponent<InvokeOnDisable>();
            invokeOnDisable.OnDisabledGameObject += (GameObject go) => objects.Push(go);
            objects.Push(instance);
        }
    }
    /// <summary>
    /// Gets an object from the Objectpool stack.
    /// </summary>
    /// <param name="activate"> State of the GameObject when returned.</param>
    public GameObject Rent(bool activate = false)
    {
        if (objects.Count == 0)
        {
            Expand(expandBy);
        }
        var objInstance = objects.Pop();
        objInstance = objInstance != null ? objInstance : Rent(activate);
        objInstance.SetActive(activate);
        return objInstance;
    }
}