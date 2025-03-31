using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPointData", menuName = "TType/SpawnPointData", order = 1)]
public class SpawnPointData : ScriptableObject
{
    [SerializeField] private EntityData _entityData;
    [SerializeField] private int _amount;
    [SerializeField] private Vector3 _spacing;
    [SerializeField] private float _rate;

    public EntityData EntityData
    {
        get { return _entityData; }
    }

    public int Amount
    {
        get { return _amount; }
    }

    public float Rate
    {
        get { return _rate; }
    }

    public Vector3 Spacing
    {
        get { return _spacing; }
    }
}