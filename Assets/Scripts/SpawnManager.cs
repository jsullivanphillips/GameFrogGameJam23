using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitType { PEASANT }
public enum SpawnLocation { TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT}
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Singleton { get; private set; }
    public const int MAXPOOLSIZE = 40;
    [SerializeField] Transform _SpawnTOPLEFT;
    [SerializeField] Transform _SpawnTOPRIGHT;
    [SerializeField] Transform _SpawnBOTTOMLEFT;
    [SerializeField] Transform _SpawnBOTTOMRIGHT;



    [SerializeField] GameObject _PeasantPrefab;
    private int _NumPeasantsToSpawn = 0;
    private int _NumPeasantsSpawned = 0;
    private int _NumAlivePeasants = 0;
    private List<GameObject> _InactivePeasantPool = new List<GameObject>();
    private List<GameObject> _ActivePeasantPool = new List<GameObject>();



    void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Singleton = this;
        }
        if(GameState.Singleton != null)
        {
            _NumPeasantsToSpawn = GameState.Singleton.numPeasantsToSpawn;
            Debug.Log($"_NumPeasantsToSpawn = {_NumPeasantsToSpawn}");
            PopulatePools();
            StartCoroutine(SpawnMobs());
        }
    }

    private void PopulatePools()
    {
        for(int i = 0; i < MAXPOOLSIZE; i++)
        {
            GameObject peasant = Instantiate(_PeasantPrefab, Vector3.zero, Quaternion.identity);
            peasant.SetActive(false);
            _InactivePeasantPool.Add(peasant);
        }
    }

    public IEnumerator SpawnMobs()
    {
        List<GameObject> tempList = new List<GameObject>();
        tempList.AddRange(_InactivePeasantPool);
        for (int i = 0; (i < tempList.Count) && (_NumPeasantsSpawned < _NumPeasantsToSpawn) && (_NumAlivePeasants < MAXPOOLSIZE); i++)
        {
            tempList[i].SetActive(true);
            tempList[i].transform.position = RandomSpawnLocation();
            tempList[i].GetComponent<Peasant>().SetHp(10);
            _ActivePeasantPool.Add(tempList[i]);
            _InactivePeasantPool.Remove(tempList[i]);
            _NumPeasantsSpawned++;
            _NumAlivePeasants++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private Vector3 RandomSpawnLocation()
    {
        int location = Random.Range(0, 4);
        switch(location)
        {
            case 0:
                {
                    return _SpawnTOPLEFT.position;
                }
            case 1:
                {
                    return _SpawnTOPRIGHT.position;
                }
            case 2:
                {
                    return _SpawnBOTTOMLEFT.position;
                }
            case 3:
                {
                    return _SpawnBOTTOMRIGHT.position;
                }
        }
        return _SpawnTOPLEFT.position;
    }

    public void PeasantHasDied(GameObject peasant)
    {
        peasant.SetActive(false);
        _ActivePeasantPool.Remove(peasant);
        _InactivePeasantPool.Add(peasant);
        _NumAlivePeasants--;
        if(_NumPeasantsSpawned < _NumPeasantsToSpawn)
        {
            StartCoroutine(SpawnMobs());
        }
    }

    public void DespawnAllMobs()
    {
        foreach(GameObject peasant in _ActivePeasantPool)
        {
            peasant.SetActive(false);
        }
    }
}
