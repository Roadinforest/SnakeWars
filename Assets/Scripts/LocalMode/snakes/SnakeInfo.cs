using Gameplay.SnakeLogic;
using Network.Schemas;

namespace LocalMode.Snakes
{
    // 数据模式，但是单机模式下只用一个，所以没有Schema
    public class SnakeInfo
    {
        public Snake Snake;
        public PlayerSchema Player;
    }
}