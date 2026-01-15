using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string id;
    public string Id => id;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}
