﻿using Microsoft.Xna.Framework;
using TrueCraft.Client.Input;

namespace TrueCraft.Client.Modules
{
    public interface IInputModule : IGameplayModule
    {
        bool KeyDown(GameTime gameTime, KeyboardKeyEventArgs e);
        bool KeyUp(GameTime gameTime, KeyboardKeyEventArgs e);
        bool MouseMove(GameTime gameTime, MouseMoveEventArgs e);
        bool MouseButtonDown(GameTime gameTime, MouseButtonEventArgs e);
        bool MouseButtonUp(GameTime gameTime, MouseButtonEventArgs e);
        bool MouseScroll(GameTime gameTime, MouseScrollEventArgs e);
        bool GamePadButtonDown(GameTime gameTime, GamePadButtonEventArgs e);
        bool GamePadButtonUp(GameTime gameTime, GamePadButtonEventArgs e);
    }
}
