#!/usr/bin/env bash
source $(git rev-parse --show-toplevel)/scripts/lib/assertions.sh
function can_build_linux_game_server_using_game_ci_image() {
  project_editor_version=$1
  gameci_tag="ubuntu-${project_editor_version}-linux-il2cpp-3"
  cmd_output=$(docker pull --platform linux/amd64 unityci/editor:$gameci_tag | tee >(cat))
  if ! [ $? -eq 0 ]; then
    fail "game-ci image for editor version $project_editor_version is not available\n  Command Output:\n$cmd_output"
  fi
  pass "game-ci image for editor version $project_editor_version is available"
  echo "can_build_linux_game_server_using_game_ci_image is not implemented yet."
}
function main() {
  version_file=$(find $(git rev-parse --show-toplevel) -name ProjectVersion.txt)
  project_editor_version=$(grep 'm_EditorVersion:' $version_file | awk '{print $2}')
  can_build_linux_game_server_using_game_ci_image $project_editor_version
}
main