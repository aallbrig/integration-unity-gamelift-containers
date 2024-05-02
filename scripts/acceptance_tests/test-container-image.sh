#!/usr/bin/env bash

function pass() {
  printf "✅ $1\n"
}
function fail() {
  printf "❌ $1\n"
  exit 1
}
function __cleanup_running_test_containers(){
  tag_name=$1
  containers_with_label=$(docker ps -aq -f label=test_label)
  if ! [ -n "$containers_with_label" ]; then
    echo "No containers found with the specified test label test_label."
    return
  fi
  docker stop $containers_with_label 2>&1 > /dev/null
  docker rm $containers_with_label 2>&1 > /dev/null
}
function assert_container_image_exists() {
  image_definition=$1
  if [ -f $image_definition ]; then
    pass "container definition file '$(basename $image_definition)' exists"
  else
    fail "container definition file does not exist.\n  Details  \n  image definition file path: $image_definition"
  fi
}
function assert_container_image_build_context_is_directory() {
  build_context=$1
  if [ -d $build_context ]; then
    pass "container build context '$(basename $build_context)' is a directory"
  else
    fail "container build context is not a directory.\n  Details\n  build context directory path: $build_context"
  fi
}
function assert_container_image_buildable() {
  image_definition=$1
  build_context=$2
  test_image_tag=$3
  # reminder: the 2>&1 appended to the command is to redirect stream into variable so I control when to output
  cmd_output=$(docker buildx build --platform linux/amd64 --tag $test_image_tag --file $image_definition $build_context 2>&1)
  if [ $? -eq 0 ]; then
    pass "container definition file '$(basename $image_definition)' is confirmed buildable container image"
  else
    fail "container definition file is not buildable\n  Details\n  image definition: $image_definition\n  build context: $build_context\n  Command Output:\n$cmd_output"
  fi
}
function assert_container_image_runnable() {
  image_tag=$1
  container_id=$(docker run --platform linux/amd64 --detach --label test_label=container_runnable_$image_tag $image_tag 2>&1)
  run_timeout_seconds=2
  expected_log_detection=""
  for i in $(seq 0 0.25 $run_timeout_seconds); do
    expected_log_detection=$(docker ps --quiet --no-trunc --filter "id=$container_id")
    if [ $expected_log_detection == $container_id ]; then
      pass "container image with tag '$image_tag' is determined runnable"
      __cleanup_running_test_containers
      return
    fi
    sleep 0.25
  done
  fail "container image tag $image is determined not runnable\n  Details\n  run_timeout_seconds: $run_timeout_seconds\n  container_id: $container_id\n  Last Query Command Output:\n$expected_log_detection"
  __cleanup_running_test_containers
}
function assert_specific_log_in_running_container() {
  image_tag=$1
  expected_log=$2
  container_id=$(docker run --platform linux/amd64 --detach --label test_label=container_runnable_$image_tag $image_tag 2>&1)
  run_timeout_seconds=2
  expected_log_detection=""
  for i in $(seq 0 0.25 $run_timeout_seconds); do
    expected_log_detection=$(docker logs $container_id)
    if echo $expected_log_detection | grep -q "$expected_log"; then
      pass "container image with tag '$image_tag' contains expected log '$expected_log'"
      __cleanup_running_test_containers
      return
    fi
    sleep 0.25
  done
  fail "running container not determined to contain expected log '$expected_log'\n  Details\n  run_timeout_seconds: $run_timeout_seconds\n  container_id: $container_id\n  Last Query Command Output:\n$expected_log_detection"
  __cleanup_running_test_containers
}
function main() {
  game_server_dev_image_definition="$(git rev-parse --show-toplevel)/unity/Multiplayer_TowerDefense/LinuxGameServer.Dev.Dockerfile"
  game_server_prod_image_definition="$(git rev-parse --show-toplevel)/unity/Multiplayer_TowerDefense/LinuxGameServer.Prod.Dockerfile"
  game_server_build_context="$(git rev-parse --show-toplevel)/unity/Multiplayer_TowerDefense"
  game_server_tag="game-server-container-test"

  assert_container_image_exists \
    $game_server_dev_image_definition
  assert_container_image_exists \
    $game_server_prod_image_definition
  assert_container_image_build_context_is_directory \
    $game_server_build_context
  assert_container_image_buildable \
    $game_server_dev_image_definition \
    $game_server_build_context \
    "${game_server_tag}:dev"
  assert_container_image_buildable \
    $game_server_prod_image_definition \
    $game_server_build_context \
    "${game_server_tag}:prod"
  assert_container_image_runnable \
    "${game_server_tag}:dev"
  assert_container_image_runnable \
    "${game_server_tag}:prod"
  assert_specific_log_in_running_container \
    "${game_server_tag}:dev" \
    "boot.config"
  assert_specific_log_in_running_container \
    "${game_server_tag}:dev" \
    "Game Server is running"
}
main