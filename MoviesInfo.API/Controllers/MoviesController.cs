using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoviesInfo.API.Controllers
{
    [ApiController]
    [Route("api/movies")]

    public class MoviesController:ControllerBase
    {
        private readonly IConfiguration _config;

      

        private readonly IHttpClientFactory _clientFactory;
        public MoviesController(IHttpClientFactory httpClientFactory,IConfiguration config)
        {
            _clientFactory = httpClientFactory;
            _config = config;
        }

        [HttpGet("{Title}")]
        public async Task<ActionResult<Movie>> Series(string title)
        {
            try
            {
                var apiKey = _config.GetValue<string>("apiKey");
                var t = title;

                var httpClient = _clientFactory.CreateClient();
                string omdbApiUrl = $"http://www.omdbapi.com?apikey={apiKey}&t={t}";
                var responseString = await httpClient.GetStringAsync(omdbApiUrl);
                var result = JsonConvert.DeserializeObject<Movie>(responseString);
                if (result.Response == "False")
                {
                    throw new Exception();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
            

        }


        [HttpGet("{Title}/{season}")]
        public async Task<ActionResult<Ep[]>> SeasonInfo(string title,int season)
        {
            try
            {
                var t = title;
                var s = season;
                var apiKey = _config.GetValue<string>("apiKey");
                var httpClient = _clientFactory.CreateClient();
                string omdbApiUrl = $"http://www.omdbapi.com?apikey={apiKey}&t={t}&season={s}";
                var responseString = await httpClient.GetStringAsync(omdbApiUrl);
                var result = JsonConvert.DeserializeObject<SeasonInfo>(responseString);
                if(result.Response == "False")
                {
                    throw new Exception();
                }
                return Ok(result.Episodes);
            }
            catch (Exception)
            {

                return NotFound();
            }
        
        }
    }
}
