namespace Application.Modules.Pets.Commands;

public class DeletePetCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<DeletePetCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(DeletePetCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        var pet = await _dbContext.Pets
            .FilterSoftDeleted()
            .Include(p => p.Pictures)
            .Include(p => p.Applications)
                .ThenInclude(a => a.Meeting)
            .FirstOrDefaultAsync(p => p.Id == command.PetId, cancellationToken)
            ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundPet, $"Pet with id {command.PetId} not found");

        pet.SoftDelete();
        pet.Pictures.ForEach(p => p.SoftDelete());
        pet.Applications.ForEach(a =>
        {
            if (a.Status is not ApplicationStatus.Approved)
                a.Status = ApplicationStatus.Rejected;

            if (a.Meeting?.Status is MeetingStatus.Scheduled)
                a.Meeting.Status = MeetingStatus.Cancelled;
        });
        await _dbContext.SaveChangesAsync(cancellationToken);

        return pet.Id;
    }
}
