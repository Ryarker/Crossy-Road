using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Terrain
{
    [SerializeField] List<GameObject> treePrefabsList;
    [SerializeField, Range(0,1)] float treeProbability;
    public void setTreePercentage (float newProbability){
        this.treeProbability = Mathf.Clamp01(newProbability);
    }
    public override void Generate(int size)
    {
        base.Generate(size);

        var limit = Mathf.FloorToInt((float)size / 2);
        var treeCount = Mathf.FloorToInt((float)size * treeProbability);

        //membuat daftar posisi yang masih kosong
        List<int> emptyPosition = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(i);
        }


        for (int i = 0; i < treeCount; i++)
        {
            //memilih posisi posong secara random
            var randomIndex = Random.Range(0,emptyPosition.Count);
            var pos = emptyPosition[randomIndex];

            //posisi yg terpilih dihapus dari daftar posisi kosong
            emptyPosition.RemoveAt(randomIndex);
            SpawnRandomTree(pos);

        }

        SpawnRandomTree(-limit - 1);
        SpawnRandomTree(-limit - 2);
        SpawnRandomTree(-limit - 3);
        SpawnRandomTree(-limit - 4);
        SpawnRandomTree(-limit - 5);
        SpawnRandomTree(-limit - 6);
        SpawnRandomTree(-limit - 7);
        SpawnRandomTree(-limit - 8);
        SpawnRandomTree(-limit - 9);
        SpawnRandomTree(-limit - 10);
        SpawnRandomTree(limit + 1);
        SpawnRandomTree(limit + 2);
        SpawnRandomTree(limit + 3);
        SpawnRandomTree(limit + 4);
        SpawnRandomTree(limit + 5);
        

    }
    private void SpawnRandomTree (int xPos) {

        
            //set pohon ke posisi yang terpilih
            var randomIndex = Random.Range(0,treePrefabsList.Count);
            var prefab = treePrefabsList[randomIndex];


            var tree = Instantiate(
			prefab, 
			 new Vector3(xPos, 0, this.transform.position.z), 
			 Quaternion.identity, 
			 transform);
    }
}
