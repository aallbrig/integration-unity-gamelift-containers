FROM amazonlinux:2023
RUN yum install -y nc
COPY Builds/GameServer/Debug /GameServer
RUN chmod +x "/GameServer/multiplayer_tower_defense.x86_64"
ENTRYPOINT ["/GameServer/multiplayer_tower_defense.x86_64"]
