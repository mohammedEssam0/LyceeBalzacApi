using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LyceeBalzacApi.data_models;


namespace LyceeBalzacApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Level1Controller : ControllerBase
    {
        static readonly ILevel1Services services = new Level1Services();
        [HttpGet]
        public IEnumerable<Level1> GetAllData()
        {
            return services.GetAll();
        }
        [Route("{id}")]
        [HttpGet]
        public Level1 GetRecord(int id)
        {
            Level1 record = services.Get(id);
            if(record == null)
            {
                // throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
                throw new BadHttpRequestException("Not Found");
            }
            return record;
        }
        [Route("ByName/{Name}")]
        public IEnumerable<Level1> GetRecordByName(string Name)
        {
            return services.GetAll().Where(x => string.Equals(x.EnglishName, Name, StringComparison.OrdinalIgnoreCase));
        }
        [HttpPost]
        //Create a new record
        public Level1 PostRecord([FromBody]Level1 record)
        {
            record = services.Add(record);
            return record;
        }
        [HttpDelete]
        [Route("{id}")]
        public void DeleteRecord(int id)
        {
            Level1 record = services.Get(id);
            if(record == null)
            {
                throw new BadHttpRequestException("Not found");
            }
            else
            {
                services.Remove(id);

            }
            
        }
        [Route("{id}")]
        [HttpPut]
        public void PutProduct(int id, Level1 record)
        {
            record.Id = id;
            if(!services.Update(record))
            {
                throw new BadHttpRequestException("can't update");
            }
        }
    }
}
