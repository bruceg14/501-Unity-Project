using UnityEngine;

public class GameManager
{
    public GameManager(Player player, CameraController cameraController, MapManager mapManager, AssetManager assetManager, InputManager inputManager, UIManager uiManager)
    {
        _player = player;
        _cameraController = cameraController;
        _mapManager = mapManager;
        _assetManager = assetManager;
        _inputManager = inputManager;
        _uiManager = uiManager;

        _uiManager.OnNewGame += StartNewGame;
        _uiManager.OnExit += Exit;
    }

    public void OnUpdate()
    {
        if (_isGameStarted)
        {
            _timeSinceStart += Time.deltaTime;
            if (_player.IsDead)
            {
                _isGameStarted = false;
                _uiManager.EnterMenu();
                _timeSinceStart = 0f;
            }
            var movement = _inputManager.GetHorizontalAxis();
            if (Mathf.Abs(movement) > IgnorableValue)
            {
                _player.Move(movement);
            }

            if (_inputManager.GetKeyPressed("Jump"))
            {
                _player.Jump();
            }

            _uiManager.TimeElapsed = _timeSinceStart;
            _uiManager.DistanceTravelled = _player.LevelsVisited;
        }
    }

    public void OnDestroy()
    {
        _uiManager.OnNewGame -= StartNewGame;
        _uiManager.OnExit -= Exit;
    }

    private const float IgnorableValue = 0.001f;

    private Player _player;
    private CameraController _cameraController;
    private MapManager _mapManager;
    private AssetManager _assetManager;
    private InputManager _inputManager;
    private UIManager _uiManager;
    private bool _isGameStarted;
    private float _timeSinceStart;

    private void StartNewGame()
    {
        _player.Reset();
        _cameraController.Restart();
        _mapManager.CreateMap();
        _isGameStarted = true;
        _uiManager.EnterPlaymode();
        _timeSinceStart = 0f;
    }

    private void Exit()
    {
        Application.Quit();
    }
}
