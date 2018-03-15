using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclularSpawner : MonoBehaviour {

    public GameObject spawnPrefab;
    public float spawnRate;
    public float range;

	// Use this for initialization
	void Start () {
        InvokeRepeating ("Spawn", spawnRate, spawnRate);
	}
	
	void Spawn () {
        Vector3 position = Random.onUnitSphere * range;
        position.y = 2f;

        Instantiate (spawnPrefab, transform.position + position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere (transform.position, range);
    }
}
