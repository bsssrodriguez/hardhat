echo off

set cmd=%1
set dir=%2

if defined dir (
    cd /d %dir%\
)
start cmd /K %cmd%
cls