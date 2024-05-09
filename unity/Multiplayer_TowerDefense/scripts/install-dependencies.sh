#!/usr/bin/env bash
function install_mirror() {
  tag="v89.7.0"
  mirror_repo_url="https://github.com/MirrorNetworking/Mirror.git"
  clone_path="/tmp/Mirror-${tag}"
  target_path="Assets/Mirror"
  if [ ! -d "$target_path" ]; then
    git clone --depth 1 --branch $tag $mirror_repo_url $clone_path
    mv $clone_path/Assets/Mirror $target_path
  fi
}
function install_gamelift_sdk() {
  gamelift_sdk_version="5.1.2"
  gamelift_sdk_url="https://gamelift-server-sdk-release.s3.us-west-2.amazonaws.com/unity/GameLift-CSharp-ServerSDK-UnityPlugin-${gamelift_sdk_version}.zip"
  download_path="/tmp/gameliftSDK-${tag}"
  target_path="../Packages"
  mkdir -p $download_path
  if [ ! -f "$download_path/$(basename $gamelift_sdk_url)" ]; then
    wget -O $download_path/$(basename $gamelift_sdk_url) $gamelift_sdk_url
    unzip $download_path/$(basename $gamelift_sdk_url) -d $download_path
  fi
  tgz_file=$(find $download_path -name "*.tgz")
  if [ -f "$target_path/$(basename $tgz_file)" ]; then
    mv $tgz_file $target_path
  fi
}
function main() {
  install_mirror
  install_gamelift_sdk
}
main "${@}"
