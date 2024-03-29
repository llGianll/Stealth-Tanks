using UnityEngine;

public enum SpawnScreenSide { Top, Bottom }

/*
    [Revisit Notes] LineBomber class' purpose is to determine the start and end point of the line, then spawn the StealthBomber, then initialize the velocity and unsorted targets
    It also spawns a 'Despawner' collider at the end of the Stealth Bomber's path to send the object back to the ObjectPooler
 */

public class LineBomber : Weapon
{
    [SerializeField] GridSizeSO _gridSizeSO;

    [Header("Subclass Variables - Bomber Spawn")]
    [SerializeField] string _bomberID;
    [SerializeField] float _ySpawnOffset = 3f;
    [SerializeField] float _spawnAndEndOffset = 5f;
    [SerializeField] SpawnScreenSide _spawnSide;

    [Header("Subclass Variables - Bomber Properties")]
    [SerializeField] float _bomberFlightSpeed = 10f;

    Vector3 _startPoint, _endpoint;
    Vector3 _moveDirection;
    bool _isHorizontal;

    protected override void UseWeapon()
    {
        GetStartAndEndPoint();

        if (CanFire())
        {
            GameObject bomber = PooledObjectManager.Instance.GetPooledObject(_bomberID);
            bomber.transform.position = new Vector3(_startPoint.x, _ySpawnOffset, _startPoint.z);
            bomber.transform.rotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
            bomber.GetComponent<StealthBomber>().InitializeTargets(_targetMode.Target, _isHorizontal, _projectileID, _spawnSide);
            bomber.SetActive(true);
            bomber.GetComponent<StealthBomber>().SetVelocity((_endpoint - _startPoint).normalized * _bomberFlightSpeed);


            GameObject despawner = PooledObjectManager.Instance.GetPooledObject("Despawner");
            despawner.transform.position = new Vector3(_endpoint.x, _ySpawnOffset, _endpoint.z);
            despawner.SetActive(true);
        }
    }

    private void GetStartAndEndPoint()
    {
        //[Revisit Notes] Since these variables are just used to determine the vector of the bombing, _targetMode.Target's element order doesn't matter
        if (_targetMode.Target.Count <= 0)
            return;

        Vector3 firstTile = _targetMode.Target[0].transform.position;
        Vector3 nextTile = _targetMode.Target[1].transform.position;

        CheckIfHorizontal(firstTile, nextTile);

        //get unit vector of the position of the first two tiles on the list 
        _moveDirection = (firstTile - nextTile).normalized;
        Vector3 differenceToOrigin = OriginDifference(firstTile);
        _moveDirection = FlipDirection(_moveDirection);

        _startPoint = StartPointWithOffset(differenceToOrigin);
        _endpoint = _startPoint + (_moveDirection * (GridSizeLine() +_spawnAndEndOffset*2)); 

        ModifySpawnLocation(_spawnSide);
    }

    private float GridSizeLine()
    {
        if (_isHorizontal)
            return _gridSizeSO.ZCount;
        else
            return _gridSizeSO.XCount;
    }


    #region Calculating Start and End Point Helper Functions
    private void ModifySpawnLocation(SpawnScreenSide spawnSide)
    {

        switch (spawnSide)
        {
            case SpawnScreenSide.Top:
                if (_isHorizontal)
                {
                    SwapStartAndEndPoint();
                }
                break;
            case SpawnScreenSide.Bottom:
                if (!_isHorizontal)
                {
                    SwapStartAndEndPoint();
                }
                break;
            default:
                break;
        }
    }

    private void SwapStartAndEndPoint()
    {
        Vector3 temp = _endpoint;
        _endpoint = _startPoint;
        _startPoint = temp;

        _moveDirection = -_moveDirection;
    }

    private Vector3 StartPointWithOffset(Vector3 differenceToOrigin)
    {
        Vector3 startPoint;

        if (_isHorizontal)
            startPoint = (Vector3.zero + differenceToOrigin) + new Vector3(0, 0, -_spawnAndEndOffset);
        else
            startPoint = (Vector3.zero + differenceToOrigin) + new Vector3(-_spawnAndEndOffset, 0, 0);

        return startPoint;
    }

    private Vector3 FlipDirection(Vector3 tileDirection)
    {
        //[Revisit Notes] flip the normal's direction to how I want the bomber to move depending if going horizontal or vertical 
        if (_isHorizontal)
        {
            if (Vector3.Dot(Vector3.forward, tileDirection) < 0)
                return -tileDirection;
            else
                return tileDirection;
        }
        else
        {
            if (Vector3.Dot(Vector3.down, tileDirection) < 0)
                return -tileDirection;
            else
                return tileDirection;
        }
    }

    private Vector3 OriginDifference(Vector3 firstTile)
    {
        //[Revisit Notes] Find the starting point of the line relative to the clicked tile, if horizontal: get the first tile of the row where the clicked tile is 
        //if vertical: get the first tile of the column, GridGenerator pivot at Vector.zero
        if (_isHorizontal) 
            return new Vector3(firstTile.x, 0, 0);
        else
            return new Vector3(0, 0, firstTile.z);
    }

    private void CheckIfHorizontal(Vector3 firstTile, Vector3 nextTile)
    {
        if (firstTile.z != nextTile.z) //if the z of the two tiles not equal, the orientation is horizontal
            _isHorizontal = true;
        else
            _isHorizontal = false;

    } 
    #endregion

}
