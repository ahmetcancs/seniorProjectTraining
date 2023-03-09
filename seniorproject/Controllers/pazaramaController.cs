using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace seniorproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class pazaramaController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var url = "https://www.pazarama.com/arama?q=cep%20telefonu";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            IList<HtmlNode> nodes = doc.QuerySelectorAll("div.product-card.bg-white.relative");
            var data = nodes.Select((node) =>
            {
              
                return new
                {
                   name = node.QuerySelector("a")
                    .GetAttributeValue("title",null),
                    productUrl = "https://www.pazarama.com" +  node.QuerySelector("a")
                    .GetAttributeValue("href",null),
                    imageUrl = node.QuerySelector("picture img")
                    .GetAttributeValue("src", null),
                   price = node.QuerySelector("div.text-gray-600.leading-tight.font-semibold.text-huge").InnerText

                };
            });


            return Ok(data);
        }
    }
}

