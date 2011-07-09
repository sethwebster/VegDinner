using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenSearchToolkit;
using NerdDinner.Models;

namespace NerdDinner
{
    /// <summary>
    /// Summary description for OpenSearch
    /// </summary>
    public class OpenSearch : OpenSearchHandler
    {
        public override Description Description
        {
            get
            {
                return new Description
                {
                    DisplayName = "VegDinner.com",
                    LongDescription = "VegDinner - Organizing the world's vegans and helping them eat in packs",
                    SearchPathTemplate = "/Dinners?q={0}",
                    IconPath = "~/favicon.ico"
                };
            }
        }

        public override IEnumerable<SearchResult> GetResults(string q)
        {
            var dinners = new DinnerRepository().FindDinnersByText(q).ToArray();

            return from dinner in dinners
                   select new
                       SearchResult
                       {
                           Description = dinner.Description,
                           Title = dinner.Title + " with " + dinner.HostedBy,
                           Path = "/" + dinner.DinnerID
                       };
        }

        public override IEnumerable<SearchSuggestion> GetSuggestions(string term)
        {
            var dinners = new DinnerRepository().FindDinnersByText(term).ToArray();

            return from dinner in dinners
                   select new
                       SearchSuggestion
                   {
                       Description = dinner.Description,
                       Term = dinner.Title + " with " + dinner.HostedBy,
                   };
        }

        public override bool SupportsSuggestions
        {
            get { return true; }
        }
    }
}