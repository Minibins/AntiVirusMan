using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SpawnTrain : MonoBehaviour
{
    [SerializeField] private GameObject _trainPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private GameObject[] _spawnedObjects;
    [SerializeField] private Sprite[] _interior;
    [SerializeField] private Sprite[] _wheels;
    [SerializeField] private Tilemap[] _tileMaps;
    
    private void Update()
    {
        for (int i = 0; i < _spawnedObjects.Length; i++)
        {
            if (_spawnedObjects[i].transform.position.x <= _endPoint.position.x)
            {
                Destroy(_spawnedObjects[i]);

                _spawnedObjects[i] = Instantiate(_trainPrefab, _spawnPoint.position, Quaternion.identity);
                
                NewOption(i);
            }
        }
    }

    private void NewOption(int _index)
    {
        _spawnedObjects[_index].GetComponent<TrainOption>().ChangeTrain(_tileMaps[Random.Range(0, _tileMaps.Length)],_interior[Random.Range(0, _interior.Length)],
            _wheels[Random.Range(0, _wheels.Length)], _wheels[Random.Range(0, _wheels.Length)],
            _wheels[Random.Range(0, _wheels.Length)], _wheels[Random.Range(0, _wheels.Length)]);
    }
}