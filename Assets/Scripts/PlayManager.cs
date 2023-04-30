using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Duck duck;
    [SerializeField] List<Terrain> terrainList;
    [SerializeField] List<Coin> coinList;
    [SerializeField] int initialGrassCount = 5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -4;
    [SerializeField] int forwardViewDistance = 15;
    [SerializeField, Range(0,1)] float treeProbability;
    [SerializeField] private int travelDistance;
    [SerializeField] private int coin;
    [SerializeField] float initialTimer = 10;
    Dictionary<int, Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);
    public UnityEvent<int, int> onUpdateTerrainLimit;
    public UnityEvent<int> onScoreUpdate;
    
    
    private void Start() {
        for (int zPos = backViewDistance; zPos < initialGrassCount; zPos++)
        {
            
            var terrain = Instantiate(terrainList[0]);

            terrain.transform.position = new Vector3(0,0,zPos);

            if(terrain is Grass grass)
                grass.setTreePercentage(zPos < -1 ? 1 : 0);

            terrain.Generate(horizontalSize);
            activeTerrainDict[zPos] = terrain;
        }

        for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
        {
            spawnRandomTerrain(zPos);
        }

        onUpdateTerrainLimit.Invoke(horizontalSize,travelDistance+backViewDistance);
        
    }
    

    private Terrain spawnRandomTerrain(int zPos){
        Terrain comparatorTerrain = null;
        int randomIndex;
        for (int z = -1; z >= -3; z--)
        {
           var checkPos = zPos + z;

           if(comparatorTerrain == null){
            comparatorTerrain = activeTerrainDict[checkPos];
            continue;
           }
           else if(comparatorTerrain.GetType() != activeTerrainDict[checkPos].GetType()  )
           {
                randomIndex = Random.Range(0,terrainList.Count);
                return SpawnTerrain(terrainList[randomIndex], zPos);
           }
           else
           {
                continue;
           }
        }
        var candidateTerrrain = new List<Terrain>(terrainList);
        for (int i = 0; i < candidateTerrrain.Count; i++)
        {
            if(candidateTerrrain.GetType() == candidateTerrrain[i].GetType())
            {
                candidateTerrrain.Remove(candidateTerrrain[i]);
                break;
            }
        }

        randomIndex = Random.Range(0,candidateTerrrain.Count);
        return SpawnTerrain(candidateTerrrain[randomIndex],zPos);
    }
    public Terrain SpawnTerrain (Terrain terrain, int zPos) {
        terrain = Instantiate(terrain);
        terrain.transform.position =  new Vector3(0,0,zPos);
        terrain.Generate(horizontalSize);
        activeTerrainDict[zPos] = terrain;
        SpawnCoin(horizontalSize, zPos);
        return terrain;
    }
    public Coin SpawnCoin(int horizontalSize, int zPos, float probability = 0.3f){
        if(probability == 0)
        return null;

        List<Vector3> SpawnPosCandidateList = new List<Vector3>();
        for (int x = -horizontalSize/2; x <= horizontalSize/2; x++)
        {
            var spawnPos = new Vector3(x,0,zPos)
;            if(Tree.AllPositions.Contains(spawnPos) == false)
            SpawnPosCandidateList.Add(spawnPos);
        }
        if(probability >= Random.value){

            var index = Random.Range(0,coinList.Count);
            var spawnPostIndex = Random.Range(0, SpawnPosCandidateList.Count);
            return Instantiate(coinList[index],
                               SpawnPosCandidateList[spawnPostIndex],
                               Quaternion.identity);
        }
    return null;
    }
    public void UpdateTravelDistance (Vector3 targetPosition) {
        if(targetPosition.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt(targetPosition.z);
            UpdateTerrain();
            onScoreUpdate.Invoke(GetScore());
        }
    }
    public void AddCoin(int value=1) {
        this.coin += value;
    }
    private int GetScore () {
        return travelDistance + coin;
    }
    public void UpdateTerrain () {
        var destroyPos = travelDistance - 1 + backViewDistance;
        Destroy(activeTerrainDict[destroyPos].gameObject);
        activeTerrainDict.Remove(destroyPos);

        var spawnPosition = travelDistance - 1 + forwardViewDistance;

        spawnRandomTerrain(spawnPosition);
        onUpdateTerrainLimit.Invoke(horizontalSize,travelDistance+backViewDistance);
    }
}
