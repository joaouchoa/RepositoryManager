namespace ABC.RepositoryManager.Application.ValidationMessages
{
    public static class RepoValidationMessages
    {
        public const string NOT_EMPTY_ERROR_MESSAGE = "{PropertyName} cannot be empty.";
        public const string NEGATIVE_NUMBER_ERROR_MESSAGE = "{PropertyName} cannot be a negative number.";
        public const string LIMITE_PER_PAGE_ERROR_MESSAGE = "{PropertyName} cannot be bigger than 30.";
    }
}
