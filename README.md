# Nvidia auto pstate tool

## Features

- **Automatic GPU Power State Management**: Automatically switches between high-performance (P0) and power-saving (P5) mode based on GPU usage
  - Switches to high performance when the (throttled) GPU's usage exceeds 90% for more than 3 consecutive seconds
  - Switches to power-saving mode when the (full power) GPU's usage drops below 10% for more than 5 consecutive seconds

- **Manual Control**: Manual buttons to force GPU into power-saving or performance mode

- **Toggle Automatic Mode**: Checkbox to enable/disable automatic switching while keeping manual controls available

- **Force enable (optional)**: Forces power-saving mode when the current active window contains a specific substring

- **Multi-GPU Support**: Dropdown selection to choose which GPU to manage in multi-GPU systems

- **Safe Shutdown**: Automatically restores GPU to normal performance state when application closes

## Code location

https://github.com/timothelaborie/nvidia_auto_pstate/blob/main/limit-nvpstate/MainForm/MainForm.cs

## How to use

- Download the repo as a zip
- The exe can be found at /limit-nvpstate/bin/x64/Debug/
