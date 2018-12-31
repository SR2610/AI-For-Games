using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns collectable objects after a short delay
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    // Prefab to spawn
    public GameObject ObjectPrefabToSpawn;
    public int RespawnDelay = 5;

    // The new GameObject
    private GameObject _newObject;
    private string _objectName;
    private bool _isSpawnScheduled = false;

    // Use this for initialization
    public void Start ()
	{
        SetObjectName();
        SpawnObject();
	}

    /// <summary>
    /// Set the objects name from the prefab name
    /// </summary>
    public void SetObjectName()
    {
        _objectName = ObjectPrefabToSpawn.name;
    }

    // Update is called once per frame
    void Update ()
	{
	    if (_newObject == null && !_isSpawnScheduled)
	    {
	        StartCoroutine(SpawnDelay());
	        _isSpawnScheduled = true;
	    }
	}

    /// <summary>
    /// Delay the spawn for a few seconds
    /// </summary>
    /// <returns></returns>
    protected IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(RespawnDelay);
        SpawnObject();
        _isSpawnScheduled = false;
        yield return null;
    }

    /// <summary>
    /// Perform the actual spawning
    /// </summary>
    protected void SpawnObject()
    {
        _newObject = Instantiate(ObjectPrefabToSpawn, gameObject.transform.position, gameObject.transform.localRotation);
        _newObject.name = _objectName;
    }
}
