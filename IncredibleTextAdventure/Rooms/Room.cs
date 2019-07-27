﻿using IncredibleTextAdventure.Directives;
using IncredibleTextAdventure.Items;
using System.Collections.Generic;

namespace IncredibleTextAdventure.Rooms
{
    public abstract class Room : IRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAccessible { get; set; }
        public string FirstDescription { get; set; }

        protected bool IsFirstTime { get; set; }
        protected List<string> LinkedRooms { get; set; }
        protected List<IItem> ItemsInRoom { get; set; }
        protected IDirective[] SpecialDirectives { get; set; }

        protected Room()
        {
            IsAccessible = false;
            IsFirstTime = true; ;
            LinkedRooms = new List<string>();
            ItemsInRoom = new List<IItem>();
            SpecialDirectives = new IDirective[] {};
        }

        public List<string> GetLinkedRooms()
        {
            return LinkedRooms;
        }

        public bool IsFirstTimePlayerEntersRoom()
        {
            if (IsFirstTime)
            {
                SetFirstTimeFalse();
                return true;
            }
            return false;
        }

        public void SetFirstTimeFalse()
        {
            IsFirstTime = false;
        }

        public List<IItem> GetItemsInRoom()
        {
            return ItemsInRoom;
        }

        public IDirective[] GetSpecialDirectives()
        {
            return SpecialDirectives;
        }

        public void RemoveItemFromRoom(IItem itemToRemove)
        {
            ItemsInRoom.Remove(itemToRemove);
        }
    }
}
