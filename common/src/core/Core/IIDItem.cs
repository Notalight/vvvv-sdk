﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VVVV.Core.Model;

namespace VVVV.Core
{
    public enum RootingAction
    {
        Rooted,
        ToBeUnrooted
    }
    
    public class RootingChangedEventArgs : EventArgs
    {
        public RootingChangedEventArgs(RootingAction rooting)
        {
            Rooting = rooting;
        }
        
        public RootingAction Rooting
        {
            get;
            private set;
        }
    }
    
    public delegate void RootingChangedEventHandler(object sender, RootingChangedEventArgs args);
    
    public interface IIDItem : INamed
    {
        IIDContainer Owner
        {
            get;
            set;
        }

        IModelMapper Mapper
        {
            get;
        }

        /// <summary>
        /// RootingChanged event occurs when the IIDItem was either added to or is going
        /// to be removed from a rooted object graph.
        /// </summary>
        event RootingChangedEventHandler RootingChanged;
        
        /// <summary>
        /// Whether this IIDItem is rooted through its parent containers or not.
        /// </summary>
        bool IsRooted
        {
            get;
        }
    }

    public static class IDExtensions
    {
        public static string GetID(this IIDItem item)
        {
            if (item.Owner != null)
                return item.Owner.GetID() + "/" + item.Name;
            else
                return item.Name;
        }

        public static string GetIDRelativeTo(this IIDItem item, IIDItem other)
        {
            return item.GetID().Replace(other.GetID()+"/", "");
        }
    }
}
