Emotional-Adaptive Games

Type: Research / Interactive Unity Games
Unity Version: 6.2 (6000.2.6f2)
Theme: Visual Computing – Emotion-Recognizing / Affective Gaming

This Unity project explores emotion-adaptive gaming, where player emotions influence game mechanics in real-time. It includes two games: Tetris and 3D Pac-Man, integrated with a Python-based emotion recognition system via UDP communication.

Overview

Player emotions are captured through a camera and recognized via Python.

Emotional data is sent to Unity using UDP.

Gameplay adapts dynamically based on the detected emotion:

Tetris: block falling speed changes

Pac-Man: ghost speed and chasing behavior changes

This project demonstrates how affective computing can be used to create interactive, emotion-driven gameplay.

Games & Features
Tetris

Blocks fall at variable speed depending on player emotions.

Controls:

Rotate Block: R

Move Left/Right: Left / Right Arrow

Drop Block Faster: Down Arrow

Notes:

No block storage available.

Game difficulty adapts in real-time.

3D Pac-Man

Ghosts’ speed and behavior change based on player emotions.

Ghosts randomly switch between chasing the player and wandering.

Features:

2 portals connecting different areas

No power-pellets implemented

Controls:

Move Player: WASD

Camera Control: Mouse

Pause Game: P

Setup Instructions

Open Unity Hub and load the project folder Emotional-Adaptive Games.

Run the Python emotion recognition script to start the camera and UDP server.

Open the desired Unity scene:

TetrisScene

PacMan3DScene

Play the game. Unity will receive real-time emotion data and adjust gameplay mechanics accordingly.

How It Works

Python script detects player emotions via the camera.

Emotion data is sent to Unity via UDP.

Unity scripts adjust gameplay parameters:

Tetris: block speed changes

Pac-Man: ghost speed and chasing behavior changes


| Game    | Action            | Key / Control  |
| ------- | ----------------- | -------------- |
| Tetris  | Rotate Block      | `R`            |
| Tetris  | Move Left/Right   | `Left / Right` |
| Tetris  | Drop Block Faster | `Down`         |
| Pac-Man | Move Player       | `WASD`         |
| Pac-Man | Camera Control    | `Mouse`        |
| Pac-Man | Pause Game        | `P`            |


Credits

Unity Development: [P310 - Emilie & Liza - Emotional-adaptive Games]

Research: Affective Computing & Visual Computing
