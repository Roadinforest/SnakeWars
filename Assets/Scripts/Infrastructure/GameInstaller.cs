using Reflex.Core;
using Reflex.Logging;
using UnityEngine;

using Network.Services;
using Network.Services.Factory;
using Network.Services.RoomHandlers;
using Network.Services.Snakes;
using Services;
using Services.Leaders;
using UI.Factory;
using LocalMode.Factory;
using LocalMode.Snakes;

using SnakesFactory = Network.Services.Factory.SnakesFactory;
using AppleFactory = Network.Services.Factory.AppleFactory;
using VfxFactory = Network.Services.Factory.VfxFactory;
using SnakesDestruction = Network.Services.Factory.SnakesDestruction;
//using SnakesRegistry = LocalMode.Snakes.SnakesRegistry;


namespace Infrastructure
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            Debug.Log("GameInstaller installed start");
            builder.AddSingleton(typeof(Injector));
            builder.AddSingleton(typeof(CameraProvider));
            builder.AddSingleton(typeof(UIFactory));
            builder.AddSingleton(typeof(Game));
            builder.AddSingleton(typeof(InputService));
            builder.AddSingleton(typeof(Assets));
            builder.AddSingleton(typeof(StaticDataService));

            //New
            builder.AddSingleton(typeof(LocalGameFactory));
            builder.AddSingleton(typeof(LocalSnakesFactory));
            builder.AddSingleton(typeof(LocalSnakesRegistry));
            builder.AddSingleton(typeof(LocalSnakesDestruction));
            builder.AddSingleton(typeof(LocalVfxFactory));


            builder.AddSingleton(typeof(LeaderboardService));
            builder.AddSingleton(typeof(SnakesFactory));
            builder.AddSingleton(typeof(AppleFactory));
            builder.AddSingleton(typeof(VfxFactory));
            builder.AddSingleton(typeof(SnakesDestruction));

            builder.AddSingleton(typeof(SnakesRegistry));
            builder.AddSingleton(typeof(NetworkClient));
            builder.AddSingleton(typeof(NetworkPlayersListener));
            builder.AddSingleton(typeof(NetworkApplesListener));
            builder.AddSingleton(typeof(NetworkStateInitializer), typeof(INetworkRoomHandler));
            builder.AddSingleton(typeof(NetworkStatusProvider), typeof(INetworkRoomHandler), typeof(INetworkStatusProvider));
            builder.AddSingleton(typeof(NetworkTransmitter), typeof(NetworkTransmitter), typeof(INetworkRoomHandler));
            builder.AddSingleton(typeof(NetworkGameFactory));
        }
    }
}