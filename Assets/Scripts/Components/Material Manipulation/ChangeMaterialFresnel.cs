using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialFresnel : MonoBehaviour
{
    MeshRenderer _meshRenderer;

    public void ChangeFresnel(float fresnelValue)
    {
        if(_meshRenderer == null)
            _meshRenderer = GetComponent<MeshRenderer>();

        Material[] mats = _meshRenderer.materials;
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat("_FresnelPower", fresnelValue);
        }

        _meshRenderer.materials = mats;
    }
}
