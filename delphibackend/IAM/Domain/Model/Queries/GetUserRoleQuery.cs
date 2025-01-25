namespace delphibackend.IAM.Domain.Model.Queries;

    public class GetUserRoleQuery
    {
        public Guid AuthUserId { get; }

        public GetUserRoleQuery(Guid authUserId)
        {
            AuthUserId = authUserId;
        }
    }

    