using System;
using UnityEngine;
/// <summary> Component that Invokes OnDisabledGameObject(gameObject) when disabled. Used for objectpool
/// </summary>
public class InvokeOnDisable : MonoBehaviour
{
    public Action<GameObject> OnDisabledGameObject;
    private void OnDisable()
    {
        OnDisabledGameObject?.Invoke(gameObject);
    }

}
