# Nvidia auto pstate tool

## Features

- **Automatic GPU Power State Management**: Automatically switches between performance and power-saving modes based on GPU utilization
  - Switches to high performance when the (throttled) GPU's usage exceeds 90% for more than 3 consecutive seconds
  - Switches to power-saving mode when the (full power) GPU's usage drops below 10% for more than 5 consecutive seconds

- **Manual Control**: Manual buttons to force GPU into slow (power-saving) or fast (performance) modes

- **Toggle Automatic Mode**: Checkbox to enable/disable automatic switching while keeping manual controls available

- **Force enable (optional)**: Forces power-saving mode when the current active window contains a specific substring

- **Multi-GPU Support**: Dropdown selection to choose which GPU to manage in multi-GPU systems

- **Safe Shutdown**: Automatically restores GPU to normal performance state when application closes

## How to use

The exe can be found at /limit-nvpstate/bin/x64/Debug/
