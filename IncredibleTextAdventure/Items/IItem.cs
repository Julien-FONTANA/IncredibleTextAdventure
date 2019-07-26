﻿using IncredibleTextAdventure.Service.Context;

namespace IncredibleTextAdventure.Items
{
    public interface IItem
    {
        string Description { get; set; }
        string Name { get; set; }
        bool CanBePickedUp { get; set; }

        bool CanInteractWith(string other);
        void InteractWith(IGameContext context);
    }
}