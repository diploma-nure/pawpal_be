namespace Application.Modules.Applications.Commands;

public class SubmitApplicationCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<SubmitApplicationCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(SubmitApplicationCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.User)
            throw new ForbiddenException();

        var pet = await _dbContext.Pets
            .AsNoTracking()
            .FilterSoftDeleted()
            .FirstOrDefaultAsync(p => p.Id == command.PetId, cancellationToken)
            ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundPet, $"Pet with id {command.PetId} not found");

        var application = await _dbContext.Applications
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.UserId == _dbContext.User!.Id && l.PetId == pet.Id, cancellationToken);

        if (application is not null && application.Status is not ApplicationStatus.Rejected)
            return application.Id;

        application = new PetApplication
        {
            UserId = _dbContext.User!.Id,
            PetId = pet.Id,
            Status = ApplicationStatus.WaitingForConsideration,
        };

        _dbContext.Applications.Add(application);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return application.Id;
    }
}
