using AutoMapper;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using ElectronicVotingSystem.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVotingSystem.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing parties in the election system
    /// </summary>
    [Authorize]
    [Route("api/{electionId}/parties")]
    [ApiController]
    [Produces("application/json")]
    public class PartiesController(IElectionRepository electionRepository, IPartyRepository partyRepository, IMapper mapper) : ControllerBase
    {
        private readonly IElectionRepository _electionRepository = electionRepository;
        private readonly IPartyRepository _partyRepository = partyRepository;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Get all parties
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Party>>> GetAllParties(Guid electionId)
        {
            if (!await _electionRepository.ExistsAsync(electionId)) return NotFound($"Election with ID {electionId} not found!");
            var parties = await _partyRepository.GetAllPartiesInElectionAsync(electionId);
            return Ok(parties);
        }

        /// <summary>
        /// Get details of a specific party by ID
        /// </summary>
        /// <param name="electionId">Election ID</param>
        /// <param name="partyId">Party ID</param>
        /// <returns></returns>
        [HttpGet("{partyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Party>> GetPartyById(Guid electionId, Guid partyId)
        {
            if (!await _electionRepository.ExistsAsync(electionId))
                return NotFound($"Election with ID {electionId} not found!");

            var party = await _partyRepository.GetPartyInElectionAsync(electionId, partyId);

            if (party == null)
                return NotFound($"Party with ID {partyId} not found!");

            return Ok(party);
        }

        /// <summary>
        /// Create a new party
        /// </summary>
        /// <param name="electionId">Election ID</param>
        /// <param name="partyDto">Party DTO</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateParty(Guid electionId, UpsertPartyDto partyDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var partyEntity = _mapper.Map<Party>(partyDto);

            await _electionRepository.AddPartyToAnElection(electionId, partyEntity);
            await _partyRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPartyById), new 
            { 
                electionId = partyEntity.ElectionId,
                partyId = partyEntity.Id 
            }, 
            partyEntity);
        }

        /// <summary>
        /// Update a party
        /// </summary>
        /// <param name="partyId">Election ID</param>
        /// <param name="electionId">Party DTO</param>
        /// <param name="partyDto">Party DTO</param>
        /// <returns></returns>
        [HttpPut("{partyId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> UpdateParty(Guid electionId, Guid partyId, UpsertPartyDto partyDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var partyEntity = await _partyRepository.GetPartyInElectionAsync(electionId, partyId);

            if (partyEntity == null) return NotFound($"Party with ID {partyId} not found!");

            _mapper.Map(partyDto, partyEntity);
            await _partyRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a specific party
        /// </summary>
        /// <param name="electionId">Party ID</param>
        /// <param name="partyId">Party ID</param>
        /// <returns></returns>
        [HttpDelete("{partyId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteParty(Guid electionId, Guid partyId)
        {
            if (!await _electionRepository.ExistsAsync(electionId))
            {
                return NotFound($"Election with ID {electionId} not found!");
            }

            var party = await _partyRepository.GetPartyInElectionAsync(electionId, partyId);

            if (party == null) return NotFound($"Party with ID {partyId} not found!");

            _partyRepository.DeleteParty(party);
            await _partyRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}