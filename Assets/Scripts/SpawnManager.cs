using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitType { PEASANT }
public enum SpawnLocation { TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT}
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Singleton { get; private set; }
    public const int NUMPEASANTSTOPOOL = 20;
    [SerializeField] Vector3 _SpawnTOPLEFT;
    [SerializeField] Vector3 _SpawnTOPRIGHT;
    [SerializeField] Vector3 _SpawnBOTTOMLEFT;
    [SerializeField] Vector3 _SpawnBOTTOMRIGHT;
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
    }

    public void SpawnUnit(UnitType unitType, SpawnLocation spawnLocation)
    {
        GameObject unitToSpawn;
        Vector3 spawnLocation;
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
                    spawnLocation = _SpawnTOPLEFT;
                    break;
                }
            case (SpawnLocation.TOPRIGHT):
                {
                    spawnLocation = _SpawnTOPRIGHT;
                    break;
                }
            case (SpawnLocation.BOTTOMLEFT):
                {
                    spawnLocation = _SpawnBOTTOMLEFT;
                    break;
                }
            case (SpawnLocation.BOTTOMRIGHT):
                {
                    spawnLocation = _SpawnBOTTOMRIGHT;
                    break;
                }
        }

    }
}
