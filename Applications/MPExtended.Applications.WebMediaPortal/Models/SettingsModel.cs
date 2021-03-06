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
using System.Web;

namespace MPExtended.Applications.WebMediaPortal.Models
{
    public class SettingModel
    {
        public int DefaultGroup { get; set; }
        public string TranscodingProfile { get; set; }
        public bool UseLocalServices { get; set; }
        public string RemoteTVServiceURL { get; set; }
        public string RemoteMediaServiceURL { get; set; }

        public int TVShowProvider { get; set; }
        public int MovieProvider { get; set; }
        public int MusicProvider { get; set; }
        public int PicturesProvider { get; set; }
        public int FileSystemProvider { get; set; }
    }
}