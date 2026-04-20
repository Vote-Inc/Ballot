using Ballot.Domain.Seedwork;

namespace Ballot.Application.Common;

public static class ErrorExtensions
{
    public static object ToResponse(this Error error) => new { error.Code, error.Message };
}
