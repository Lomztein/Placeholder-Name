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
        Instantiate (spawnPrefab, transform.position + GetSpawnPos (), Quaternion.identity);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere (transform.position, range);
    }

    private Vector3 GetSpawnPos () {
        Vector3 center = Vector3.zero;
        float angle = Random.Range (0f, 360f);

        float cos = Mathf.Cos (angle * Mathf.Rad2Deg);
        float sin = Mathf.Sin (angle * Mathf.Rad2Deg);

        center += new Vector3 (cos * range, 0f, sin * range);
        return center;
    }
}
