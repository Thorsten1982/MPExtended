﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPExtended.Services.MediaAccessService.Interfaces.TVShow
{
    public class WebTVShowDetailed : WebTVShowBasic
    {
        public WebTVShowDetailed() : base()
        {
            Actors = new List<string>();
        }

        public IList<string> Actors { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public string Network { get; set; }
        public string AirsDay { get; set; }
        public string AirsTime { get; set; }
        public int Runtime { get; set; }
    }
}
