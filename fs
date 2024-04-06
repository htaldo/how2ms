#!/bin/bash

#fs - find snippet
#usage: ./fs . string
dir=$1; pattern=$2
cd $dir
raw="$(find | sed 's/ /\\ /g' | grep -i -d skip -r "$pattern" | fzf)"
[ -n "$raw" ] || exit
file="$(cut -d : -f 1 <<< "$raw")"
query="$(sed -e 's/[^:]\+://' -e 's/^[[:space:]]*//' <<< "$raw")"
#escape special characters so nvim can parse the query in command mode
#clean="$(sed 's/~\|\/\|\*\`\|\^\|\$\|\.\|\[\|\]\|\+\|\(\|\)\|\\/\\&/g' <<< $query)"
clean="$(sed 's/~\|\/\|\*/\\&/g' <<< $query)"
nvim -c /"$clean" $file
