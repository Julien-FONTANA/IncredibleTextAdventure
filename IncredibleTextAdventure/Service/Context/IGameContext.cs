﻿using IncredibleTextAdventure.Characters;
using IncredibleTextAdventure.Rooms;

namespace IncredibleTextAdventure.Service.Context
{
    public interface IGameContext
    {
        // TODO - make this private ?
        IPlayer Player { get; set; }

        void Command(string cmd);
        IRoom GetCurrentRoom();
        IRoom GetRoom(string room);
    }
}