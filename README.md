# Integration: Unity GameLift Containers
Goal: provide proof of a multiplayer game server (unity) running inside a container integrating with aws gamelift.

## Resources
- AWS Skillbuilder Course: Using GameLift FleetIQ for Game Servers

## Development
### Useful command snippets
```bash
# build and run the game server container
dockerfile=$(git rev-parse --show-toplevel)/unity/Multiplayer_TowerDefense/LinuxGameServer.Dev.Dockerfile
build_context=$(git rev-parse --show-toplevel)/unity/Multiplayer_TowerDefense
docker run \
  --net=host \
  --platform linux/amd64 \
  --interactive \
  --tty \
  --rm \
  $(docker buildx build \
    --platform linux/amd64 \
    --quiet \
    --file $dockerfile \
    $build_context)
```

### M2 Macbook Pro Observations
- when running `docker` commands, it's important to add `--platform linux/amd64`. If this does not happen for both the `docker run` and `docker buildx build` commands the container will not run the unity game server properly, complaining about qemu issues.
- When on macbook, must enable a beta feature to get `--net=host` option working. I feel blessed because they just enabled this feature in Docker Desktop for mac. Isn't that lucky?