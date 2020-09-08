using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }
    // Public keeps getter
    public Keep Get(int id)
    {
      var found = _repo.GetById(id);
      if (found.IsPrivate == true)
      {
        throw new Exception("Cannot get, Private Keep.");
      }
      return found;
    }
    // Personal keeps getter

    public Keep Get(int id, string userId)
    {
      var found = _repo.GetById(id);
      if (found.UserId == userId)
      {
        return found;
      }
      if (found.IsPrivate == true)
      {
        throw new Exception("Cannot get, Private Keep.");
      }
      return found;

    }
    public Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    public string Delete(int id, string userId)
    {
      Keep found = Get(id, userId);
      if (found.UserId != userId)
      {
        throw new Exception("Cannot get, Not Keep owner.");
      }
      if (_repo.Delete(id, userId))
      {
        return "Successfully Deleted";
      }
      throw new Exception("Error, dev check your code. (deletion)");
    }

    public Keep Edit(Keep keepEdit, string userId)
    {
      Keep foundKeep = Get(keepEdit.Id, userId);
      if (foundKeep.Views < keepEdit.Views && _repo.IncrementViewCount(keepEdit))
      {
        foundKeep.Views = keepEdit.Views;
        return foundKeep;
        // throw new Exception("Problem, can't increment view count.");
      }
      if (foundKeep.Keeps < keepEdit.Keeps && _repo.IncrementKeptCount(keepEdit))
      {
        foundKeep.Keeps = keepEdit.Keeps;
        return foundKeep;
        // throw new Exception("Problem, can't increment kept count.");
      }
      if (foundKeep.UserId == userId && _repo.Edit(keepEdit, userId))
      {
        return keepEdit;
      }
      throw new Exception("Problem, cannot edit, review code. (KeepEdit)");

    }



  }
}