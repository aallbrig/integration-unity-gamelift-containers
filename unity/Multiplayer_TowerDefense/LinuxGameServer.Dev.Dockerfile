FROM amazonlinux:2023
COPY Builds/GameServer/Debug /GameServer
RUN chmod +x "/GameServer/multiplayer_tower_defense.x86_64"
ENTRYPOINT ["/GameServer/multiplayer_tower_defense.x86_64"]
