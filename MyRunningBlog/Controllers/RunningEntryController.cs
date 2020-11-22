using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyRunningBlog.Data;
using MyRunningBlog.Models;

namespace MyRunningBlog.Controllers
{
    [Route("api/[controller]")]
    public class RunningEntryController : Controller
    {
        private readonly IRunningRepository _runningRepository;
        private readonly IMapper _mapper;

        public RunningEntryController(IRunningRepository runningRepository, IMapper mapper)
        {
            _runningRepository = runningRepository;
            _mapper = mapper;
        }


        [HttpGet("[action]")]
        public async Task<IEnumerable<RunningEntry>> GetRuningEntries()
        {
            var activitySummaries = await _runningRepository.GetRunningEntries();

            var result = _mapper.Map<List<RunningEntry>>(activitySummaries);

            return result;
        }
    }
}