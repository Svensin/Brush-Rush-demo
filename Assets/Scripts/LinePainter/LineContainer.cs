using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineContainer : MonoBehaviour
{
    /// <summary>
    /// об'єкт, що зберігатиме на собі лінії намалальовані пензнликом на об'єкті, на який накладено скрипт
    /// </summary>
    [SerializeField] private Transform container;

    /// <summary>
    /// публічне проперті, що дозволяє доступитись до контейнера при потребі(напр. при зіткненні)
    /// </summary>
    public Transform Container { get => container; set => container = value; }


    /// <summary>
    /// засовує лінію в конетйнер(батьківський об'єкт)
    /// </summary>
    /// <param name="line">Контейнер в якому зберігатиметься лінія</param> 
    public void SetLineToContainer(Transform line)
    {
        line.parent = container;
    }
}
