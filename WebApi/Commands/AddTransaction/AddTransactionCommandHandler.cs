using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;
using MyWalletApp.WebApi.Mappers;
using TransactionTypeApiModel = MyWalletApp.WebApi.Models.TransactionType;


namespace MyWalletApp.WebApi.Commands.AddTransaction
{
    public class AddTransactionCommandHandler: IRequestHandler<AddTransactionCommand, CommandResult>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AddTransactionCommandHandler(
            ITransactionRepository transactionRepository, 
            IAccountRepository accountRepository,
            ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<CommandResult> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {

            var doesAccountExist = await _accountRepository.DoesExistForUser(request.AccountId);
            
            if(!doesAccountExist) 
                return new CommandResult()
                    {
                        Status = CommandResultStatus.Error, 
                        Message = $"Account with id {request.AccountId} does not exist"
                    };
            
            var transactionType = EnumMapper.Map<TransactionTypeApiModel, TransactionType>(request.TransactionType);

            var account = await _accountRepository.GetById(request.AccountId);
            var currencyId = account.CurrencyId;

            if(request.CategoryId == default(long)) {
                request.CategoryId = null;
            }
            
            var transaction = new Transaction(){
                Name = request.Name,
                Date = request.Date,
                AccountId = request.AccountId,
                Total = GetCorrectTotal(transactionType, request.Total),
                TransactionType = transactionType,
                CurrencyId = (long)currencyId,
                CategoryId = request.CategoryId
            };

            try {
                var transactionId =  await _transactionRepository.Save(transaction);

                return new CommandResult(){Status = CommandResultStatus.Success, Message = "Created"};
            }
            catch (DbUpdateException e)
            {
                return new CommandResult(){Status = CommandResultStatus.Error, Message = e.Message };
            }
            
        }

        private decimal GetCorrectTotal(TransactionType transactionType, decimal total)
        {
            var totalAbsoluteValue = Math.Abs(total);
            return transactionType == TransactionType.Expense 
                ? -totalAbsoluteValue
                : totalAbsoluteValue;
        }
    }
}