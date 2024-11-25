using Gameplay;
using Gameplay.Common;
using Gameplay.Player;
using Gameplay.SnakeLogic;
using Infrastructure;

//using Network.Extensions;
//using Network.Schemas;
//using Network.Services.Snakes;

//using Network.Schemas;
using LocalMode.Snakes;
using LocalMode.Extensions;

using Services;
using UnityEngine;

namespace LocalMode.Factory
{
    public class LocalSnakesFactory
    {
        private const string PlayerSnakePath = "Snake/Player Snake";
        private const string RemoteSnakePath = "Snake/Remote Snake";
        private const string SnakeDetailPath = "Snake/Body Detail";

        private readonly Assets _assets;
        private readonly LocalSnakesRegistry _snakes;
        private readonly StaticDataService _staticData;
        private readonly CameraProvider _cameraProvider;

        public LocalSnakesFactory(Assets assets, LocalSnakesRegistry snakes, StaticDataService staticData, CameraProvider cameraProvider)
        {
            _assets = assets;
            _snakes = snakes;
            _staticData = staticData;
            _cameraProvider = cameraProvider;
        }

        public Snake CreateLocalSnake(string key)
        {
            Debug.Log("Try to create Local Snake");
            var data = _staticData.ForSnake();
            var skin = _staticData.ForSnakeSkin(0);

            var snake = CreateSnake(PlayerSnakePath, new Vector3(0, 0, 0), skin, data.MovementSpeed);

            //New
            var localSnake = snake.GetComponent<LocalSnake>();
            localSnake.Initialize();

            _snakes.Add(key, snake);

            snake.GetComponentInChildren<PlayerAim>().Construct(data.MovementSpeed, data.RotationSpeed);
            _cameraProvider.Follow(snake.Head.transform);

            return snake;
        }

        private Snake CreateSnake(string pathToPrefab, Vector3 position, Material skin, float movementSpeed)
        {
            var snake = _assets.Instantiate<Snake>(pathToPrefab, position, Quaternion.identity, null);
            snake.Head.Construct(movementSpeed);
            snake.GetComponentInChildren<SnakeSkin>().ChangeTo(skin);
            return snake;
        }


        //        public Snake CreatePlayerSnake(string key, PlayerSchema schema)
        //        {
        //            var data = _staticData.ForSnake();

        //            var remoteSnake = CreateRemoteSnake(key, schema, PlayerSnakePath);
        //            remoteSnake.GetComponentInChildren<PlayerAim>().Construct(data.MovementSpeed, data.RotationSpeed);

        //            _cameraProvider.Follow(remoteSnake.Head.transform);
        //            return remoteSnake;
        //        }

        //        public Snake CreateRemoteSnake(string key, PlayerSchema schema) =>
        //            CreateRemoteSnake(key, schema, RemoteSnakePath);

        public void RemoveSnake(string key)
        {
            //var info = _snakes[key];
            //_snakes.Remove(key);
            //Object.Destroy(info.Snake.gameObject);
        }

        public void AddSnakeDetail(string snakeId, int count)
        {
            var snakeInfo = _snakes[snakeId];
            var skin = _staticData.ForSnakeSkin(1);

            for (var i = 0; i < count; i++)
                snakeInfo.AddDetail(CreateSnakeDetail(snakeInfo.Head.transform, snakeInfo.transform, skin));
        }

        public void RemoveSnakeDetails(string snakeId, int count)
        {
            var snakeInfo = _snakes[snakeId];

            for (var i = 0; i < count; i++)
                Object.Destroy(snakeInfo.RemoveDetail());
        }

        //        private Snake CreateRemoteSnake(string key, PlayerSchema schema, string pathToPrefab)
        //        {
        //            var data = _staticData.ForSnake();
        //            var skin = _staticData.ForSnakeSkin(schema.skinId);

        //            var snake = CreateSnake(pathToPrefab, schema.position.ToVector3(), skin, data.MovementSpeed);
        //            snake.GetComponent<UniqueId>().Construct(key);
        //            _snakes.Add(key, schema, snake);

        //            var remoteSnake = snake.GetComponent<RemoteSnake>();
        //            remoteSnake.Initialize(schema);

        //            return snake;
        //        }




        private GameObject CreateSnakeDetail(Transform head, Transform parent, Material skin)
        {
            var spawnPoint = head.position - head.forward;
            var instance = _assets.Instantiate<SnakeSkin>(SnakeDetailPath, spawnPoint, head.rotation, parent);
            instance.ChangeTo(skin);
            return instance.gameObject;
        }
    }
}