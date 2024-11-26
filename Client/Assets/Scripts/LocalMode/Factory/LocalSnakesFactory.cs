﻿using Gameplay;
using Gameplay.Common;
using Gameplay.Player;
using Gameplay.SnakeLogic;
using Infrastructure;

using LocalMode.Snakes;
using LocalMode.Extensions;

using Services;
using UnityEngine;
using Gameplay.Animations;
using Network.Schemas;

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
        private Snake _OnlySnake;//唯一的小蛇蛇

        public LocalSnakesFactory(Assets assets, LocalSnakesRegistry snakes, StaticDataService staticData, CameraProvider cameraProvider)
        {
            _assets = assets;
            _snakes = snakes;
            _staticData = staticData;
            _cameraProvider = cameraProvider;
        }

        public Snake CreateLocalSnake()
        {
            Debug.Log("Try to create Local Snake");

            //填写身份证阶段
            SnakeInfo snakeInfo = new SnakeInfo();

            PlayerSchema _playerSchema = new PlayerSchema();
            _playerSchema = new PlayerSchema();
            _playerSchema.username = "Player";

            _playerSchema.position = new Vector2Schema();
            _playerSchema.position.x = 0;
            _playerSchema.position.y = 0;

            _playerSchema.skinId = (byte)Random.Range(0, 7);
            _playerSchema.size = (byte)3;//默认长度一开始为3
            snakeInfo.Player = _playerSchema;

            var data = _staticData.ForSnake();
            var skin = _staticData.ForSnakeSkin(_playerSchema.skinId);

            var snake = CreateSnake(PlayerSnakePath, new Vector3(0, 0, 0), skin, data.MovementSpeed);
            snakeInfo.Snake = snake;

            //添加户口，默认ID为Player
            _snakes.Add("Player", snakeInfo);

            //本地小蛇
            var localSnake = snake.GetComponent<LocalSnake>();
            localSnake.Initialize(_playerSchema);


            snake.GetComponentInChildren<PlayerAim>().Construct(data.MovementSpeed, data.RotationSpeed);
            _cameraProvider.Follow(snake.Head.transform);
            //_OnlySnake=snake;

            return snake;
        }

        private Snake CreateSnake(string pathToPrefab, Vector3 position, Material skin, float movementSpeed)
        {
            var snake = _assets.Instantiate<Snake>(pathToPrefab, position, Quaternion.identity, null);
            _OnlySnake=snake;
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

        public void RemoveSnake()
        {
            Object.Destroy(_OnlySnake.gameObject);
            Debug.Log("RemoveSnake in LocalSnakesFactory");
        }

        public void AddSnakeDetail(string snakeId, int count)
        {
            var snakeInfo = _snakes[snakeId];
            var skin = _staticData.ForSnakeSkin(snakeInfo.Player.skinId);

            for (var i = 0; i < count; i++)
                snakeInfo.Snake.AddDetail(CreateSnakeDetail(snakeInfo.Snake.Head.transform, snakeInfo.Snake.transform, skin));
        }

        public void RemoveSnakeDetails(string snakeId, int count)
        {
            var snakeInfo = _snakes[snakeId];

            for (var i = 0; i < count; i++)
                Object.Destroy(snakeInfo.Snake.RemoveDetail());
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