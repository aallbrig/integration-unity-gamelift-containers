#!/usr/bin/env bash

function pass() {
  printf "✅ $1\n"
}
function fail() {
  printf "❌ $1\n"
  exit 1
}
