using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignsController : ControllerBase
    {
        private readonly CampaignContext _context;

        public CampaignsController(CampaignContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campaign>>> GetAll()
        {
            return await _context.Campaigns.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign>> Get(int id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
                return NotFound();
            return campaign;
        }

        [HttpPost]
        public async Task<ActionResult<Campaign>> Create(Campaign campaign)
        {
            _context.Campaigns.Add(campaign);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = campaign.Id }, campaign);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Campaign campaign)
        {
            if (id != campaign.Id)
                return BadRequest();

            _context.Entry(campaign).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
                return NotFound();

            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
