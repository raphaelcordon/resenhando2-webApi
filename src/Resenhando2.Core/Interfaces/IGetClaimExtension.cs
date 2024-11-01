namespace Resenhando2.Core.Interfaces;

public interface IGetClaimExtension
{
    bool IsOwner(Guid id);
    string GetUserIdFromClaims();
}