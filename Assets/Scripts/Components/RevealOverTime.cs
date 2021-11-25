using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealOverTime : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _unitModel;
    [Header("Material Manipulation")]
    [SerializeField] List<ChangeMaterialFresnel> _changeFresnel = new List<ChangeMaterialFresnel>();
    [SerializeField] float _revealSpeed = 1f;
    [SerializeField] float _minFresnel = 0f;
    [SerializeField] float _maxFresnel = 3f;

    public bool IsFinished { get; set; }

    private void OnEnable()
    {
        StartCoroutine(Reveal());
    }
    public IEnumerator Reveal()
    {
        IsFinished = false;

        float elapsedTime = 0f;
        float fresnelValue = 0f; //shader force field value

        while (elapsedTime < _revealSpeed)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / _revealSpeed;

            fresnelValue = Mathf.Lerp(_minFresnel, _maxFresnel, percent);

            foreach (var item in _changeFresnel)
            {
                item.ChangeFresnel(fresnelValue);
            }
            yield return null;
        }

        IsFinished = true;
        //_unitModel.SetActive(true);
        //gameObject.SetActive(false);
    }
}
