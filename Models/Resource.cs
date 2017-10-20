using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MSSqlRestApi.Models
{
    public abstract class Resource
    {
        [JsonProperty(Order = -4)]
        public Dictionary<string, Uri> links;

        public Resource()
        {
            this.links = new Dictionary<string, Uri>();
        }

        public abstract void UpdateLinks(IUrlHelper urlHelper);
    }
}