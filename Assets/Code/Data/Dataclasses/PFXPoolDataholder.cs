using UnityEngine;

/// <summary>PoolData for the oneshot particlesystems, acquirable with their respective enum. Should be placed in Resources/PFX
/// </summary>
[CreateAssetMenu(fileName = "NewPFXPoolData", menuName = "ScriptableObjects/PFXData", order = 1)]
public class PFXPoolDataholder : ScriptableObject
{
    [SerializeField]
    private PFXPoolData stdPoolData;

    /// <summary>Gets the Pooldata paired with the input enum. 
    /// </summary>
    public PFXPoolData GetPFXPoolDataByType(PFXType pFXType)
    {
        switch (pFXType)
        {
            case PFXType.StdConsumableExplosion:
                return stdPoolData;
            default:
                throw new System.ArgumentException("No prefab was found for the enum: " + pFXType);
        }
    }

    /// <summary>Contains prefabs and objectpoolsize for the particlesystems
    /// </summary>
    [System.Serializable]
    public class PFXPoolData
    {
        [SerializeField]
        private OneShotPFXPlayer pfxPrefab;
        [SerializeField]
        public int objectPoolSize;

        public int ObjectPoolSize => objectPoolSize;
        public OneShotPFXPlayer PfxPrefab => pfxPrefab;
    }
}
