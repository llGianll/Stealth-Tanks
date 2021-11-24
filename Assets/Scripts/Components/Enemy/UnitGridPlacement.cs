using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGridPlacement : MonoBehaviour
{
    //[refactor]need to turn this class into Unit Placement class so that it's not limited to placing enemies 
    [SerializeField] TileDetector _tileDetector;
    float yRotationInterval = 90f;

    public Action<List<GridTileProcessor>> OnFinishedPlacement = delegate { };

    public bool IsPlacementDetermined()
    {
        int facingDirections = 4;
        _tileDetector.gameObject.SetActive(false);

        float initialYRotation = RandomizeStartingRotation();
        transform.Rotate(new Vector3(0f, initialYRotation, 0f), Space.Self);

        //int rotationMultiplier = CalculateRotationMultiplier();
        int rotationMultiplier = 1;

        for (int i = 0; i < facingDirections; i++)
        {
            _tileDetector.gameObject.SetActive(false);

            float newRotation = rotationMultiplier * yRotationInterval * i;
            transform.Rotate(new Vector3(0f, newRotation, 0f), Space.Self);

            _tileDetector.gameObject.SetActive(true);

            if (_tileDetector.AllTilesAvailable())
            {
                foreach (var gridTiles in _tileDetector.GridTilesHit)
                {
                    gridTiles.IsOccupied = true;
                }
                OnFinishedPlacement(_tileDetector.GridTilesHit);
                return true;
            }
        }

        return false;
    }

    private int CalculateRotationMultiplier()
    {
        //clockwise or counterclockwise
        int rand = UnityEngine.Random.Range(0, 2);
        return (rand == 0) ? -1 : 1;
    }

    private float RandomizeStartingRotation()
    {
        int rand = UnityEngine.Random.Range(0, 4);
        float yRotationValue = 0 + yRotationInterval * rand;

        return yRotationValue;
    }
}
