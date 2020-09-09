using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }


    [HttpGet]
    public ActionResult<IEnumerable<VaultKeep>> Get()
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vks.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }


    [HttpGet("{id}")]
    public ActionResult<IEnumerable<VaultKeep>> Get(int id)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        return Ok(_vks.Get(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }


    [HttpPost]
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        newVaultKeep.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vks.Create(newVaultKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
        throw;
      }
    }

    [HttpDelete("{id}")]
    public ActionResult<VaultKeep> Delete(int id)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vks.Delete(userId, id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }

}