using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour {

    class PoolObject {
        public Transform transform;
        public bool inUse;
        public PoolObject(Transform t) { transform = t; }
        public void Use() { inUse = true; }
        public void Dispose() { inUse = false; }
    }

    [System.Serializable]
    public struct YSpawnRange {
        public float min;
        public float max;
    }

    public GameObject Prefab;
    public int poolSize;
    public float shiftSpeed;
    public float spawnRate;

    public YSpawnRange spawnRange;
    public Vector3 defaultSpawnPos;
    public Vector3 immediateSpawnPos;
    public bool spawnImmediate;
    public Vector2 targetAspectRatio;

    float spawnTimer;
    float targetAspect;
    PoolObject[] poolObjectsArray;
    GameManager gameManager;

    void Awake() {
        Configure();
    }

    void Start() {
        gameManager = GameManager.Instance;
    }

    void OnEnable() {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed; // Subscribe to OnGameOverConfirmed event
    }

    void OnDisable() {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed; // Unsubscribe to OnGameOverConfirmed event
    }

    //  restart all game objects
    void OnGameOverConfirmed() {
        for (int i = 0; i < poolObjectsArray.Length; i++) {
            poolObjectsArray[i].Dispose();
            poolObjectsArray[i].transform.position = Vector3.one * 1000;
        }
        if (spawnImmediate) SpawnImmediate();
    }

    void Update() {
        if (gameManager.getGameOver()) return;   // return if its game over
        Shift();
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate) {
            Spawn();
            spawnTimer = 0;
        }
    }

    void Configure() {
        targetAspect = targetAspectRatio.x / targetAspectRatio.y;
        poolObjectsArray = new PoolObject[poolSize];
        for (int i = 0; i < poolObjectsArray.Length; i++) {
            GameObject go = Instantiate(Prefab) as GameObject;
            Transform t = go.transform;
            t.SetParent(transform);
            t.position = Vector3.one * 1000;
            poolObjectsArray[i] = new PoolObject(t);
        }
        if (spawnImmediate) SpawnImmediate();
    }

    void Spawn() {
        Transform t = GetPoolObject();
        if (t == null) return;  // if true, pool size is too small
        Vector3 pos = Vector3.zero;
        pos.x = defaultSpawnPos.x;  // pos.x is dependent on target aspect
        pos.y = Random.Range(spawnRange.min, spawnRange.max);   // pos.y is a random value
        t.position = pos;   // set position of our pool object

    }

    void SpawnImmediate() {
        Transform t = GetPoolObject();
        if (t == null) return;  // if true, pool size is too small
        Vector3 pos = Vector3.zero;
        pos.x = immediateSpawnPos.x;
        pos.y = Random.Range(spawnRange.min, spawnRange.max);   // pos.y is a random value
        t.position = pos;   // set position of our pool object
        Spawn();
    }

    void Shift() {
        for (int i = 0; i < poolObjectsArray.Length; i++) {
            //  move poolObject to the left at a constant pace
            poolObjectsArray[i].transform.localPosition += -Vector3.right * shiftSpeed * Time.deltaTime;
            //  Check if its disposed
            CheckDisposedObject(poolObjectsArray[i]);
        }
    }

    //  Check to see if parallax object is off-screen, if it is, set inUse to false using Dispose()
    void CheckDisposedObject(PoolObject poolObject) {
        //  if our poolObject is on the left side of the camera
        if (poolObject.transform.position.x < -defaultSpawnPos.x) {
            //  set as unused
            poolObject.Dispose();
            // set position to someplace off-screen to the right
            poolObject.transform.position = Vector3.one * 1000;
        }
    }

    Transform GetPoolObject() {
        // Get the first available object in poolObjectsArray
        for (int i = 0; i < poolObjectsArray.Length; i++) {
            //  if poolObject at i is not in use...
            if (!poolObjectsArray[i].inUse) {
                // mark it as used
                poolObjectsArray[i].Use();
                return poolObjectsArray[i].transform;
            }
        }
        return null;
    }
}
