using Microsoft.AspNetCore.Mvc;
using UnitOfworkAndRepositoryExampleProject.IConfiguration;
using UnitOfworkAndRepositoryExampleProject.Models;

namespace UnitOfworkAndRepositoryExampleProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> Logger;
    private readonly IUnitOfWork UnitOfWork;

    public UsersController(ILogger<UsersController> logger, IUnitOfWork unitOfWork)
    {
        Logger = logger;
        UnitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        if (ModelState.IsValid)
        {
            user.Id = Guid.NewGuid();
            await UnitOfWork.Users.Add(user);
            await UnitOfWork.CompleteAsync();

            return CreatedAtAction("GetItem", new { user.Id }, user);
        }
        return BadRequest("Bad Model State.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItem(Guid id)
    {
        var user = await UnitOfWork.Users.GetById(id);

        return user != null ? Ok(user) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await UnitOfWork.Users.All());
    }

    [HttpPut]
    public async Task<IActionResult> Update(User user)
    {
        if (ModelState.IsValid)
        {
            var isUpdated = await UnitOfWork.Users.Upsert(user);
            await UnitOfWork.CompleteAsync();
            return isUpdated ? Ok() : NotFound();
        }
        return BadRequest("Invalid Model State");
    }
}
