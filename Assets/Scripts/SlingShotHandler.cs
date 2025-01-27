using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class SlingShotHandler : MonoBehaviour
{
    [Header("Line Refrences")]
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    [Header("Transform Refrences")]
    [SerializeField] private Transform _leftStartPosition;
    [SerializeField] private Transform _rightStartPosition;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private Transform _idlePosition;

    [Header("Slingshot Stats")]
    [SerializeField] private float _maxDistance = 3.5f;
    [SerializeField] private float _shotForce = 5f;
    [SerializeField] private float _respawnTimer = 2f;

    [Header("Scripts")]
    [SerializeField] private SlingShotArea _slingShotArea;

    [Header("Killer Cat")]
    [SerializeField] private FlyingCat _catAssassinPrefab;
    [SerializeField] private float _CatPositionOffset = 2f;
    [SerializeField] private float _totalGravity = 20f;


    private FlyingCat _spawnedCat;
    private bool _clickedWithinArea;
    private bool _birdOnSlinghot;
    private Vector2 _slingShotLinesPosition;
    private Vector2 _direction;
    private Vector2 _directionNormalized;


    #region Awake
    private void Awake()
    {
        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;
        SpawnACat();
    }
    #endregion

    #region Update
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _slingShotArea.IsWithinSlingshotArea())
        {
            _clickedWithinArea = true;
        }

        if (Mouse.current.leftButton.isPressed && _clickedWithinArea && _birdOnSlinghot)
        {
            DrawSlingShot();
            PositionAndRotateCat();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && _birdOnSlinghot)
        {
            _clickedWithinArea = false;
            _spawnedCat.LaunchCat(_direction, _shotForce);
            _birdOnSlinghot = false;

            SetLines(_centerPosition.position);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _spawnedCat.IncreaseGravityScale(_totalGravity);
        }
        if (_spawnedCat._isDead)
        {
            StartCoroutine(SpawnACatAfterTime());
        }
 

    }
    #endregion

    #region SlingShot Methods
    private void DrawSlingShot()
    {
        
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        _slingShotLinesPosition = _centerPosition.position + Vector3.ClampMagnitude(touchPosition - _centerPosition.position, _maxDistance);

        SetLines(_slingShotLinesPosition);

        _direction = (Vector2)_centerPosition.position - _slingShotLinesPosition;
        _directionNormalized = _direction.normalized;
    }
    private void SetLines(Vector2 position)
    {
        if (!_leftLineRenderer.enabled && !_rightLineRenderer.enabled)
        {
            _leftLineRenderer.enabled = true;
            _rightLineRenderer.enabled = true;
        }

        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _leftStartPosition.position);

        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _rightStartPosition.position);
    }
    #endregion

    #region Cat Assassin Methods

    private void SpawnACat()
    {
        SetLines(_idlePosition.position);

        Vector2 dir = (_centerPosition.position - _idlePosition.position).normalized;
        Vector2 spawnedPosition = (Vector2)_idlePosition.position + dir * _CatPositionOffset; 

        _spawnedCat = Instantiate(_catAssassinPrefab, spawnedPosition, Quaternion.identity);
        _spawnedCat.transform.right = dir;
        _birdOnSlinghot = true;
    }
    
    private void PositionAndRotateCat()
    {
        _spawnedCat.transform.position = _slingShotLinesPosition + _directionNormalized * _CatPositionOffset;
        _spawnedCat.transform.right = _directionNormalized;
    }

    private IEnumerator SpawnACatAfterTime()
    {
        _spawnedCat._isDead = false;
        yield return new WaitForSeconds(_respawnTimer);
        Destroy(_spawnedCat.gameObject);
        SpawnACat();
    }

    #endregion
}
