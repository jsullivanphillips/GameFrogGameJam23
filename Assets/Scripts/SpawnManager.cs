using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitType { PEASANT }
public enum SpawnLocation { TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT}
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Singleton { get; private set; }
    public const int NUMPEASANTSTOPOOL = 20;
    [SerializeField] Transform _SpawnTOPLEFT;
    [SerializeField] Transform _SpawnTOPRIGHT;
    [SerializeField] Transform _SpawnBOTTOMLEFT;
    [SerializeField] Transform _SpawnBOTTOMRIGHT;
    [SerializeField] GameObject PeasantPrefab;
    private List<GameObject> _PeasantPool = new List<GameObject>();

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
        StartCoroutine(SpawnUnits());
    }

    IEnumerator SpawnUnits()
    {
        for (int i = 0; i < NUMPEASANTSTOPOOL; i++)
        {
            SpawnUnit(UnitType.PEASANT, SpawnLocation.TOPLEFT);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SpawnUnit(UnitType unitType, SpawnLocation spawnLocation)
    {
        GameObject unitToSpawn = PeasantPrefab;
        Vector3 spawnVector = _SpawnTOPLEFT.position;
        switch(unitType)
        {
            case (UnitType.PEASANT):
                {
                    unitToSpawn = PeasantPrefab;
                    break;
                }
        }

        switch(spawnLocation)
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

        Instantiate(unitToSpawn, spawnVector, Quaternion.identity);

    }
}
