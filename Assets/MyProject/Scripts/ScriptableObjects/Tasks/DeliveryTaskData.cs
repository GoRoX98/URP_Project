using UnityEngine;

[CreateAssetMenu(fileName = "New Delivery Task", menuName = "ScriptableObjects/Tasks/Create Delivery Task")]
public class DeliveryTaskData : TaskData
{
    [SerializeField] private GameObject _deliveryPrefab;
    [SerializeField] private Vector3 _spawnPos;
    [SerializeField] private Vector3 _endPos;

    public GameObject GameObject => _deliveryPrefab;
    public Vector3 SpawnPos => _spawnPos;
    public Vector3 EndPos => _endPos;
}
