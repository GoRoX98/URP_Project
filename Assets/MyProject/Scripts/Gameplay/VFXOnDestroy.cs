using UnityEngine;

public class VFXOnDestroy : MonoBehaviour
{
    [SerializeField] private GameObject _particlesObject;

    public void SpawnParticles()
    {
        Instantiate(_particlesObject, transform.position, Quaternion.identity);
    }
}
