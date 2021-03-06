﻿#region Copyright (C) 2011 MPExtended
// Copyright (C) 2011 MPExtended Developers, http://mpextended.github.com/
// 
// MPExtended is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MPExtended is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MPExtended. If not, see <http://www.gnu.org/licenses/>.
#endregion

using System;
using System.ServiceModel;
using MPExtended.Services.MediaAccessService.Interfaces;
using MPExtended.Services.StreamingService.Interfaces;
using MPExtended.Services.TVAccessService.Interfaces;

namespace MPExtended.Libraries.General
{
    public static class MPEServices
    {
        private static IMediaAccessService MASConnection;
        private static IWebStreamingService WSSForMAS;
        private static IStreamingService StreamForMAS;
        private static ITVAccessService TASConnection;
        private static IWebStreamingService WSSForTAS;
        private static IStreamingService StreamForTAS;

        public static IMediaAccessService MAS
        {
            get
            {
                if (MASConnection == null || ((ICommunicationObject)MASConnection).State == CommunicationState.Faulted)
                {
                    MASConnection = CreateConnection<IMediaAccessService>(Configuration.Services.MASConnection, "MediaAccessService");
                }

                return MASConnection;
            }
        }

        public static bool HasMASConnection
        {
            get
            {
                try
                {
                    MAS.GetServiceDescription();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static IWebStreamingService MASStreamControl
        {
            get
            {
                if (WSSForMAS == null || ((ICommunicationObject)WSSForMAS).State == CommunicationState.Faulted)
                {
                    WSSForMAS = CreateConnection<IWebStreamingService>(Configuration.Services.MASConnection, "StreamingService");
                }

                return WSSForMAS;
            }
        }

        public static IStreamingService MASStream
        {
            get
            {
                if (StreamForMAS == null || ((ICommunicationObject)StreamForMAS).State == CommunicationState.Faulted)
                {
                    StreamForMAS = CreateConnection<IStreamingService>(Configuration.Services.MASConnection, "StreamingService");
                }

                return StreamForMAS;
            }
        }

        public static bool HasMASStreamConnection
        {
            get
            {
                try
                {
                    MASStreamControl.GetServiceDescription();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static ITVAccessService TAS
        {
            get
            {
                if (TASConnection == null || ((ICommunicationObject)TASConnection).State == CommunicationState.Faulted)
                {
                    TASConnection = CreateConnection<ITVAccessService>(Configuration.Services.TASConnection, "TVAccessService");
                }

                return TASConnection;
            }
        }

        public static bool HasTASConnection
        {
            get
            {
                try
                {
                    return TAS.TestConnectionToTVService();
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static IWebStreamingService TASStreamControl
        {
            get
            {
                if (WSSForTAS == null || ((ICommunicationObject)WSSForTAS).State == CommunicationState.Faulted)
                {
                    WSSForTAS = CreateConnection<IWebStreamingService>(Configuration.Services.TASConnection, "StreamingService");
                }

                return WSSForTAS;
            }
        }

        public static IStreamingService TASStream
        {
            get
            {
                if (StreamForTAS == null || ((ICommunicationObject)StreamForTAS).State == CommunicationState.Faulted)
                {
                    StreamForTAS = CreateConnection<IStreamingService>(Configuration.Services.TASConnection, "StreamingService");
                }

                return StreamForTAS;
            }
        }

        public static bool HasTASStreamConnection
        {
            get
            {
                try
                {
                    TASStreamControl.GetServiceDescription();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private static T CreateConnection<T>(string address, string service)
        {
            Uri addr = new Uri(address);

            // This allows us to introduce for example mas04://127.0.0.1/ in the future to allow communication with an older version of the services. 
            if (addr.Scheme != "auto")
            {
                Log.Error("Encountered unknown protocol while setting up {0} connection to {1}", service, address);
                return default(T);
            }

            if (addr.IsLoopback)
            {
                return ChannelFactory<T>.CreateChannel(
                    new NetNamedPipeBinding() { MaxReceivedMessageSize = 100000000 },
                    new EndpointAddress(String.Format("net.pipe://127.0.0.1/MPExtended/{0}", service))
                );
            }
            else
            {
                return ChannelFactory<T>.CreateChannel(
                    new BasicHttpBinding() { MaxReceivedMessageSize = 100000000 },
                    new EndpointAddress(String.Format("http://{0}:{1}/MPExtended/{2}", addr.Host, addr.Port, service))
                );
            }
        }
    }
}