using UnityEngine;

namespace LevelLogic
{
    public abstract class BlocksSpawner : MonoBehaviour
    {
        protected BlockData spawnedBlock;

        /// <summary>
        /// Секція для блоків.
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
        /// Спавнить блок на задану позицію.
        /// </summary>
        /// <param name="objectToSpawn">Asset із <see cref="Resources"/> який треба заспавнити.</param>
        /// <param name="positionToSpawn">позиція спавну</param>
        /// <param name="parent">батько</param>
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
        /// Спавнить рандомний блок із префабів блоків на позицію.
        /// </summary>
        /// <param name="positionToSpawn">позиція</param>
        public virtual void SpawnRandomBlock(Vector3 positionToSpawn)
        {
            int sectionBlock = Random.Range(0, blockPrefabsPaths.Length);
            GameObject levelBlockToSpawn = Resources.Load<GameObject>(blockPrefabsPaths[sectionBlock]);

            GameObject spawnedBlockGameObject = Instantiate(levelBlockToSpawn, positionToSpawn, Quaternion.identity, level) as GameObject;

            spawnedBlock = spawnedBlockGameObject.GetComponent<BlockData>();

            InitializeLineContainers();
        }
    
        /// <summary>
        /// Ініціалізує усі контейнери для блоків.
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