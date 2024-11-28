using Gameplay;
using Gameplay.Common;
using Gameplay.Player;
using Gameplay.SnakeLogic;
using Infrastructure;


using Services;
using UnityEngine;
using Gameplay.Animations;
using Network.Services.Snakes;
using Network.Schemas;

namespace LocalMode.Factory
{
    public class LocalSnakesFactory
    {
        private const string PlayerSnakePath = "Snake/Player Snake";
        private const string SnakeDetailPath = "Snake/Body Detail";

        private readonly Assets _assets;
        private readonly StaticDataService _staticData;
        private readonly CameraProvider _cameraProvider;
        private Snake _onlySnake;//唯一的小蛇蛇
        private SnakeInfo _onlySnakeInfo;
        private readonly SnakesRegistry _snakes;

        public LocalSnakesFactory(Assets assets, SnakesRegistry snakes, StaticDataService staticData, CameraProvider cameraProvider)
        {
            _assets = assets;
            _staticData = staticData;
            _cameraProvider = cameraProvider;
            _onlySnakeInfo = new SnakeInfo();
            _snakes = snakes;
        }

        public Snake CreateLocalSnake()
        {

            //填写身份证阶段
            PlayerSchema _playerSchema = new PlayerSchema();
            _playerSchema.username = "Player";

            _playerSchema.position = new Vector2Schema();
            _playerSchema.position.x = 0;
            _playerSchema.position.y = 0;

            _playerSchema.skinId = (byte)Random.Range(0, 7);
            _playerSchema.size = (byte)3;//默认长度一开始为3

            var data = _staticData.ForSnake();
            var skin = _staticData.ForSnakeSkin(_playerSchema.skinId);
            _onlySnake = CreateSnake(PlayerSnakePath, new Vector3(0, 0, 0), skin, data.MovementSpeed);

            _onlySnakeInfo.Player = _playerSchema;
            _onlySnakeInfo.Snake = _onlySnake;

            // 上户口
            _snakes.Add("Player", _playerSchema, _onlySnake);

            //本地小蛇
            var localSnake = _onlySnake.GetComponent<LocalSnake>();
            localSnake.Initialize(_playerSchema);

            _onlySnake.GetComponentInChildren<PlayerAim>().Construct(data.MovementSpeed, data.RotationSpeed);
            _cameraProvider.Follow(_onlySnake.Head.transform);

            return _onlySnake;
        }

        private Snake CreateSnake(string pathToPrefab, Vector3 position, Material skin, float movementSpeed)
        {
            var snake = _assets.Instantiate<Snake>(pathToPrefab, position, Quaternion.identity, null);
            snake.Head.Construct(movementSpeed);
            snake.GetComponentInChildren<SnakeSkin>().ChangeTo(skin);
            return snake;
        }


        public void RemoveSnake()
        {
            Object.Destroy(_onlySnake.gameObject);
        }

        public void AddSnakeDetail(string snakeId, int count)
        {
            var skin = _staticData.ForSnakeSkin(_onlySnakeInfo.Player.skinId);

            for (var i = 0; i < count; i++)
                _onlySnake.AddDetail(CreateSnakeDetail(_onlySnakeInfo.Snake.Head.transform, _onlySnake.transform, skin));
        }

        public void RemoveSnakeDetails(string snakeId, int count)
        {

            for (var i = 0; i < count; i++)
                Object.Destroy(_onlySnake.RemoveDetail());
        }

        private GameObject CreateSnakeDetail(Transform head, Transform parent, Material skin)
        {
            var spawnPoint = head.position - head.forward;
            var instance = _assets.Instantiate<SnakeSkin>(SnakeDetailPath, spawnPoint, head.rotation, parent);
            instance.ChangeTo(skin);
            return instance.gameObject;
        }
    }
}