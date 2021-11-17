using System;
using System.Collections.Generic;
using UnityEngine;
// transform.forward - right Side, transform.right - down side
public class AdjacentTilesChecker : MonoBehaviour
{
    GridTileProcessor _up;
    GridTileProcessor _down;
    GridTileProcessor _left;
    GridTileProcessor _right;

    public GridTileProcessor Up => _up;
    public GridTileProcessor Down => _down;
    public GridTileProcessor Left => _left;
    public GridTileProcessor Right => _right;

    [SerializeField] float _rayLength = 0.6f;

    private RaycastHit _hit;

    private void OnEnable()
    {
        GridGenerator.Instance.OnFinishedGridGeneration += SetAdjacentReferences;
    }

    private void OnDisable()
    {
        GridGenerator.Instance.OnFinishedGridGeneration -= SetAdjacentReferences;
    }


    private void SetAdjacentReferences(List<GridTileProcessor> _gridTiles)
    {
        //up 
        if(Physics.Raycast(transform.position, -transform.right * _rayLength, out _hit))
            _up = _hit.collider.GetComponent<GridTileProcessor>();

        //down
        if(Physics.Raycast(transform.position, transform.right * _rayLength, out _hit))
            _down = _hit.collider.GetComponent<GridTileProcessor>();

        //left
        if(Physics.Raycast(transform.position, -transform.forward * _rayLength, out _hit))
            _left = _hit.collider.GetComponent<GridTileProcessor>();

        //right
        if(Physics.Raycast(transform.position, transform.forward * _rayLength, out _hit))
            _right = _hit.collider.GetComponent<GridTileProcessor>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        if (_left != null)
            Gizmos.DrawWireCube(transform.position + -Vector3.forward + new Vector3(0, 0.25f, 0), Vector3.one);

        if (_right != null)
            Gizmos.DrawWireCube(transform.position + Vector3.forward + new Vector3(0, 0.25f, 0), Vector3.one);

        if (_up != null)
            Gizmos.DrawWireCube(transform.position + -Vector3.right + new Vector3(0, 0.25f, 0), Vector3.one);

        if (_down != null)
            Gizmos.DrawWireCube(transform.position + Vector3.right + new Vector3(0, 0.25f, 0), Vector3.one);
    }
}