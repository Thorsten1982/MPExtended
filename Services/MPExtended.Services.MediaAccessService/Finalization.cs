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
using MPExtended.Services.MediaAccessService.Interfaces;

namespace MPExtended.Services.MediaAccessService
{
    internal static class Finalization
    {
        public static List<T> ForList<T>(IEnumerable<T> list, int? provider, ProviderType providerType) where T : WebObject
        {
            // special-case LazyQuery here again: execute the query just once instead of a lot of times
            var operList = list.ToList();

            if (operList.Count() == 0)
                return new List<T>();

            int realProvider = ProviderHandler.GetProviderId(providerType, provider);
            bool isArtwork = operList.First() is IArtwork;

            List<T> retlist = new List<T>();
            foreach (T item in operList)
            {
                item.PID = realProvider;
                if (isArtwork)
                {
                    (item as IArtwork).Artwork = (item as IArtwork).Artwork.Select(x => new WebArtwork()
                    {
                        Offset = x.Offset,
                        Type = x.Type,
                        Filetype = x.Filetype,
                        Id = x.Id,
                        Rating = x.Rating
                    }).ToList();
                }
                retlist.Add(item);
            }

            return retlist;
        }

        public static List<T> ForList<T>(IEnumerable<T> list, int? provider, WebMediaType providerType) where T : WebObject
        {
            return ForList(list, provider, providerType.ToProviderType());
        }

        public static T ForItem<T>(T item, int? provider, ProviderType providerType) where T : WebObject
        {
            item.PID = ProviderHandler.GetProviderId(providerType, provider);
            if (item is IArtwork)
            {
                (item as IArtwork).Artwork = (item as IArtwork).Artwork.Select(x => new WebArtwork()
                {
                    Offset = x.Offset,
                    Type = x.Type,
                    Filetype = x.Filetype,
                    Id = x.Id,
                    Rating = x.Rating
                }).ToList();
            }

            return item;
        }

        public static T ForItem<T>(T item, int? provider, WebMediaType providerType) where T : WebObject
        {
            return ForItem(item, provider, providerType.ToProviderType());
        }
    }
}
