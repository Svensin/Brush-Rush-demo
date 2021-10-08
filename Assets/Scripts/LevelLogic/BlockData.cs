using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData : MonoBehaviour
{
    /// <summary>
    /// Колайдер стола.
    /// </summary>
    [Header("Table")]
    [SerializeField] private BoxCollider tableCollider;

    public BoxCollider TableCollider => tableCollider;
    
    /// <summary>
    /// Контейнер для ліній на столі.
    /// </summary>
    [SerializeField] private LineContainer tableLineContainer;
    public LineContainer TableLineContainer => tableLineContainer;

    /// <summary>
    /// Остання точка у блоці для приєднання <see cref="nextBlock"/> або префабу рівня.
    /// </summary>
    [Header("Transforms")]
    [SerializeField] private Transform lastBlockPoint;
    public Transform LastBlockPoint => lastBlockPoint;
    
    /// <summary>
    /// Контейнери для ліній на паперах.
    /// </summary>
    [Space] [Header("Papers")] [SerializeField]
    private List<LineContainer> papersLineContainers = new List<LineContainer>();
    public List<LineContainer> PapersLineContainers => papersLineContainers;

    /// <summary>
    /// Контейнер для паперів
    /// </summary>
    [SerializeField] Transform blockDataPapers;
    public Transform BlockDataPapers => blockDataPapers;
    
    
    /// <summary>
    /// Наступний за порядком <see cref="BlockData"/>.
    /// </summary>
    [Header("References")] [Space]
    [SerializeField] private BlockData nextBlock;
    public BlockData NextBlock
    {
        get => nextBlock;
        set => nextBlock = value;
    }

    /// <summary>
    /// Присвоює <see cref="Transform"/> переданого екземпляра як дитину для іншого <see cref="Transform"/>.
    /// </summary>
    /// <param name="parentTransform">батьківський <see cref="Transform"/></param>
    public void SetParentTo(Transform parentTransform)
    {
        this.transform.parent = parentTransform;
    }
    
}
