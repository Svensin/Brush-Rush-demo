using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��� ������� ��'���� � ��������� ������ �����
/// </summary>
public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ScriptReferences.Instance.levelController.OnLevelFinish();
    }


}
