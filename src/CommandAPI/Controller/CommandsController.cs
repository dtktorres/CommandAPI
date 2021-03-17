using System.Windows.Input;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Models;
using AutoMapper;
using CommandAPI.Dtos;
using Microsoft.AspNetCore.JsonPatch;
namespace CommandAPI.Controllers
//Ramdom not
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommandAPIRepo repository, IMapper mapper){
            _repository = repository;
            _mapper = mapper;
        } 
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
           var commandItems = _repository.GetAllCommands();
           return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }
        [HttpGet("{id}",Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
           var commandIem = _repository.GetCommandById(id);
           if(commandIem == null){
               return NotFound();
           }
           return Ok(_mapper.Map<CommandReadDto>(commandIem));
        }
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto){
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel); 
            return CreatedAtRoute(nameof(GetCommandById),new {Id = commandModel.Id},commandReadDto);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto){
            Command cmd = _repository.GetCommandById(id);
            if(cmd == null){
                 return NotFound();
            }
            _mapper.Map(commandUpdateDto,cmd);
           // _repository.UpdateCommand(cmd);
            _repository.SaveChanges();
            return NoContent();

        }
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDto>patchDoc){
            var cmd = _repository.GetCommandById(id);
            if(cmd == null){
                return NotFound();
            }
            var toPatch = _mapper.Map<CommandUpdateDto>(cmd);
            patchDoc.ApplyTo(toPatch,ModelState);
            if(!TryValidateModel(toPatch)){
                return ValidationProblem(ModelState);
            }
            _mapper.Map(toPatch,cmd);
            //_repository.UpdateCommand();
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id){
            var cmd = _repository.GetCommandById(id);
            if(cmd == null){
                return NotFound();
            }
            _repository.DeleteCommand(cmd);
            _repository.SaveChanges();

            return NoContent();
        }

    }
}