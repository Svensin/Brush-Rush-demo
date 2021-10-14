using System.Collections.Generic;
using UnityEngine;

namespace LevelLogic
{
    public class MenuBlocksSpawner : BlocksSpawner
    {
        /// <summary>
        /// Set all papers to one paper lines container
        /// </summary>
        /// <param name="blockData">block data with setted paper lines container</param>
        /// <returns></returns>
        public BlockData ResetLineContainers(BlockData blockData)
        {
            blockData.TableLineContainer.Container = ScriptReferences.Instance.levelScript.BadLinesContainer.transform;

            for (int i = 0; i < blockData.PapersLineContainers.Count; i++)
            {
                blockData.PapersLineContainers[i].Container =
                    ScriptReferences.Instance.levelScript.PaperLinesContainer.transform;
            }

            return blockData;
        }
        /// <summary>
        /// Spawns a block at wanted position.
        /// </summary>
        /// <param name="sectionWithPrefabs">section we want to spawn</param>
        /// <param name="positionToSpawn">position we want to spawn at</param>
        /// <param name="parent">parent of spawned section</param>
        /// <returns></returns>
        public List<BlockData> SpawnBlocks(Section sectionWithPrefabs, Vector3 positionToSpawn, Transform parent = null)
        {
            var currentPosition = positionToSpawn;
            var blockSData = new List<BlockData>();
            GameObject[] menuBlocks = Resources.LoadAll<GameObject>(blockPrefabsPaths[(int)sectionWithPrefabs]);

            foreach (var block in menuBlocks)
            {
               var blockData = InstantiateBlock(block,currentPosition, parent);

                currentPosition = spawnedBlock.LastBlockPoint.position;
                blockSData.Add(blockData);
            }

            return blockSData;
        }
        /// <summary>
        /// Spawns random chosen from prefabs list block on wanted position
        /// </summary>
        /// <param name="positionToSpawn">spawn position</param>
        public override void SpawnRandomBlock(Vector3 positionToSpawn)
        {
            GameObject[] menuBlocks = Resources.LoadAll<GameObject>(blockPrefabsPaths[0]);

            var randomBlock = Random.Range(0, menuBlocks.Length);
            
            InstantiateBlock(menuBlocks[randomBlock],positionToSpawn);

            Resources.UnloadUnusedAssets();
        }
    }
}