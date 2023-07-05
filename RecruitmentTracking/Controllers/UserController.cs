using Microsoft.AspNetCore.Mvc;

using RecruitmentTracking.Models;
using RecruitmentTracking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace RecruitmentTracking.Controllers;

public class UserController
{
	private readonly ApplicationDbContext _dbContext;
	public UserController(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext?? throw new ArgumentNullException("db context is null");
	}
}
