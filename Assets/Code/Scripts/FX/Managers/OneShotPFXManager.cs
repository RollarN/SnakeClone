using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Static class, which can objectpool play a PFX at target location.
/// </summary>
public static class OneShotPFXManager
{
    static Dictionary<PFXType, ObjectPool> PFXPoolDictionary;

    /// <summary>Initializes the PFX object pool based on the PFXPoolData object in Resources/PFX
    /// </summary>
    public static void SetUpObjectPool()
    {
        var PFXPoolData = Resources.Load("PFX/PFXPoolData") as PFXPoolDataholder;
        if (PFXPoolData == null)
            throw new Exception("No PfxPoolData found in Resources/PFX/PFXPoolData");
        PFXPoolDictionary = new Dictionary<PFXType, ObjectPool>();

        //Loops through every Enum value
        foreach (PFXType pType in Enum.GetValues(typeof(PFXType)))
        {
            if (PFXPoolData.GetPFXPoolDataByType(pType) == null)
            {
                Debug.LogWarning("Null PFXPool for enum:  " + pType + ". add a PFXpool to the DataHolder.");
                continue;
            }

            //Gets poolsize and prefab
            var poolSize = PFXPoolData.GetPFXPoolDataByType(pType).objectPoolSize;
            var PFXPrefab = PFXPoolData.GetPFXPoolDataByType(pType).PfxPrefab;

            if (PFXPrefab.GetComponent<ParticleSystem>() == false)
                throw new ArgumentException("No Particle system found for the PFX prefab:" + PFXPrefab.name);

            var pool = new ObjectPool(poolSize, PFXPrefab.gameObject, new GameObject("PFXParent").transform);
            PFXPoolDictionary.Add(pType, pool);
        }
    }

    /// <summary>Plays a PFX based on the Input PFXType at the target location. 
    /// </summary>
    public static void PlayPFX(Vector2 targetPosition, PFXType PfxType)
    {
        if (PFXPoolDictionary[PfxType] == null)
            throw new ArgumentException("No Object pool found for the PFXtype:" + PfxType);

        //Play pfx from first inactive gameobject found
        PFXPoolDictionary[PfxType].Rent().GetComponent<OneShotPFXPlayer>().PlayPFX(targetPosition);
    }
}
public enum PFXType { StdConsumableExplosion }