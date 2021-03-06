﻿namespace BubblesClient.Network
{
    using System;
    using BubblesClient.Model;
    using Balloons.Messaging.Model;
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    /// <summary>
    /// INetworkManager is the interface for a component which communicates 
    /// with the Server and provides methods for notifying the server of state
    /// changes.
    /// </summary>
    public interface INetworkManager : IDisposable
    {
        /// <summary>
        /// Connects the Network Manager to the Server.
        /// </summary>
        void Connect();

        /// <summary>
        /// Notifies the Server that a balloon has moved off screen. The 
        /// implementation is responsible for ensuring that this event is not 
        /// called twice for the same balloon.
        /// </summary>
        /// <param name="balloon">The balloon to move</param>
        /// <param name="direction">The side of the screen the balloon is 
        /// exiting via</param>
        /// <param name="exitHeight">The normalised position on the screen the 
        /// balloon was at when it left the screen</param>
        /// <param name="velocity">The velocity of the balloon when it left the
        /// screen</param>
        void MoveBalloonOffscreen(ClientBalloon balloon, Direction direction, float exitHeight, Vector2 velocity);

        /// <summary>
        /// Notifies the Server that a plane has moved off screen. The 
        /// implementation is responsible for ensuring that this event is not 
        /// called twice for the same plane.
        /// </summary>
        /// <param name="plane">The plane to move</param>
        /// <param name="direction">The side of the screen the plane is 
        /// exiting via</param>
        /// <param name="exitHeight">The normalised position on the screen the 
        /// plane was at when it left the screen</param>
        /// <param name="velocity">The velocity of the plane when it left the
        /// screen</param>
        /// <param name="time"> Current time for the plane's animation. </param>
        void MovePlaneOffscreen(ClientPlane plane, Direction direction, float exitHeight, Vector2 velocity, float time);

        /// <summary>
        /// Notifies the Server that a balloon has been popped by a user.
        /// </summary>
        /// <param name="balloon"></param>
        void NotifyBalloonPopped(ClientBalloon balloon);

        /// <summary>
        /// Requests the balloon's content from the server.
        /// </summary>
        /// <param name="balloonID"></param>
        void RequestBalloonContent(string balloonID);

        /// <summary>
        /// Requests the balloon's state from the server.
        /// </summary>
        /// <param name="balloonID"></param>
        void RequestBalloonState(string balloonID);

        /// <summary>
        /// Notifies the Server that the state of a balloon (usually decoration) has been changed by a user.
        /// </summary>
        /// <param name="balloon"></param>
        void UpdateBalloonState(Balloon balloon);

        /// <summary>
        /// Retrieves all the messages that the Network Manager has received
        /// from the Server since the last call to this function.
        /// </summary>
        /// <param name="handlers"> Message handlers. </param>
        void ProcessMessages(Dictionary<MessageType, Action<Message>> handlers);
    }
}
