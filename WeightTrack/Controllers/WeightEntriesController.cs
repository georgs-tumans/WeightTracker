using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WeightTrack.Data.DbModels;
using WeightTrack.Data.Requests;
using WeightTrack.Data.Responses;

namespace WeightTrack.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [SwaggerResponse(500, Description = "Internal server error")]
    [SwaggerResponse(200, Description = "Ok")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(byte[]), (int)HttpStatusCode.OK)]
    public class WeightEntriesController : ControllerBase
    {
        
        private readonly ILogger<WeightEntriesController> _logger;
        private readonly AppDbContext _appDbContext;

        public WeightEntriesController(ILogger<WeightEntriesController> logger, AppDbContext context)
        {
            _logger = logger;
            _appDbContext = context;
        }

        /// <summary>
        /// Adds a new weight entry
        /// </summary>
        /// <returns>A simple response with the ID of the newly created item</returns>
        [HttpPost]
        [Route("api/[controller]")]
        [ProducesResponseType(typeof(AddResponse), 200)]
        public async Task<ActionResult<AddResponse>> AddWeight(AddItemRequest request)
        {
            try
            {
                
                var newEntry = new WeightEntry()
                {
                    Weight = (double)request.Weight,    //wont be null because of automatic incoming request object checks
                    Note = request.Note,
                    EntryDate = (DateTime)request.Date
                };
                
                _appDbContext.WeightEntries.Add(newEntry);
                await _appDbContext.SaveChangesAsync();

                return Ok(new AddResponse()
                {
                    NewEntrytId = newEntry.Id
                });

            }
           
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of existing weight entries. Can be filtered by entry date
        /// </summary>
        /// <returns>A list of weight objects</returns>
        [HttpGet]
        [Route("api/[controller]")]
        [ProducesResponseType(typeof(List<WeightEntry>), 200)]
        public async Task<ActionResult<List<WeightEntry>>> Get(DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
     
                var resultList = _appDbContext.WeightEntries.OrderBy(x => x.EntryDate);

                if (dateFrom is not null)
                {
                    resultList = (IOrderedQueryable<WeightEntry>)resultList.Where(x => x.EntryDate >= dateFrom);
                }

                if (dateTo is not null)
                {
                    resultList = (IOrderedQueryable<WeightEntry>)resultList.Where(x => x.EntryDate <= dateTo);
                }

                return Ok(await resultList.ToListAsync());
            }

            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}