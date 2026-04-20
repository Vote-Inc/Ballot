namespace Ballot.API.Controllers;

[ApiController]
[Route("api/ballots")]
public sealed class BallotsController(
    GetActiveElectionsQueryHandler getActiveElectionsQueryHandler,
    GetElectionQueryHandler getElectionQueryHandler) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
    {
        var result = await getActiveElectionsQueryHandler.Handle(new GetActiveElectionsQuery(), cancellationToken);
        
        return result.Match<IActionResult>(
            onSuccess: res => Ok(res),
            onFailure: error => error.Code switch
            {
                _            => BadRequest(error.ToResponse())
            });
    }

    [HttpGet("{electionId:guid}")]
    public async Task<IActionResult> GetById(Guid electionId, CancellationToken cancellationToken)
    {
        var result = await getElectionQueryHandler.Handle(new GetElectionQuery(electionId), cancellationToken);

        return result.Match<IActionResult>(
            onSuccess: Ok,
            onFailure: error => error.Code switch
            {
                "ballot.election.not_found"  => NotFound(error.ToResponse()),
                _                            => BadRequest(error.ToResponse())
                
        }); 
    }
}
