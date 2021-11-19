using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGridPlacement : MonoBehaviour
{
    /*
        - receives a GridTileProcessor picked random from enemy spawner 
        - return and ask for a different gridtileprocessor if occupied (may not be necessary if enemy spawner tracks unoccupied tiles), 
        - if gridtileprocessor(tile) is available, pick a random y rotation value by 90 (0, 90, 180, 270) to start with 
        - modify rotation of object to the starting y rotation value 
        - loop through all 4 orientations by incrementing y rotation value by 90, 
            - avtivate and check TileDetector and see if all gridtiles inside it are available 
            - only continue loop if at least one of the detected tiles are occupied 
            - if all TileDetector checks results in failure, ask enemyspawner for a different gridTileprocessor  
     */
    TileDetector _tileDetector;
    float yRotationInterval = 90f;

    private void Awake()
    {
        _tileDetector = GetComponentInChildren<TileDetector>();
        _tileDetector.gameObject.SetActive(false);
    }

    public bool IsPlacementDetermined()
    {
        _tileDetector.gameObject.SetActive(false);

        //[Debug]randomizing start position rarely causes a bug where enemy is spawned hanging from the map
        float initialYRotation = RandomizeStartingRotation();
        Debug.Log("Initial Rotation:" +transform.eulerAngles.y);
        Debug.Log("Random Rotation:" + initialYRotation);
        //float initialYRotation = 0;
        transform.Rotate(new Vector3(0f, initialYRotation, 0f), Space.Self);
        Debug.Log("Next Rotation:" + transform.eulerAngles.y);

        //int rotationMultiplier = CalculateRotationMultiplier();
        int rotationMultiplier = 1;

        for (int i = 0; i < 4; i++)
        {
            _tileDetector.gameObject.SetActive(false);

            float newRotation = rotationMultiplier * yRotationInterval * i;
            transform.Rotate(new Vector3(0f, newRotation, 0f), Space.Self);

            Debug.Log("Rotation:" + newRotation);

            _tileDetector.gameObject.SetActive(true);

            if (_tileDetector.AllTilesAvailable())
            {
                foreach (var gridTiles in _tileDetector.GridTilesHit)
                {
                    gridTiles.IsOccupied = true;
                }
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
