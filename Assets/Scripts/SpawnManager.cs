using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitType { PEASANT }
public enum SpawnLocation { TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT}
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Singleton { get; private set; }
    public const int MAXPOOLSIZE = 20;
    [SerializeField] Transform _SpawnTOPLEFT;
    [SerializeField] Transform _SpawnTOPRIGHT;
    [SerializeField] Transform _SpawnBOTTOMLEFT;
    [SerializeField] Transform _SpawnBOTTOMRIGHT;



    [SerializeField] GameObject _PeasantPrefab;
    private int _NumPeasantsToSpawn = 0;
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
        _NumPeasantsToSpawn = 15;
        PopulatePools();
        SpawnMobs();
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

    public void SpawnMobs()
    {
        List<GameObject> tempList = new List<GameObject>();
        tempList.AddRange(_InactivePeasantPool);
        for (int i = 0; i < tempList.Count && i < _NumPeasantsToSpawn; i++)
        {
            tempList[i].SetActive(true);
            tempList[i].transform.position = RandomSpawnLocation();
            _ActivePeasantPool.Add(tempList[i]);
            _InactivePeasantPool.Remove(tempList[i]);
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

    public void SpawnUnit(UnitType unitType, SpawnLocation spawnLocation)
    {

        /*
        switch (spawnLocation)
        {
            case (SpawnLocation.TOPLEFT):
                {
                    spawnVector = _SpawnTOPLEFT.position;
                    break;
                }
            case (SpawnLocation.TOPRIGHT):
                {
                    spawnVector = _SpawnTOPRIGHT.position;
                    break;
                }
            case (SpawnLocation.BOTTOMLEFT):
                {
                    spawnVector = _SpawnBOTTOMLEFT.position;
                    break;
                }
            case (SpawnLocation.BOTTOMRIGHT):
                {
                    spawnVector = _SpawnBOTTOMRIGHT.position;
                    break;
                }
        }
        */
    }
}
