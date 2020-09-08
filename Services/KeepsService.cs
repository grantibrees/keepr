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

    internal string Delete(int id, string userId)
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

    internal Keep Edit(Keep editKeep, string userId)
    {
      Keep found = Get(editKeep.Id, userId);
      if (found.Views < editKeep.Views)
      {
        if (_repo.IncrementViews(editKeep))
        {
          found.Views = editKeep.Views;
          return found;
        }
        throw new Exception("Problem, can't increment view count.");
      }
      if (found.Keeps < editKeep.Keeps)
      {
        if (_repo.IncrementKeptCount(editKeep))
        {
          found.Keeps = editKeep.Keeps;
          return found;
        }
        throw new Exception("Problem, can't increment kept count.");

      }
      if (found.UserId == userId && _repo.Edit(editKeep, userId))
      {
        return editKeep;
      }
      throw new Exception("You cant do that edit.");

    }



  }
}