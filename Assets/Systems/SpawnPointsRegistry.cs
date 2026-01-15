using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsRegistry : MonoBehaviour
{
    private Dictionary<string, Transform> _spawns;

    private void Awake()
    {
        _spawns = new Dictionary<string, Transform>();
        foreach (var sp in GetComponentsInChildren<SpawnPoint>(true))
            _spawns[sp.Id] = sp.transform;
    }

    public bool TryGet(string id, out Transform t) => _spawns.TryGetValue(id, out t);
}
