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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MPExtended.Libraries.General
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ServicesConfiguration
    {
        public bool BonjourEnabled { get; set; }
        public string BonjourName { get; set; }

        public int Port { get; set; }
        public bool EnableIPv6 { get; set; }

        public string MASConnection { get; set; }
        public string TASConnection { get; set; }

        public List<User> Users { get; set; }

        public ServicesConfiguration()
        {
            XElement file = XElement.Load(Configuration.GetPath("Services.xml"));

            BonjourEnabled = file.Element("bonjour").Element("enabled").Value == "true";
            BonjourName = file.Element("bonjour").Element("pcname").Value;

            Port = Int32.Parse(file.Element("port").Value);
            EnableIPv6 = file.Element("enableIPv6").Value == "true";

            MASConnection = file.Element("connections").Element("mas").Value;
            TASConnection = file.Element("connections").Element("tas").Value;

            Users = file.Element("users").Elements("user").Select(x => new User()
            {
                Username = x.Element("username").Value,
                Password = x.Element("password").Value
            }).ToList();
        }

        public bool Save() 
        {
            try
            {
                XElement file = XElement.Load(Configuration.GetPath("Services.xml"));

                file.Element("bonjour").Element("enabled").Value = BonjourEnabled ? "true" : "false";
                file.Element("bonjour").Element("pcname").Value = BonjourName;

                file.Element("port").Value = Port.ToString();
                file.Element("enableIPv6").Value = EnableIPv6 ? "true" : "false";

                file.Element("connections").Element("mas").Value = MASConnection;
                file.Element("connections").Element("tas").Value = TASConnection;

                file.Element("users").Elements().Remove();
                foreach(User u in Users) {
                    var userElement = new XElement("user", new XElement("username", u.Username), new XElement("password", u.Password));
                    file.Element("users").Add(userElement);
                }

                file.Save(Configuration.GetPath("Services.xml"));
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Failed to write Services.xml", ex);
                return false;
            }
        }
    }
}
