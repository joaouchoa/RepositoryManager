namespace ABC.RepositoryManager.Application.ValidationMessages
{
    public static class RepoValidationMessages
    {
        public const string NOT_EMPTY_ERROR_MESSAGE = "{PropertyName} cannot be empty.";
        public const string NEGATIVE_NUMBER_ERROR_MESSAGE = "{PropertyName} cannot be a negative number.";
        public const string LIMITE_PER_PAGE_ERROR_MESSAGE = "{PropertyName} cannot be bigger than 30.";
        public const string ID_MATCHES_ERROR_MESSAGE = "{PropertyName} can only contain numbers bigger than 0.";
        public const string MAX_LENGTH_ERROR_MESSAGE = "{PropertyName} must not reach {MaxLength} characters.";
        public const string START_END_HYPHEN_ERROR_MESSAGE = "{PropertyName} cannot start or end with a hyphen.";
        public const string CONSECUTIVE_HYPHENS_ERROR_MESSAGE = "{PropertyName} cannot contain two consecutive hyphens.";
        public const string REPO_CREATED_MESSAGE = "Repository created successfully.";
        public const string REPO_DONT_CREATED_ERROR_MESSAGE = "Failed to create the repository.";
        public const string REPO_ALREADY_EXISTS_ERROR_MESSAGE = "Repository already favorited.";
        public const string FAVORITE_REPO_DELETED_ERROR_MESSAGE = "The Favorite Repository was removed.";
        public const string FAVORITE_REPO_DONT_DELETED_ERROR_MESSAGE = "Failed to remove the Favorite Repository.";
    }
}
