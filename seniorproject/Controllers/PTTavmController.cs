﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace seniorproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PTTavmController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var url = "https://www.pttavm.com/arama?q=cep%20telefon";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            IList<HtmlNode> nodes = doc.QuerySelectorAll("div.flex.flex-row.flex-wrap div.product-list-box-container.transition-all.duration-250");

            var data = nodes.Select((node) =>
            {


                return new
                {
                     name = node.QuerySelector("a")
                    .GetAttributeValue("title", null),
                     price = node.QuerySelector("div.price-box-price ").InnerText,
                    productUrl = "https://www.pttavm.com" + node.QuerySelector("a")
                    .GetAttributeValue("href", null)
                   
                };
            });


            return Ok(data);
        }
    }
}

