using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.TrainStations;
using SP23.P02.Web.Features.UserRoles;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;

namespace SP23.P02.Web.Controllers;

[Route("api/stations")]
[ApiController]
//[Authorize (Roles = "Admin")]
public class StationsController : ControllerBase
{
    private readonly DbSet<TrainStation> stations;
    private readonly DataContext dataContext;

    public StationsController(DataContext dataContext)
    {
        this.dataContext = dataContext;
        stations = dataContext.Set<TrainStation>();
    }

    [HttpGet]
    public IQueryable<TrainStationDto> GetAllStations()
    {
        return GetTrainStationDtos(stations);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<TrainStationDto> GetStationById(int id)
    {
        var result = GetTrainStationDtos(stations.Where(x => x.Id == id)).FirstOrDefault();
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult<TrainStationDto> CreateStation(TrainStationDto dto)
    {
        if (IsInvalid(dto))
        {
            return BadRequest();
        }

        var station = new TrainStation
        {
            Name = dto.Name,
            Address = dto.Address,
            Manager = dataContext.Users.Find(dto.ManagerId)
            
        };

        stations.Add(station);

        dataContext.SaveChanges();

        dto.Id = station.Id;
        //dto.Name = station.Name;
        //dto.Address = station.Address;
        //dto.ManagerId = station.Manager.Id;


        return CreatedAtAction(nameof(GetStationById), new { id = dto.Id }, dto);
    }

    public static string GetUserId(IPrincipal user)
    {
        var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
        return claim == null ? null : claim.Value;
    }

    public static string GetUserRoles(IPrincipal user)
    {
        var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Role);
        return claim == null ? null : claim.Value;
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize]
    public ActionResult<TrainStationDto> UpdateStation(int id, TrainStationDto dto)
    {
        if (IsInvalid(dto))
        {
            return BadRequest();
        }

        var station = stations.FirstOrDefault(x => x.Id == id);
        if (station == null)
        {
            return NotFound();
        }

        //if (!(GetUserRoles(User) == "Admin"))
        //{
        //    return Forbid();
        //}

        if (!(dto.ManagerId.ToString() == GetUserId(User)) && !(GetUserRoles(User) == "Admin"))
        {
            return Forbid();
        }

        station.Name = dto.Name;
        station.Address = dto.Address;
        //stop gap measure
        station.Manager = dataContext.Users.Find(dto.ManagerId);

        dataContext.SaveChanges();

        dto.Id = station.Id;
        dto.Name = station.Name;
        dto.Address = station.Address;
        dto.ManagerId = station.Manager.Id;

        return Ok(dto);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize (Roles = "Admin")]
    public ActionResult DeleteStation(int id)
    {
        var station = stations.FirstOrDefault(x => x.Id == id);
        if (station == null)
        {
            return NotFound();
        }

        stations.Remove(station);

        dataContext.SaveChanges();

        return Ok();
    }

    
    private static bool IsInvalid(TrainStationDto dto)
    {
        return string.IsNullOrWhiteSpace(dto.Name) ||
               dto.Name.Length > 120 ||
               string.IsNullOrWhiteSpace(dto.Address);
    }

    private static IQueryable<TrainStationDto> GetTrainStationDtos(IQueryable<TrainStation> stations)
    {
        return stations
            .Select(x => new TrainStationDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                ManagerId = x.Manager.Id
            });
    }
}