﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPExtended.Services.MediaAccessService.Interfaces.Movie
{
    public class WebMovieBasic : WebMediaItem, ITitleSortable, IYearSortable, IGenreSortable, IRatingSortable, ICategorySortable, IArtwork
    {
        public WebMovieBasic()
        {
            Genres = new List<string>();
            UserDefinedCategories = new List<string>();
            Artwork = new List<WebArtwork>();
        }

        public bool IsProtected { get; set; }
        public IList<string> Genres { get; set; }
        public IList<string> UserDefinedCategories { get; set; }
        public IList<WebArtwork> Artwork { get; set; }

        public string Title { get; set; }
        public int Year { get; set; }
        public float Rating { get; set; }
        public int Runtime { get; set; }
        public string IMDBId { get; set; }
        public string TMDBId { get; set; }

        public override WebMediaType Type 
        {
            get
            {
                return WebMediaType.Movie;
            }
        }

        public override string ToString()
        {
            return Title;
        }
    }
}