# Snake Battle - Multiplayer Game

## 游戏简介 (Game Introduction)
《贪吃蛇大作战》是一款多人联机版的经典贪吃蛇游戏，提供本地单人、联机双人和多人三种模式。游戏设计有三个富有创意的地图，采用可爱丰富的动画效果和精美的地图，提供极致的视觉体验。技术上，使用了Reflex框架、Colyseus服务器和TypeORM数据库，支持跨端联机与多人实时竞技，给玩家带来流畅的游戏体验。

## Features
- **本地单人模式**：经典的单人贪吃蛇玩法，挑战自我。
- **联机双人模式**：与朋友在同一局域网内进行对战，进行即时联机对决。
- **多人在线模式**：跨平台实时竞技，与全球玩家共同对战。
- **精美地图**：三个独特的地图，每一张都拥有独特的挑战和风景。
- **动画效果**：细腻的动画效果，增加了游戏的趣味性和沉浸感。

## 技术栈 (Tech Stack)
- **Reflex**: 作为游戏的核心引擎，提供高效的客户端与服务器通讯。
- **Colyseus**: 实现跨平台的多人在线对战，提供可靠的实时同步。
- **TypeORM**: 用于数据库存储，确保玩家数据的持久性与安全性。

## 安装与运行 (Installation & Setup)

1. 克隆项目到本地:
   ```bash
   git clone https://github.com/youru/SnakeBattle.git
   ```

2. 进入项目server目录
   ```
   cd Server/SnakeWarsServer
   ```
3. 运行安装依赖
  ```
  npm install
  ```
4. 启动本地服务器
  ```
  npm start
  ```

 ## 运行服务端
  使用Unity打开Client文件夹，便可以直接运行

## 更改ip和接口
  位于 Client/Assets/Resources/StaticData下的Colyseus Settings （使用Unity打开），修改其中的 Server Information 即可
