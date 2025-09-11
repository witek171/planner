using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces.Services;
using Schedule.Contracts.Dtos.Requests;
using Schedule.Contracts.Dtos.Responses;
using Schedule.Domain.Models;

namespace PlannerNet.Controllers;

[ApiController]
[Route("api/[controller]/{companyId:guid}")]
public class ReservationController : ControllerBase
{
	private readonly IReservationService _reservationService;
	private readonly IMapper _mapper;

	public ReservationController(
		IReservationService reservationService,
		IMapper mapper)
	{
		_reservationService = reservationService;
		_mapper = mapper;
	}

	[HttpGet("all")]
	public async Task<ActionResult<List<ReservationResponse>>> GetAll(Guid companyId)
	{
		List<Reservation> reservations = await _reservationService.GetAllAsync(companyId);
		List<ReservationResponse> responses = _mapper.Map<List<ReservationResponse>>(reservations);
		return Ok(responses);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ReservationResponse>> GetById(
		Guid id,
		Guid companyId)
	{
		Reservation? reservation = await _reservationService.GetByIdAsync(id, companyId);
		if (reservation == null)
			return NotFound();

		ReservationResponse response = _mapper.Map<ReservationResponse>(reservation);
		return Ok(response);
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create(
		Guid companyId,
		[FromBody] ReservationRequest request)
	{
		Reservation reservation = _mapper.Map<Reservation>(request);
		reservation.SetCompanyId(companyId);
		Guid id = await _reservationService.CreateAsync(reservation);
		return CreatedAtAction(nameof(Create), id);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult> Update(
		Guid id,
		Guid companyId,
		[FromBody] ReservationRequest request)
	{
		Reservation? reservation = await _reservationService.GetByIdAsync(id, companyId);
		if (reservation == null)
			return NotFound();

		_mapper.Map(request, reservation);
		await _reservationService.UpdateAsync(reservation);
		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult> Delete(
		Guid id,
		Guid companyId)
	{
		Reservation? reservation = await _reservationService.GetByIdAsync(id, companyId);
		if (reservation == null)
			return NotFound();

		await _reservationService.DeleteAsync(id, companyId);
		return NoContent();
	}
}