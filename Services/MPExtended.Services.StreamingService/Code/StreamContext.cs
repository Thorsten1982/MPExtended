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
using MPExtended.Libraries.General;
using MPExtended.Services.StreamingService.Interfaces;

namespace MPExtended.Services.StreamingService.Code
{
    internal class StreamContext
    {
        public MediaSource Source { get; set; }
        public WebMediaInfo MediaInfo { get; set; }
        public bool IsTv { get; set; }

        public Resolution OutputSize { get; set; }
        public TranscoderProfile Profile { get; set; }

        public Pipeline Pipeline { get; set; }

        public WebTranscodingInfo TranscodingInfo { get; set; }

        public int? AudioTrackId { get; set; }
        public int? SubtitleTrackId { get; set; }
    }
}
