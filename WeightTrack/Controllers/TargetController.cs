using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
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
    public class TargetController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<WeightEntriesController> _logger;

        public TargetController(ILogger<WeightEntriesController> logger, AppDbContext context)
        {
            _logger = logger;
            _appDbContext = context;
        }

        /// <summary>
        /// Adds a new weight target
        /// </summary>
        /// <returns>A simple response with the ID of the newly created item</returns>
        [HttpPost]
        [Route("api/[controller]")]
        [ProducesResponseType(typeof(AddResponse), 200)]
        public async Task<ActionResult<AddResponse>> AddTarget(AddItemRequest request)
        {
            try
            {
                
                var newEntry = new WeightTarget()
                {
                   Active = 1,          //for now everything will be active
                   Note = request.Note,
                   TargetDate = (DateTime)request.Date,
                   TargetWeight = (double)request.Weight
                };

                _appDbContext.WeightTargets.Add(newEntry);
                await _appDbContext.SaveChangesAsync();


                return Ok(new AddResponse()
                {
                    NewEntrytId = newEntry.Id
                });

            }

            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); //likely not a good idea in an actual prod system, but will do for now
            }
        }

        /// <summary>
        /// Returns a list of existing weight targets. Can be filtered by target date
        /// </summary>
        /// <returns>A list of target objects</returns>
        [HttpGet]
        [Route("api/[controller]/GetAll")]
        [ProducesResponseType(typeof(List<WeightTarget>), 200)]
        public async Task<ActionResult<List<WeightTarget>>> GetAll(DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                var resultList = _appDbContext.WeightTargets.OrderBy(x => x.TargetDate);

                if (dateFrom is not null)
                {
                    resultList = (IOrderedQueryable<WeightTarget>)resultList.Where(x => x.TargetDate >= dateFrom);
                }

                if (dateTo is not null)
                {
                    resultList = (IOrderedQueryable<WeightTarget>)resultList.Where(x => x.TargetDate <= dateTo);
                }

                return Ok(await resultList.ToListAsync());
            }

            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Returns data for evaluating progress against a particular target
        /// </summary>
        /// <returns>An object containing current weight and target weight</returns>
        [HttpGet]
        [Route("api/[controller]/GetProgress/{targetId}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [SwaggerResponse(404, Description = "Not found")]
        [ProducesResponseType(typeof(ProgressResponse), 200)]
        public async Task<ActionResult<ProgressResponse>> GetProgress([Required]int targetId)
        {
            try
            {
                if (await _appDbContext.WeightTargets.Where(x => x.Id == targetId && x.Active == 1).AnyAsync())
                {
                    var latestEntry = await _appDbContext.WeightEntries.OrderByDescending(x => x.EntryDate).FirstOrDefaultAsync();
                    if (latestEntry is null)
                        return NotFound($"No weight entries currently");

                    double currentWeight = latestEntry.Weight;
                    var target = await _appDbContext.WeightTargets.Where(x => x.Id == targetId && x.Active == 1).FirstAsync();

                    return Ok(new ProgressResponse() { 
                        CurrentWeight = currentWeight, 
                        TargetWeigth = target.TargetWeight,
                        TargetId = target.Id,
                        TargetDate = target.TargetDate
                    });
                }

                else
                    return NotFound($"Target {targetId} not found");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
