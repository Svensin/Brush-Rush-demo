using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData : MonoBehaviour
{
    /// <summary>
    /// Table collider
    /// </summary>
    [Header("Table")]
    [SerializeField] private BoxCollider tableCollider;

    public BoxCollider TableCollider => tableCollider;
    
    /// <summary>
    /// Container of lines on the table
    /// </summary>
    [SerializeField] private LineContainer tableLineContainer;
    public LineContainer TableLineContainer => tableLineContainer;

    /// <summary>
    /// Last in block for connection of <see cref="nextBlock"/> or level prefab.
    /// </summary>
    [Header("Transforms")]
    [SerializeField] private Transform lastBlockPoint;
    public Transform LastBlockPoint => lastBlockPoint;
    
    /// <summary>
    /// Container for lines on paper.
    /// </summary>
    [Space] [Header("Papers")] [SerializeField]
    private List<LineContainer> papersLineContainers = new List<LineContainer>();
    public List<LineContainer> PapersLineContainers => papersLineContainers;

    /// <summary>
    /// Container for papers
    /// </summary>
    [SerializeField] Transform blockDataPapers;
    public Transform BlockDataPapers => blockDataPapers;
    
    
    /// <summary>
    /// Next <see cref="BlockData"/>.
    /// </summary>
    [Header("References")] [Space]
    [SerializeField] private BlockData nextBlock;
    public BlockData NextBlock
    {
        get => nextBlock;
        set => nextBlock = value;
    }

    /// <summary>
    /// Sets <see cref="Transform"/> as a child of another <see cref="Transform"/>.
    /// </summary>
    /// <param name="parentTransform">parent of <see cref="Transform"/></param>
    public void SetParentTo(Transform parentTransform)
    {
        this.transform.parent = parentTransform;
    }
    
}
