
using System.Collections;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] fruits;
    [SerializeField] private GameObject bomb;

    [SerializeField] private float minSpawnDelay = 0.25f;
    [SerializeField] private float maxSpawnDelay = 1f;
    [SerializeField] private float minForce = 18f;
    [SerializeField] private float maxForce = 22f;
    [SerializeField] private float minAngle = -15f;
    [SerializeField] private float maxAngle = 15f;
    [SerializeField] private float lifetime = 5f;
    [Range(0f,1f)]
    [SerializeField] private float bombChance = 0.5f;

    private Collider spawnArea;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
        GameManager.Instance.spawner = this;
    }
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            GameObject prefab = fruits[Random.Range(0, fruits.Length)];

            // spawn bomb insted of fruit in random 
            if(Random.value < bombChance) prefab = bomb;

            Vector3 position = new Vector3();

            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f,0f,Random.Range(minAngle,maxAngle));

            GameObject fruit =  Instantiate(prefab, position, rotation);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * Random.Range(minForce, maxForce), ForceMode.Impulse);
            Destroy(fruit , lifetime);



            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
