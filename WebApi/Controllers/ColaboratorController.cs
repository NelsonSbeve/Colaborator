using Microsoft.AspNetCore.Mvc;

using Application.Services;
using Application.DTO;
using Domain.Factory;
using RabbitMQ.Client;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboratorController : ControllerBase
    {   
        private readonly ColaboratorService _colaboratorService;
        private readonly ColaboratorPublisher _colaboratorPublisher;

        List<string> _errorMessages = new List<string>();

        public ColaboratorController(ColaboratorService colaboratorService, ColaboratorPublisher colaboratorPublisher)
        {
            _colaboratorService = colaboratorService;
            _colaboratorPublisher = colaboratorPublisher;
        }

        // GET: api/Colaborator
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColaboratorDTO>>> GetColaborators()
        {
            IEnumerable<ColaboratorDTO> colabsDTO = await _colaboratorService.GetAllWithAddress();

            return Ok(colabsDTO);
        }


        // GET: api/Colaborator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ColaboratorDTO>> GetColaboratorById(long id)
        {
            var colaboratorDTO = await _colaboratorService.GetByIdWithAddress(id);

            if (colaboratorDTO == null)
            {
                return NotFound();
            }

            return Ok(colaboratorDTO);
        }

        // GET: api/Colaborator/a@bc
        [HttpGet("email/{email}")]
        public async Task<ActionResult<ColaboratorDTO>> GetColaboratorByEmail(string email)
        {
            var colaboratorDTO= await _colaboratorService.GetByEmailWithAddress(email);

            if (colaboratorDTO == null)
            {
                return NotFound();
            }

            return Ok(colaboratorDTO);
        }


        // PUT: api/Colaborator/a@bc
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{email}")]
        public async Task<IActionResult> PutColaborator(string email, ColaboratorDTO colaboratorDTO)
        {
            if (email != colaboratorDTO.Email)
            {
                return BadRequest();
            }

            bool wasUpdated = await _colaboratorService.Update(email, colaboratorDTO, _errorMessages);

            if (!wasUpdated /* && _errorMessages.Any() */)
            {
                return BadRequest(_errorMessages);
            }

            return Ok();
        }

        // POST: api/Colaborator
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ColaboratorDTO>> PostColaborator(ColaboratorDTO colaboratorDTO)
        {
            ColaboratorDTO colaboratorResultDTO = await _colaboratorService.Add(colaboratorDTO, _errorMessages);

            if(colaboratorResultDTO != null){
                
                _colaboratorPublisher.PublishMessage($"New collaborator created with Id = {colaboratorResultDTO.Id}");
                

                return CreatedAtAction(nameof(GetColaboratorById), new { Id = colaboratorDTO.Id }, colaboratorResultDTO);
            }else{
                return BadRequest(_errorMessages);
            }
        }

        // // DELETE: api/Colaborator/5
        // [HttpDelete("{email}")]
        // public async Task<IActionResult> DeleteColaborator(string email)
        // {
        //     var colaborator = await _context.Colaboradores.FindAsync(email);
        //     if (colaborator == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Colaboradores.Remove(colaborator);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

    }
}
