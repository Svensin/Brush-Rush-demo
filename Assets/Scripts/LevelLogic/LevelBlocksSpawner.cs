using System.Collections;
using System.Collections.Generic;
using LevelLogic;
using UnityEngine;

public class LevelBlocksSpawner : BlocksSpawner
{
    /// <summary>
    /// Spawns a block at wanted position
    /// </summary>
    /// <param name="section">section to spawn</param>
    /// <param name="positionToSpawn">spawn position</param>
    /// <param name="parent">parent of spawned block</param>
    public void SpawnBlock(Section section, Vector3 positionToSpawn, Transform parent = null)
    {
        GameObject levelBlock = Resources.Load<GameObject>(blockPrefabsPaths[(int) section]);
        
        InstantiateBlock(levelBlock,positionToSpawn,parent);
    }
}
