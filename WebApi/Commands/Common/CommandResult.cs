namespace MyWalletApp.WebApi.Commands.Common
{
    public class CommandResult
    {
        public CommandResultStatus Status { get; set; }
        public string Message { get; set; }
    }
}