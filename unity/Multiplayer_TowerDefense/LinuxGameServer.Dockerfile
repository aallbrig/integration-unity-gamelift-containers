FROM amazonlinux:2023

COPY Builds/GameServer /GameServer

CMD ["/GameServer/multiplayer_tower_defense.x86_64"]