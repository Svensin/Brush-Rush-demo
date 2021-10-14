using UnityEngine;

namespace LevelLogic
{
    public abstract class BlocksSpawner : MonoBehaviour
    {
        protected BlockData spawnedBlock;

        /// <summary>
        /// Section for blocks.
        /// </summary>
        public enum Section
            {
                A = 0,
                B = 1,
                C = 2
            }

        [SerializeField] protected Transform level;
    
        [SerializeField] protected string[] blockPrefabsPaths;
    
        /// <summary>
        /// Spawns a block at wanted position.
        /// </summary>
        /// <param name="objectToSpawn">Asset is from <see cref="Resources"/> which has to be spawned.</param>
        /// <param name="positionToSpawn">spawn position</param>
        /// <param name="parent">parent of spawned block</param>
        public BlockData InstantiateBlock(Object objectToSpawn, Vector3 positionToSpawn, Transform parent = null)
        {
            GameObject spawnedBlockGameObject = parent == null ? 
                Instantiate(objectToSpawn as GameObject, positionToSpawn, Quaternion.identity, level) as GameObject : 
                Instantiate(objectToSpawn as GameObject, positionToSpawn, Quaternion.identity, parent) as GameObject;

            spawnedBlock = spawnedBlockGameObject.GetComponent<BlockData>();

            InitializeLineContainers();

            return spawnedBlock;
        }

        /// <summary>
        /// Spawns random chosen from prefabs list block on wanted position
        /// </summary>
        /// <param name="positionToSpawn">spawn position</param>
        public virtual void SpawnRandomBlock(Vector3 positionToSpawn)
        {
            int sectionBlock = Random.Range(0, blockPrefabsPaths.Length);
            GameObject levelBlockToSpawn = Resources.Load<GameObject>(blockPrefabsPaths[sectionBlock]);

            GameObject spawnedBlockGameObject = Instantiate(levelBlockToSpawn, positionToSpawn, Quaternion.identity, level) as GameObject;

            spawnedBlock = spawnedBlockGameObject.GetComponent<BlockData>();

            InitializeLineContainers();
        }
    
        /// <summary>
        /// Initializes all containers.
        /// </summary>
        private void InitializeLineContainers()
        {
            spawnedBlock.TableLineContainer.Container = ScriptReferences.Instance.levelScript.BadLinesContainer.transform;

            for (int i = 0; i < spawnedBlock.PapersLineContainers.Count; i++)
            {
                spawnedBlock.PapersLineContainers[i].Container = ScriptReferences.Instance.levelScript.PaperLinesContainer.transform;
            }

            ScriptReferences.Instance.levelScript.BlocksData.Add(spawnedBlock);
        }
    }

    
}