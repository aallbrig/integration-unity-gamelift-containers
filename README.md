# Integration: Unity GameLift Containers
Goal: provide proof of a multiplayer game server (unity) running inside a container integrating with aws gamelift.

## Resources
- AWS Skillbuiler Course: Using GameLift FleetIQ for Game Servers

## Development
### Useful command snippets
```bash
# build and run the game server container
docker run --platform linux/amd64 -it --rm $(docker buildx build -q --platform linux/amd64 -f $(git rev-parse --show-toplevel)/unity/Multiplayer_TowerDefense/LinuxGameServer.Dev.Dockerfile $(git rev-parse --show-toplevel)/unity/Multiplayer_TowerDefense)
```