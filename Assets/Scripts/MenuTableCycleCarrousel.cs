using System;
using System.Collections;
using System.Collections.Generic;
using LevelLogic;
using UnityEngine;

public class MenuTableCycleCarrousel : MonoBehaviour
{
    /// <summary>
    /// all level blocks which of menu table cycle is created of
    /// </summary>
    [Header("Blocks data")] [Space] [SerializeField]
    private List<BlockData> blocksData;
    
    //[SerializeField] private Transform[] blocks;
    /// <summary>
    /// Gameobject which contains all table blocks as a parent object
    /// </summary>
    [SerializeField] private Transform _blocks;
    /// <summary>
    /// rigidbody of all blocks
    /// </summary>
    [SerializeField] private Rigidbody _rigidbody;
    /// <summary>
    /// block spawner of menu table blocks
    /// </summary>
    [SerializeField] private BlocksSpawner _menuBlocksSpawner;
    
    public List<BlockData> BlocksData => blocksData;

    /// <summary>
    /// point by reaching which table blocks deactivate
    /// </summary>
    public Transform DeactivationPoint;
    /// <summary>
    /// point which is neede to remove unreachable for player papers
    /// </summary>
    public Transform BrushPoint;

   
    private BlockData _previousBlock;
    [Header("Important blocks")][Space]
    [SerializeField] private BlockData _currentBlock;
    [SerializeField] private BlockData _lastBlock;

    /// <summary>
    /// speed of table in menu
    /// </summary>
    [Range(2, 10)] [SerializeField] private int blockslSpeed;

    public BlockData CurrentBlock => _currentBlock;

    private void Update()
    {
        var prevPos = _blocks.position;
        var newPos = new Vector3(prevPos.x, prevPos.y, prevPos.z - blockslSpeed * Time.deltaTime);
        _rigidbody.MovePosition(newPos);
       
        
        if (_currentBlock.LastBlockPoint.position.z <=
            DeactivationPoint.position.z)
        {
            _previousBlock = _currentBlock;
            _currentBlock = _currentBlock.NextBlock;

            _previousBlock.transform.position = _lastBlock.LastBlockPoint.position;
            _lastBlock = _previousBlock;
        }
    }
    
    /// <summary>
    /// removes all unreachable for player papers
    /// </summary>
    /// <param name="blockData">block which papers has to be removed</param>
    public void RemoveUnreachablePapers(BlockData blockData)
    {
        var unreachablePapers = new List<Transform>();
        foreach (Transform paper in blockData.BlockDataPapers)
        {
            if (paper.GetChild(2).position.z <= BrushPoint.position.z)
            {
                unreachablePapers.Add(paper);
            }
        }

        foreach (var paper in unreachablePapers)
        {
            paper.parent = CurrentBlock.transform;
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        enabled = false;
    }


    private void Start()
    {
        blocksData = ((MenuBlocksSpawner)_menuBlocksSpawner).SpawnBlocks(LevelController.CurrentSection,Vector3.zero, _blocks);
        
        for (int i = 0; i < blocksData.Count; i++)
        {
            blocksData[i].NextBlock = i == blocksData.Count - 1 ? blocksData[0] : blocksData[i + 1];
        }

        _currentBlock = blocksData[0];
        _lastBlock = blocksData[blocksData.Count - 1];

    }
}