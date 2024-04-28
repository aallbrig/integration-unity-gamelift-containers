#!/usr/bin/env bash


function assert_container_image_exists() {
  image_definition=$1
  if [ -f $image_definition ]; then
    echo "✅ container definition file exists"
  else
    echo "❌ container definition file does not exist. $image_definition"
    exit 1
  fi
}
function assert_container_image_buildable() {
  image_definition=$1
  build_context=$2
  if [ docker build -f $image_definition $build_context]; then
    echo "✅ container definition file is buildable"
  else
    echo "❌ cannot definition file is not buildable: $image_definition $build_context"
    exit 1
  fi
}

function main() {
  container_image_definition_file="$(git rev-parse --show-toplevel)/unity/Multiplayer_Tower_Defense/Dockerfile"
  container_image_build_context="$(git rev-parse --show-toplevel)/unity/Multiplayer_Tower_Defense"
  assert_container_image_exists $container_image_definition_file
  assert_container_image_buildable $container_image_definition_file $container_image_build_context
}
main