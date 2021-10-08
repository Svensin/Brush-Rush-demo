using System.Collections;
using System.Collections.Generic;
using LevelLogic;
using UnityEngine;

public class LevelBlocksSpawner : BlocksSpawner
{
    public void SpawnBlock(Section section, Vector3 positionToSpawn, Transform parent = null)
    {
        GameObject levelBlock = Resources.Load<GameObject>(blockPrefabsPaths[(int) section]);
        
        InstantiateBlock(levelBlock,positionToSpawn,parent);
    }
}
