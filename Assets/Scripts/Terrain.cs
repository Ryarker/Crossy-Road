using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] GameObject tilePrefabs;
    
    protected float horizontalSize;

    public virtual void Generate(int size){
        horizontalSize = size;
        if(size==0)
            return;

        if((float) size % 2==0)
            size -= 1;

        int moveLimit = Mathf.FloorToInt((float) size/2);

        for (int i = -moveLimit; i <= moveLimit; i++)
        {
         SpawnTile(i);
        }
        
       var leftBoundaryTile1 = SpawnTile(-moveLimit - 1);
       var leftBoundaryTile2 = SpawnTile(-moveLimit - 2);
       var leftBoundaryTile3 = SpawnTile(-moveLimit - 3);
       var leftBoundaryTile4 = SpawnTile(-moveLimit - 4);
       var leftBoundaryTile5 = SpawnTile(-moveLimit - 5);
       var leftBoundaryTile6 = SpawnTile(-moveLimit - 6);
       var leftBoundaryTile7 = SpawnTile(-moveLimit - 7);
       var leftBoundaryTile8 = SpawnTile(-moveLimit - 8);
       var leftBoundaryTile9 = SpawnTile(-moveLimit - 9);
       var leftBoundaryTile10 = SpawnTile(-moveLimit - 10);
       var rightBoundaryTIle1 = SpawnTile(moveLimit + 1);
       var rightBoundaryTIle2 = SpawnTile(moveLimit + 2);
       var rightBoundaryTIle3 = SpawnTile(moveLimit + 3);
       var rightBoundaryTIle4 = SpawnTile(moveLimit + 4);
       var rightBoundaryTIle5 = SpawnTile(moveLimit + 5);

       darkenObject(leftBoundaryTile1);
       darkenObject(leftBoundaryTile2);
       darkenObject(leftBoundaryTile3);
       darkenObject(leftBoundaryTile4);
       darkenObject(leftBoundaryTile5);
       darkenObject(leftBoundaryTile6);
       darkenObject(leftBoundaryTile7);
       darkenObject(leftBoundaryTile8);
       darkenObject(leftBoundaryTile9);
       darkenObject(leftBoundaryTile10);
       darkenObject(rightBoundaryTIle1);
       darkenObject(rightBoundaryTIle2);
       darkenObject(rightBoundaryTIle3);
       darkenObject(rightBoundaryTIle4);
       darkenObject(rightBoundaryTIle5);
       
    }
    private GameObject SpawnTile(int xPos){
        var go = Instantiate(tilePrefabs, transform);

        go.transform.localPosition = new Vector3(xPos,0,0);  

        return go;
    }
    private void darkenObject(GameObject go){
        var renderers = go.GetComponentsInChildren<MeshRenderer>(includeInactive: true);

        foreach (var rend in renderers)
        {
            rend.material.color *= Color.grey;
        }
    }
}
