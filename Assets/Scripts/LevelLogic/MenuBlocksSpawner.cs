using System.Collections.Generic;
using UnityEngine;

namespace LevelLogic
{
    public class MenuBlocksSpawner : BlocksSpawner
    {
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

        public override void SpawnRandomBlock(Vector3 positionToSpawn)
        {
            GameObject[] menuBlocks = Resources.LoadAll<GameObject>(blockPrefabsPaths[0]);

            var randomBlock = Random.Range(0, menuBlocks.Length);
            
            InstantiateBlock(menuBlocks[randomBlock],positionToSpawn);

            Resources.UnloadUnusedAssets();
        }
    }
}